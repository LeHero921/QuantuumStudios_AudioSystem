using UnityEngine;

[CreateAssetMenu(fileName = "newAudio", menuName = "QuantuumAudio/Audio Track")]
public class AudioTrack : ScriptableObject
{
    public string audioName;
    public AudioClip clip;
    [Tooltip("DontChange -> Dont override last zone")]
    public enum AudioType
    {
        Music,
        SFX,
        Ambient,
        DontChange
    }
    public AudioType audioType;
    [Range(0,1)]
    public float volume = 1;
    [Range(-3,3)]
    public float pitch = 1;
    [Range(0,1)]
    public float spatialBlend = 0;
    public bool loop;
    public AudioPreset preset;
}