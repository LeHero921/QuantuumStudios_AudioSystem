using System;
using System.Collections;
using UnityEngine;
using QS_Audio.Requirements;

public class AudioManager : MonoBehaviour
{
    #region Singelton
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    private void Awake() {
        if (_instance == null)
        {
            _instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public SoundEffectRegestry sfxRegestry;

    public AudioSource MusicSource;
    public AudioSource AmbientSource;
    public AudioSource SFXaudioSource;

    public float fadeInDuration;
    public float targetVolume;

    private void OnEnable() {
        AudioActions.OnGlobalAudioFadeOut += GlobalFadeOut;
    }
    private void OnDisable() {
        AudioActions.OnGlobalAudioFadeOut -= GlobalFadeOut;
    }

    public void PrepareAudioSource(AudioSource source, AudioTrack track, bool changeTrack)
    {
        Debug.Log($"Current Source {source.gameObject.name} volume: {source.volume}");
        Debug.Log($"Target volume is: {track.volume}");
        if (changeTrack == true)
        {
            source.loop = track.loop;
            // if audio settings == null
            targetVolume = track.volume;
            if (targetVolume <= 0)
            {
                source.volume = 0;
            }else
            {
                source.volume = targetVolume;
            }
            source.pitch = track.pitch;
            source.spatialBlend = track.spatialBlend;
            source.panStereo = track.preset.stereoPan;
            source.reverbZoneMix = track.preset.reverbZoneMix;

            source.clip = track.clip;
        }else // change track = false => correct volume only
        {
            source.volume = track.volume;
        }
        Debug.Log($"New Source {source.gameObject.name} volume: {source.volume}");
    }

    #region Play Audio
    public void PlayAudio()
    {
        if (MusicSource.clip != null)
        {
            MusicSource.Play();
        }
        if (AmbientSource.clip != null)
        {
            AmbientSource.Play();
        }
    }

    public void PlaySFX(string soundID)
    {
        var track = Array.Find(sfxRegestry.soundEffects, AudioTrack => AudioTrack.SoundName == soundID);
        AudioClip sound = track.SoundClip;
        SFXaudioSource.PlayOneShot(sound);
        Debug.Log($"Played {sound.name} successfull");
    }
    #endregion

    public IEnumerator AsyncFading(AudioZone zone, AudioSource source, bool fadeOut)
    {
        if (fadeOut == true)
        {
            StartCoroutine(FadeOutAudio(source, zone, zone.isCurrentlyAmbient));
        }else
        {
            StartCoroutine(FadeInAudio(source, zone, zone.isCurrentlyAmbient));
        }
        yield return null;
    }

    #region Fading Audio (Logic)
    public IEnumerator FadeInAudio(AudioSource source, AudioZone zone, bool isAmbient)
    {
        Debug.Log($"Fading In: {source.gameObject.name} : {source.clip.name}");
        float currentTime = 0f;

        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            if (isAmbient == false)
            {
                source.volume = Mathf.Lerp(0f, zone.ZoneMusic.volume, currentTime / fadeInDuration);
            }else
            {
                source.volume = Mathf.Lerp(0f, zone.ZoneAmbience.volume, currentTime / fadeInDuration);
            }
            // Debug.Log("current fade in time: " + currentTime);
            yield return null;
        }

        source.volume = targetVolume;
    }

    public IEnumerator FadeOutAudio(AudioSource source, AudioZone zone, bool isAmbient)
    {
        Debug.Log($"Fading Out: {source.gameObject.name} : {source.clip.name}");
        float currentTime = 0f;

        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            if (isAmbient == false)
            {
                source.volume = Mathf.Lerp(zone.ZoneMusic.volume, 0f, currentTime / fadeInDuration);
            }else
            {
                source.volume = Mathf.Lerp(zone.ZoneAmbience.volume, 0f, currentTime / fadeInDuration);
            }
            // Debug.Log("current fade in time: " + currentTime);
            yield return null;
        }

        source.volume = 0f;
    }
    #endregion

    public void GlobalFadeOut() //TODO: Still not working: simultaniously fading out second item (ambiente)
    {
        AudioZone[] zone = FindObjectsOfType<AudioZone>();
        for (int i = 0; i < zone.Length; i++)
        {
            StartCoroutine(FadeOutAudio(MusicSource, zone[i], false));
            StartCoroutine(FadeOutAudio(AmbientSource, zone[i], true));
        }
    }
}
