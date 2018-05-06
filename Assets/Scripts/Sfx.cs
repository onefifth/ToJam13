using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour {

    AudioSource[] clips;

	private void Start()
	{
        clips = GetComponents<AudioSource>();
	}

	public void Play()
    {
        clips[Random.Range(0, clips.Length)].Play();
    }
}
