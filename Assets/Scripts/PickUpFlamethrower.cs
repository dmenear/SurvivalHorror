using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpFlamethrower : MonoBehaviour
{

    public GameObject player, textBox;
    public AudioClip pickupAudio;
    public flamethrowerpartcounter ftpc;
	public InteractiveHandler IH;
    public bool pickedUp = false;
	AudioSource audio;

	void Start(){
		audio = GetComponent<AudioSource> ();
	}

    // Update is called once per frame
    void Update()
    {
		if (IH.Interactable.Contains(this.gameObject) && !pickedUp)
        {
            if (Input.GetButtonDown("Action"))
            {
				audio.PlayOneShot (pickupAudio, 0.7f);

                displayMessage();
                foreach(Transform piece in transform)
                {
                    piece.gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                StartCoroutine(displayMessage());
                pickedUp = true;
                ftpc.body += 1;
                ftpc.UpdateOverlay();
            }
        }
    }

    IEnumerator displayMessage()
    {
        textBox.GetComponent<Text>().text = "Picked up Flamethrower Body";
        yield return new WaitForSecondsRealtime(3.0f);
        textBox.GetComponent<Text>().text = "";
        gameObject.SetActive(false);
    }
}
