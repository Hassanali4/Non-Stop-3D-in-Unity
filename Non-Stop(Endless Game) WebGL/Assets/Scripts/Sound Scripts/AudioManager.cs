//Importing the required namespaces
using UnityEngine.Audio;
using UnityEngine;
using System;

// The Audio Manager script is used to manage the game's sound effects and music.
// It contains an array of Sound objects which are set in the Unity Editor and can be played by their name.
// The AudioManager is a Singleton, meaning that there can only be one instance of it in the game at any given time.
// It adds an AudioSource component to each sound and sets its properties such as volume, pitch and looping.
// It also has a Play() and Stop() function which can be called to play or stop a sound respectively.
// The Start() function is called before the first frame update and is used to play the background music.
// The Play() and Stop() functions find the Sound object in the array by name and play or stop the AudioSource accordingly.

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;//Array of Sound objects

    public static AudioManager Instance;//Static reference to the AudioManager

    // Start is called before the first frame update
    void Awake()
    {//If there is no AudioManager instance, set it to this script, else destroy this instance and return
        if (Instance == null)   
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        //Loop through the sounds array and add AudioSources to each one
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update{
    void Start()//The start function is to play the background audio sound
    {
        Play("MainTheme"); //Play the MainTheme sound when the game starts , but its name has to be the string "MainTheme" 
    }

    // Update is called once per frame
    public void Play(string name)// Play the sound with the given name
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);//Find the sound with the given name
        if (s == null)
        {
            Debug.Log("Sound :" + name + "Not Found."); //If the sound is not found, log an error
            return;
        }
        s.source.Play(); //Play the sound
    }
    public void Stop(string name)// Stop the sound with the given name
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);//Find the sound with the given name
        if (s == null)
        {
            Debug.Log("Sound :" + name + "Not Found.");//If the sound is not found, log an error
            return;
        }
        s.source.Stop();//Stop the sound    
    }
    
}