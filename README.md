<h1 align="center">QuantuumStudios Audio System</h1>

<h3 align="center">
v1 of QS Audio System
</h3>

---

<h2 align="center">
Audio System Overview
</h2>

Important Scripts:
- Audio Manager
- Audio Zone
- Audio Track (Scriptable Object)
- Audio Preset
- Sound Effects Regestry

---
<h2 align="center">Audio Manager</h2>

<h2 align="center" style="color:red;"> ! ADD THIS TO EVERY SCENE !</h2>

<h4>Parameter:</h4>
SFX Regestry: add your SFX regestry here <br>
Music / Ambient / SFX Source: Add a music source to the scene (at best, directly as a child) <br>
Fade In Duration: float, seconds of fade (music, ambience)
Target Vol: gets set by audio Tracks volume

---
<h2 align="center">
Audio Zone
</h2>

The Audio Zone script gets attatched to any gameObject that has a collider. When the player enters an Audio Zone, music or ambience will play.

<h4>Parameter:</h4>
Play Always: Should the audio zone always trigger? (Global/ No Collision)
<br> Zone Music/ Ambience: The AudioTrack the Zone will trigger
<br> Music/ Ambience Behaviour: Try what works... I myself dont quite understand what I wrote there ;)


---
<h2 align="center">
Audio Track
</h2>

A Audio Track is a ScriptableObject that is used to store the music or ambience audio.

<h4>Parameter:</h4>
Name: ... name lol <br>
Audio Type: Ambient / Music / SFX<br>
Volume: 0 to 1<br>
Pitch: -3 to 3<br>
Spatial Blend: 0 to 1<br>
Loop: bool, should the track loop?<br>
Preset: AudioPreset (see below)


---
<h2 align="center">
Audio Preset
</h2>

The Audio Preset holds information that a Track transfers to the target audio source.

<h4>Parameter:</h4>
Stereo Pan: well its a Stereo Pan... <br>
Reverb Zone Mix: 0 to 1.1 <br>
Reverb Effect: Choose a Reverb Effect

---
<h2 align="center">
Sound Effect Regestry
</h2>

Stores all sound effects
