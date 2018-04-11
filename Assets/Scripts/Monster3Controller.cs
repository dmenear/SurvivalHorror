using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3Controller : MonoBehaviour {

	public GameObject player, prevWP, nextWP, jumpScare;
	public GameObject[] waypoints;
	public GameStateManager stateManager;
	public AudioClip jumpScareSound;
	public MusicController Music;
	AudioSource audio;
	float accuracy = 2.5f;
	float distanceToWP;
	Vector3 direction;
	bool killed;

	// Use this for initialization
	void Start () {
		killed = false;
		audio = stateManager.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		distanceToWP = Vector3.Distance(this.transform.position, nextWP.transform.position);
		direction = nextWP.transform.position - transform.position;
		direction.y = 0f;
		if (distanceToWP > accuracy) {
			this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), 3.5f * Time.deltaTime);
			this.transform.Translate (0, 0, 8.5f * Time.deltaTime);
		} else {
			if (nextWP == waypoints [0]) {
				if (prevWP == waypoints [12]) {
					prevWP = nextWP;
					nextWP = waypoints [1];
				} else if (prevWP == waypoints [1]) {
					prevWP = nextWP;
					nextWP = waypoints [12];
				}
			} else if (nextWP == waypoints [1]) {
				if (prevWP == waypoints [0]) {
					prevWP = nextWP;
					nextWP = waypoints [2];
				} else if (prevWP == waypoints [2]) {
					prevWP = nextWP;
					nextWP = waypoints [0];
				}
			}else if (nextWP == waypoints [2]) {
				if (prevWP == waypoints [1]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [3];
					} else {
						nextWP = waypoints [8];
					}
				} else if (prevWP == waypoints [3]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [1];
					} else {
						nextWP = waypoints [8];
					}
				} else if (prevWP == waypoints [8]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [1];
					} else {
						nextWP = waypoints [3];
					}
				}
			} else if (nextWP == waypoints [3]) {
				if (prevWP == waypoints [4]) {
					prevWP = nextWP;
					nextWP = waypoints [2];
				} else if (prevWP == waypoints [2]) {
					prevWP = nextWP;
					nextWP = waypoints [4];
				}
			} else if (nextWP == waypoints [4]) {
				if (prevWP == waypoints [3]) {
					prevWP = nextWP;
					nextWP = waypoints [5];
				} else if (prevWP == waypoints [5]) {
					prevWP = nextWP;
					nextWP = waypoints [3];
				}
			} else if (nextWP == waypoints [5]) {
				if (prevWP == waypoints [4]) {
					prevWP = nextWP;
					nextWP = waypoints [6];
				} else if (prevWP == waypoints [6]) {
					prevWP = nextWP;
					nextWP = waypoints [4];
				}
			} else if (nextWP == waypoints [6]) {
				if (prevWP == waypoints [5]) {
					prevWP = nextWP;
					nextWP = waypoints [7];
				} else if (prevWP == waypoints [7]) {
					prevWP = nextWP;
					nextWP = waypoints [5];
				}
			} else if (nextWP == waypoints [7]) {
				if (prevWP == waypoints [6]) {
					prevWP = nextWP;
					nextWP = waypoints [15];
				} else if (prevWP == waypoints [15]) {
					prevWP = nextWP;
					nextWP = waypoints [6];
				}
			} else if (nextWP == waypoints [8]) {
				if (prevWP == waypoints [15]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [9];
					} else {
						nextWP = waypoints [2];
					}
				} else if (prevWP == waypoints [2]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [9];
					} else {
						nextWP = waypoints [15];
					}
				} else if (prevWP == waypoints [9]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [2];
					} else {
						nextWP = waypoints [15];
					}
				}
			} else if (nextWP == waypoints [9]) {
				if (prevWP == waypoints [8]) {
					prevWP = nextWP;
					nextWP = waypoints [10];
				} else if (prevWP == waypoints [10]) {
					prevWP = nextWP;
					nextWP = waypoints [8];
				}
			} else if (nextWP == waypoints [10]) {
				if (prevWP == waypoints [9]) {
					prevWP = nextWP;
					nextWP = waypoints [11];
				} else if (prevWP == waypoints [11]) {
					prevWP = nextWP;
					nextWP = waypoints [9];
				}
			} else if (nextWP == waypoints [11]) {
				if (prevWP == waypoints [10]) {
					prevWP = nextWP;
					nextWP = waypoints [12];
				} else if (prevWP == waypoints [12]) {
					prevWP = nextWP;
					nextWP = waypoints [10];
				}
			} else if (nextWP == waypoints [12]) {
				if (prevWP == waypoints [11]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [13];
					} else {
						nextWP = waypoints [0];
					}
				} else if (prevWP == waypoints [0]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [13];
					} else {
						nextWP = waypoints [11];
					}
				} else if (prevWP == waypoints [13]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [11];
					} else {
						nextWP = waypoints [0];
					}
				}
			} else if (nextWP == waypoints [13]) {
				if (prevWP == waypoints [12]) {
					prevWP = nextWP;
					nextWP = waypoints [14];
				} else if (prevWP == waypoints [14]) {
					prevWP = nextWP;
					nextWP = waypoints [12];
				}
			} else if (nextWP == waypoints [14]) {
				if (prevWP == waypoints [13]) {
					prevWP = nextWP;
					nextWP = waypoints [15];
				} else if (prevWP == waypoints [15]) {
					prevWP = nextWP;
					nextWP = waypoints [13];
				}
			} else if (nextWP == waypoints [15]) {
				if (prevWP == waypoints [7]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [8];
					} else {
						nextWP = waypoints [14];
					}
				} else if (prevWP == waypoints [8]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [7];
					} else {
						nextWP = waypoints [14];
					}
				} else if (prevWP == waypoints [14]) {
					prevWP = nextWP;
					if (Random.Range (0, 2) == 1) {
						nextWP = waypoints [7];
					} else {
						nextWP = waypoints [8];
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject == player.gameObject && !killed){
			killed = true;
			jumpScare.SetActive (true);
			GetComponent<AudioSource> ().enabled = false;
			audio.PlayOneShot (jumpScareSound, 1.3f);
			stateManager.EndGame ();
			Music.audio.Stop ();
		}
	}
}
