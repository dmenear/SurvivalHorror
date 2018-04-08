using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	public GameObject torch;
	public AudioSource audio;
	public AudioClip ambient, chase, spider, maze, finalBoss;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.Play ();
	}

	public void changeToChase(){
		audio.Stop ();
		audio.clip = chase;
		audio.volume = 0.4f;
		audio.Play ();
	}

	public void changeToAmbient(){
		audio.Stop ();
		audio.clip = ambient;
		audio.volume = 0.3f;
		audio.Play ();
	}

	public void changeToSpider(){
		audio.Stop ();
		audio.clip = spider;
		audio.volume = 0.4f;
		audio.Play ();
	}

	public void changeToMaze(){
		audio.Stop ();
		audio.clip = maze;
		audio.volume = 0.4f;
		audio.Play ();
	}

	public void changeToFinalBoss(){
		audio.Stop ();
		audio.clip = finalBoss;
		audio.volume = 0.6f;
		audio.Play ();
	}

	public void turnOffMusic(){
		audio.Stop ();
	}
}
