﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Music_Manager : MonoBehaviour
{

    #region Vars
    public AudioClip title_Music;                    //Assign Audioclip for title music loop
    public AudioClip main_Music;                     //Assign Audioclip for main 
    public AudioMixerSnapshot volume_Down;           //Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
    public AudioMixerSnapshot volume_Up;             //Reference to Audio mixer snapshot in which the master volume of main mixer is turned up


    private AudioSource music_Source;                //Reference to the AudioSource which plays music
    private float resetTime = .01f;                 //Very short time used to fade in near instantly without a click
    #endregion

    void Awake()
    {
        //Get a component reference to the AudioSource attached to the UI game object
        music_Source = GetComponent<AudioSource>();
        //Call the PlayLevelMusic function to start playing music
    }


    public void PlayLevelMusic()
    {
        //This switch looks at the last loadedLevel number using the scene index in build settings to decide which music clip to play.
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //If scene index is 0 (usually title scene) assign the clip titleMusic to musicSource
            case 0:
                music_Source.clip = title_Music;
                break;
            //If scene index is 1 (usually main scene) assign the clip mainMusic to musicSource
            case 1:
                music_Source.clip = main_Music;
                break;
        }
        //Fade up the volume very quickly, over resetTime seconds (.01 by default)
        FadeUp(resetTime);
        //Play the assigned music clip in musicSource
        music_Source.Play();
    }

    //Used if running the game in a single scene, takes an integer music source allowing you to choose a clip by number and play.
    public void PlaySelectedMusic(int music_Choice)
    {

        //This switch looks at the integer parameter musicChoice to decide which music clip to play.
        switch (music_Choice)
        {
            //if musicChoice is 0 assigns title_Music to audio source
            case 0:
                music_Source.clip = title_Music;
                break;
            //if musicChoice is 1 assigns main_Music to audio source
            case 1:
                music_Source.clip = main_Music;
                break;
        }
        //Play the selected clip
        music_Source.Play();
    }

    //Call this function to very quickly fade up the volume of master mixer
    public void FadeUp(float fade_Time)
    {
        //call the TransitionTo function of the audioMixerSnapshot volume_Up;
        volume_Up.TransitionTo(fade_Time);
    }

    //Call this function to fade the volume to silence over the length of fade_Time
    public void FadeDown(float fade_Time)
    {
        //call the TransitionTo function of the audioMixerSnapshot volumeDown;
        volume_Down.TransitionTo(fade_Time);
    }
}
