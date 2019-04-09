using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class audio : MonoBehaviour
{

    public AudioClip[] otherClip; //make an arrayed variable (so you can attach more than one sound)

    // Play random sound from variable
    public void PlaySound()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        //Assign random sound from variable
        audio.clip = otherClip[Random.Range(0, otherClip.Length)];

        audio.Play();

        // Wait for the audio to have finished
        //yield WaitForSeconds(audio.clip.length);


        //Now you should re-loop this function Like
        //PlaySound();
    }
}