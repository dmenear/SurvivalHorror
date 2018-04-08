using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestExitTrigger : MonoBehaviour {

	public GameObject Player, Door, DoorTrigger, MazeDoor, SkeletonHolder;
	public GameObject[] Monsters, TorchHolders;
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject == Player.gameObject) {
			if (DoorTrigger.GetComponent<DoorCellOpen> ().SlamBehind) {
				DoorTrigger.GetComponent<DoorCellOpen> ().SlamBehind = false;
				DoorTrigger.GetComponent<DoorCellOpen> ().SlamDoorBehind ();
				DoorTrigger.GetComponent<DoorCellOpen> ().DoorLocked = true;
				StartCoroutine (killMonsters ());
				MazeDoor.GetComponent<DoorCellOpen> ().SkeletonDoor = true;
				MazeDoor.GetComponent<DoorCellOpen> ().CloseDoor ();
				foreach(GameObject torchHolder in TorchHolders){
					foreach (Transform torch in torchHolder.transform) {
						torch.Find ("TorchFlame").gameObject.SetActive (false);
						torch.Find ("TorchIllumination").gameObject.SetActive (false);
					}
				}
				SkeletonHolder.SetActive (true);
			}
		}
	}

	IEnumerator killMonsters(){
		yield return new WaitForSecondsRealtime (0.5f);
		foreach (GameObject monster in Monsters) {
			monster.SetActive (false);
		}
	}
}
