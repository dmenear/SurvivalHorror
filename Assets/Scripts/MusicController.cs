using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	public AudioSource audio;
	public AudioClip ambient, chase, spider, maze;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeToChase(){
		audio.Stop ();
		audio.clip = chase;
		audio.Play ();
	}

	public void changeToAmbient(){
		audio.Stop ();
		audio.clip = ambient;
		audio.Play ();
	}

	public void changeToSpider(){
		audio.Stop ();
		audio.clip = spider;
		audio.Play ();
	}

	public void changeToMaze(){
		audio.Stop ();
		audio.clip = maze;
		audio.Play ();
	}
}
