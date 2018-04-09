using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpiderController : MonoBehaviour {


	public GameObject Player, WPAttack, WPReturn, FlamethrowerFlame, SpiderMesh;
	public GameObject[] PanicWaypoints;
	public ParticleSystem Flame, Smoke;
	public Material burnedMat;
	public float posAccuracy = 0.5f, rotSpeed = 4.0f, rotAccuracy = 6.0f;
	public float walkSpeed = 3.0f, chaseSpeed = 7.5f, panicSpeed = 8.5f, diff;
	public AudioClip snarl;
	AudioSource audio;
	Animator anim;
	public int panicID;
	public bool idle, onFire, panicStarted, dead, fireDieStarted, smokeDieStarted;
	bool panicWP0Reached, panicWP1Reached, killFire;
	Vector3 direction;

	void Start () {
		idle = true;
		onFire = false;
		panicStarted = false;
		panicWP0Reached = false;
		panicWP1Reached = false;
		killFire = false;
		fireDieStarted = false;
		smokeDieStarted = false;
		dead = false;
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		panicID = 2;
	}

	void Update () {
		if (!dead) {
			if (onFire) {
				SpiderMesh.GetComponent<SkinnedMeshRenderer>().material.Lerp (SpiderMesh.GetComponent<SkinnedMeshRenderer>().material, burnedMat, 0.1f * Time.deltaTime);
				setChasing ();
				if (!panicWP0Reached) {
					direction = PanicWaypoints [0].transform.position - this.transform.position;
					direction.y = 0;
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 7.0f * Time.deltaTime);
					diff = Mathf.Abs (this.transform.rotation.eulerAngles.y - Quaternion.LookRotation (direction).eulerAngles.y);
					if (diff <= rotAccuracy || diff >= 360.0f - rotAccuracy) {
						setChasing ();
						this.transform.Translate (0, 0, panicSpeed * Time.deltaTime);
					} 
					if (direction.magnitude < 0.7f) {
						panicWP0Reached = true;
					}
				} else if (!panicWP1Reached) {
					direction = PanicWaypoints [1].transform.position - this.transform.position;
					direction.y = 0;
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 8.0f * Time.deltaTime);
					this.transform.Translate (0, 0, panicSpeed * Time.deltaTime);
					if (direction.magnitude < 0.7f) {
						panicWP1Reached = true;
					}
				} else {
					if (panicID < PanicWaypoints.Length) {
						direction = PanicWaypoints [panicID].transform.position - this.transform.position;
						direction.y = 0;
						if (direction.magnitude < 2.0f) {
							panicID++;
							panicSpeed -= 1.0f;
							anim.speed -= 0.1f;
						} else {
							this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 2.5f * Time.deltaTime);
							this.transform.Translate (0, 0, panicSpeed * Time.deltaTime);
						}
					} else {
						anim.speed = 1.0f;
						anim.SetBool ("isChasing", false);
						anim.SetBool ("isDying", true);
						dead = true;
						if (!fireDieStarted) {
							StartCoroutine (fireDie ());
							fireDieStarted = true;
						}
					}
				}

			} else {
				if (CheckMiniSpiderZone.InZone) {
					idle = false;
					direction = WPAttack.transform.position - this.transform.position;
					direction.y = 0;
					this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
					diff = Mathf.Abs (this.transform.rotation.eulerAngles.y - Quaternion.LookRotation (direction).eulerAngles.y);
					if (diff <= rotAccuracy || diff >= 360.0f - rotAccuracy) {
						setChasing ();
						this.transform.Translate (0, 0, chaseSpeed * Time.deltaTime);
					} else {
						setWalking ();
					}
				} else {
					if (idle) {
						direction = WPAttack.transform.position - this.transform.position;
						direction.y = 0;
						this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
						diff = Mathf.Abs (this.transform.rotation.eulerAngles.y - Quaternion.LookRotation (direction).eulerAngles.y);
						if (diff <= rotAccuracy || diff >= 360.0f - rotAccuracy) {
							setIdle ();
						}
					} else {
						setWalking ();
						direction = WPReturn.transform.position - this.transform.position;
						direction.y = 0;
						this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
						diff = Mathf.Abs (this.transform.rotation.eulerAngles.y - Quaternion.LookRotation (direction).eulerAngles.y);
						if (diff <= rotAccuracy || diff >= 360.0f - rotAccuracy) {
							this.transform.Translate (0, 0, walkSpeed * Time.deltaTime);
						}
						if (direction.magnitude < posAccuracy) {
							idle = true;
						}
					}
				}
			}
		} else {
			if (killFire) {
				ParticleSystem.MainModule flameMain = Flame.main;
				if (flameMain.startLifetime.constant > 0.33f) {
					flameMain.startLifetime = Mathf.Lerp (flameMain.startLifetime.constant, 0f, 0.5f * Time.deltaTime);
					flameMain.startSpeed = Mathf.Lerp (flameMain.startSpeed.constant, 0f, 2.5f * Time.deltaTime);
				} else {
					Flame.Stop ();
				}
				ParticleSystem.MainModule smokeMain = Smoke.main;
				if (smokeMain.startLifetime.constant > 2f && smokeMain.startSpeed.constant > 0.01f) {
					smokeMain.startLifetime = Mathf.Lerp (smokeMain.startLifetime.constant, 1.4f, 0.5f * Time.deltaTime);
					smokeMain.startSpeed = Mathf.Lerp (smokeMain.startSpeed.constant, 0f, 0.5f * Time.deltaTime);
				} else {
					smokeMain.startLifetime = 2.3f;
					smokeMain.startSpeed = 0.4f;
					SpiderMesh.GetComponent<MeshCollider> ().enabled = true;
					GetComponent<BoxCollider> ().enabled = false;
					if (!smokeDieStarted) {
						smokeDieStarted = true;
						StartCoroutine (smokeDie ());
					}
				}
			}
		}
	}

	void setChasing(){
		anim.SetBool ("isIdle", false);
		anim.SetBool ("isWalking", false);
		if(!anim.GetBool("isChasing")){
			anim.SetBool ("isChasing", true);
			audio.PlayOneShot (snarl, 0.7f);
		}
	}

	void setWalking(){
		anim.SetBool ("isIdle", false);
		if (!anim.GetBool ("isWalking")) {
			anim.SetBool ("isWalking", true);
		}
		anim.SetBool ("isChasing", false);
	}

	void setIdle(){
		if (!anim.GetBool ("isIdle")) {
			anim.SetBool ("isIdle", true);
		}
		anim.SetBool ("isWalking", false);
		anim.SetBool ("isChasing", false);
	}

	void OnParticleCollision(GameObject other){
		if (!dead) {
			if (other.gameObject == FlamethrowerFlame.gameObject) {
				if (!Flame.isPlaying) {
					Flame.Play ();
					Smoke.Play ();
					StartCoroutine (fireWait ());
				}
			}
		}
	}

	IEnumerator fireWait(){
		yield return new WaitForSeconds (0.25f);
		onFire = true;
	}

	IEnumerator fireDie(){
		yield return new WaitForSeconds (1.0f);
		killFire = true;
	}

	IEnumerator smokeDie(){
		yield return new WaitForSeconds (12.0f);
		Smoke.Stop ();
		killFire = false;
	}
}
