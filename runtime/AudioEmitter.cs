// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AudioEmitter : MonoBehaviour
// {
//     public AudioManager audioManager => AudioManager.Instance;
//     public SoundEffect[] Sound;
//     [Range(0,1)]
//     public int SoundPriority = 0;

//     public AudioSource source;

//     private AudioClip clip;
//     // TODO: looping sfx like player drive sound or space donut glow, swirl sound

//     public void PlaySFX(string audioName)
//     {
//         Debug.Log("Trying to play SFX");
//         SoundEffect sound = Array.Find(Sound, SoundEffect => SoundEffect.SoundName == audioName);
//         clip = sound.SoundClip;
//         Debug.Log("Now playing SFX: " + audioName + " or file: " + sound.SoundClip.name);
//         audioManager.PlaySFX(SoundPriority, clip);
//     }
    
//     public void PlaySFXlocal(string audioName)
//     {
//         SoundEffect sound = Array.Find(Sound, SoundEffect => SoundEffect.SoundName == audioName);
//         clip = sound.SoundClip;
//         source.PlayOneShot(clip);
//     }
// }