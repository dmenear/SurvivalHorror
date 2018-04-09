using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class flamethrowerpartcounter : MonoBehaviour {

    public GameObject player, fpsController, overlay, flamethrower, textBox;
    public AudioClip crafting, gas, drawFlamethrower;
    AudioSource audio;
    public int body, tank, cans;
    bool activated;

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        activated = false;
        body = 0;
        tank = 0;
        cans = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (body == 1 && tank == 1 && cans == 5)
        {
            if (!activated)
            {
                activated = true;
                StartCoroutine(displayMessage());
            }
        }
	}

    public void UpdateOverlay()
    {
        overlay.GetComponent<Text>().text = "Flamethrower Body:\t\t" + body + "/1\n";
        overlay.GetComponent<Text>().text += "Flamethrower Tank:\t\t" + tank + "/1\n";
        overlay.GetComponent<Text>().text += "Fuel Cans:\t\t\t\t\t" + cans + "/5";
    }

    IEnumerator displayMessage()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        textBox.GetComponent<Text>().text = "Constructing Flamethrower...";
        audio.PlayOneShot(crafting, 0.7f);
        yield return new WaitForSecondsRealtime(6.0f);
        audio.PlayOneShot(gas, 0.7f);
        yield return new WaitForSecondsRealtime(2.0f);
        textBox.GetComponent<Text>().text = "";
        textBox.GetComponent<Text>().text = "Flamethrower complete";
        audio.PlayOneShot(drawFlamethrower, 0.7f);
        yield return new WaitForSecondsRealtime(1.0f);
        textBox.GetComponent<Text>().text = "";
        flamethrower.SetActive(true);
        overlay.SetActive(false);
		fpsController.GetComponent<FirstPersonController> ().flashlightEnabled = false;
    }


}
