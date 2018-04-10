using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupGasContainers : MonoBehaviour
{

    public GameObject player, textBox;
    public flamethrowerpartcounter ftpc;
    public AudioClip pickupSound;
	public InteractiveHandler IH;
    public bool pickedUp = false;
    public static int containersPickedUp = 0;
	AudioSource audio;

	void Start(){
		audio = GetComponent<AudioSource> ();
	}

    void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
		if (IH.Interactable.Contains(this.gameObject) && !pickedUp)
        {
            if (Input.GetButtonDown("Action"))
            {
				audio.PlayOneShot (pickupSound, 0.7f);
                GetComponent<MeshRenderer>().enabled = false;
                pickedUp = true;
                containersPickedUp++;
                StartCoroutine(displayMessage());
                ftpc.cans += 1;
                ftpc.UpdateOverlay();
            }
        }
    }

    IEnumerator displayMessage()
    {
        textBox.GetComponent<Text>().text = "Picked up Fuel Can";
        yield return new WaitForSecondsRealtime(3.0f);
        textBox.GetComponent<Text>().text = "";
        gameObject.SetActive(false);
    }
}
