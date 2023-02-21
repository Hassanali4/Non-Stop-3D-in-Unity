// This script defines a custom data type called "Sound".
// It's used to store the details of an audio clip like its name, audio clip file, volume, pitch, and loop status.
// Import required libraries
using UnityEngine;
using UnityEngine.Audio;

// Define a serializable class for sound
[System.Serializable]
public class Sound 
{
    public string name;// name of the audio clip    
    public AudioClip clip;// audio clip file

    [Range(0f,1f)]
    public float volume;// volume of the audio clip, ranges from 0 to 1

    [Range(.1f,3f)]
    public float pitch;// pitch of the audio clip, ranges from 0.1 to 3

    public bool loop;// whether the audio clip should loop or not

    [HideInInspector]
    public AudioSource source;// audio source component attached to the object that plays the audio clip
}
