using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupGasContainers : MonoBehaviour
{

    public GameObject player, textBox;
    public AudioClip pickupSound;
    public float distance, angle;
    public bool onFloor = false;
    public bool pickedUp = false;
    public static int containersPickedUp = 0;
	AudioSource audio;

	void Start(){
		audio = GetComponent<AudioSource> ();
	}

    void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        angle = Vector3.Angle(direction, player.transform.forward);
        distance = direction.magnitude;
        if (angle >= 160 && distance <= (onFloor ? 2.3f : 2f) && !pickedUp)
        {
            if (Input.GetButtonDown("Action"))
            {
				audio.PlayOneShot (pickupSound, 0.7f);
                GetComponent<MeshRenderer>().enabled = false;
                pickedUp = true;
                containersPickedUp++;
                StartCoroutine(displayMessage());
            }
        }
    }

    IEnumerator displayMessage()
    {
        textBox.GetComponent<Text>().text = "Gas source " + containersPickedUp + "/5";
        yield return new WaitForSecondsRealtime(3.0f);
        textBox.GetComponent<Text>().text = "";
        gameObject.SetActive(false);
    }
}
