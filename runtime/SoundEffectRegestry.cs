using System;
using UnityEngine;

[CreateAssetMenu(menuName = "QuantuumAudio/Sound Regestry")]
public class SoundEffectRegestry : ScriptableObject
{
    public string RegestyID;
    public SoundEffect[] soundEffects;
}

[Serializable]
public class SoundEffect
{
    public string SoundName;
    public AudioClip SoundClip;
}
