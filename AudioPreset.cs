using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuantuumAudio/Audio Preset", fileName = "newAudioPreset")]
public class AudioPreset : ScriptableObject
{
    [Header("Audio Source Preset")]

    [Range(-1,1)]
    public float stereoPan = 0;
    [Range(0,1.1f)]
    public float reverbZoneMix = 1;

    [Header("Reverb Effect")]
    public AudioReverbPreset reverbPreset;
}
