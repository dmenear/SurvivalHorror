using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpFlamethrowerGas : MonoBehaviour
{

    public GameObject player, textBox;
    public flamethrowerpartcounter ftpc;
    public AudioClip pickupSound;
	public InteractiveHandler IH;
    public bool pickedUp = false;
	AudioSource audio;

	void Start(){
		audio = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
		if (IH.Interactable.Contains(this.gameObject) && !pickedUp)
        {
            if (Input.GetButtonDown("Action"))
            {
				audio.PlayOneShot (pickupSound, 0.7f);
                GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(displayMessage());
                pickedUp = true;
                ftpc.tank += 1;
                ftpc.UpdateOverlay();
            }
        }
    }

    IEnumerator displayMessage()
    {
        textBox.GetComponent<Text>().text = "Picked up Flamethrower Tank";
        yield return new WaitForSecondsRealtime(3.0f);
        textBox.GetComponent<Text>().text = "";
        gameObject.SetActive(false);
    }
}
