// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AudioSourceControl : MonoBehaviour
// {
//     public float maxAudioVolume;
//     public AudioSource source;

//     private void OnValidate() {
//         if (source == null && gameObject.GetComponent<AudioSource>())
//         {
//             source = gameObject.GetComponent<AudioSource>();
//         }
//     }

//     private void Update() {
//         if (source.volume > maxAudioVolume)
//         {
//             source.volume = maxAudioVolume;
//             Debug.Log("Source volume is over maxVolume |vol: " + source.volume);
//         }
//     }
// }
