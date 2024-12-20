using System.Collections;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    private AudioManager audioManager => AudioManager.Instance;

    public bool ignoreEmptyAudio;
    public bool playAlways;
    public AudioTrack ZoneMusic;
    public AudioTrack ZoneAmbience;

    public enum ZoneBehaviour{
        cancel,
        DontChange
    }
    public ZoneBehaviour musicBehaviour;
    public ZoneBehaviour ambienceBehaviour;

    [HideInInspector] public bool isCurrentlyAmbient;

    private void OnValidate() {
        if (gameObject.GetComponent<BoxCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        }
        if (gameObject.GetComponent<AudioReverbZone>() == null)
        {
            gameObject.AddComponent<AudioReverbZone>();
        }
        if (ZoneMusic.clip == null)
        {
            musicBehaviour = ZoneBehaviour.DontChange;
        }else
        {
            musicBehaviour = ZoneBehaviour.cancel;
        }
        if (ZoneAmbience.clip == null)
        {
            ambienceBehaviour = ZoneBehaviour.DontChange;
        }else
        {
            ambienceBehaviour = ZoneBehaviour.cancel;
        }
    }

    private void Start() {
        if (playAlways == true)
        {
            if (ZoneMusic.audioType != AudioTrack.AudioType.DontChange || ZoneMusic.clip != null)
            {
                audioManager.PrepareAudioSource(audioManager.MusicSource, ZoneMusic, true);
                audioManager.PlayAudio();
                audioManager.StartCoroutine(audioManager.AsyncFading(this, audioManager.MusicSource, false));
            }
            // Debug.Log($"{ZoneMusic.clip} is this null?");
            if (ZoneAmbience.audioType != AudioTrack.AudioType.DontChange || ZoneAmbience.clip != null)
            {
                audioManager.PrepareAudioSource(audioManager.AmbientSource, ZoneAmbience, true);
                audioManager.PlayAudio();
                audioManager.StartCoroutine(audioManager.AsyncFading(this, audioManager.AmbientSource, false));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PrepareMusic());
            StartCoroutine(PrepareAmbiente());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOutMusic());
            StartCoroutine(FadeOutAmbiente());
        }
    }

    public IEnumerator PrepareMusic()
    {
        audioManager.PrepareAudioSource(audioManager.MusicSource, ZoneMusic, true);
        audioManager.PlayAudio();

        isCurrentlyAmbient = false;
        audioManager.StartCoroutine(audioManager.AsyncFading(this, audioManager.MusicSource, false));
        yield return null;
    }

    public IEnumerator PrepareAmbiente()
    {
        audioManager.PrepareAudioSource(audioManager.AmbientSource, ZoneAmbience, true);
        audioManager.PlayAudio();

        isCurrentlyAmbient = true;
        audioManager.StartCoroutine(audioManager.AsyncFading(this, audioManager.AmbientSource, false));
        yield return null;
    }

    public IEnumerator FadeOutMusic()
    {
        if (musicBehaviour == ZoneBehaviour.cancel)
        {
            isCurrentlyAmbient = false;
            audioManager.StartCoroutine(audioManager.AsyncFading(this, audioManager.MusicSource, true));
        }
        yield return null;
    }

    public IEnumerator FadeOutAmbiente()
    {
        if (ambienceBehaviour == ZoneBehaviour.cancel)
        {
            isCurrentlyAmbient = true;
            audioManager.StartCoroutine(audioManager.AsyncFading(this, audioManager.AmbientSource, true));
        }
        yield return null;
    }
}
