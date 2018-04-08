using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flamethrowerpartcounter : MonoBehaviour {

    public GameObject player, overlay, flamethrower;
    public int body, tank, cans;
    // Use this for initialization
    void Start () {
        body = 0;
        tank = 0;
        cans = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (body == 1 && tank == 1 && cans == 5)
        {
            flamethrower.SetActive(true);
        }
	}

    public void UpdateOverlay()
    {
        overlay.GetComponent<Text>().text = "Flamethrower Body:\t\t" + body + "/1\n";
        overlay.GetComponent<Text>().text += "Flamethrower Tank:\t\t" + tank + "/1\n";
        overlay.GetComponent<Text>().text += "Fuel Cans:\t\t\t\t\t" + cans + "/5";
    }

}
