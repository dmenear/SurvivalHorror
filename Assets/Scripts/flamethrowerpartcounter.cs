using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flamethrowerpartcounter : MonoBehaviour {

    public GameObject player, textBox, flamethrower;
    public 
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator displayMessage()
    {
        textBox.GetComponent<Text>().text = "Flamethrower complete.";
        yield return new WaitForSecondsRealtime(3.0f);
        textBox.GetComponent<Text>().text = "";
        gameObject.SetActive(false);
    }
}
