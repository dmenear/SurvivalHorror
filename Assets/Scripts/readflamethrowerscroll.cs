using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class readflamethrowerscroll : MonoBehaviour {

	public GameObject player, flameThrowerScroll, textBox;
	public float distance, angle;
    public AudioSource efxSource;

    // Update is called once per frame
    void Update () {
		Vector3 direction = player.transform.position - this.transform.position;
		angle = Vector3.Angle (direction, player.transform.forward);
		distance = direction.magnitude;
		if (angle >= 160 && distance <= 2 && !flameThrowerScroll.activeSelf) {
			if(Input.GetButtonDown("Action")) {
				flameThrowerScroll.SetActive (true);
                efxSource.Play();
              // StartCoroutine(displayMessage());
            }
		}
		else if (flameThrowerScroll.activeSelf && (Input.GetButtonDown("Action") || Input.anyKeyDown)) {
			flameThrowerScroll.SetActive (false);
       
		}
	}

    //IEnumerator displayMessage()
   // {
       // textBox.GetComponent<Text>().text = "Gas source /5";
      //  yield return new WaitForSecondsRealtime(3.0f);
      //  textBox.GetComponent<Text>().text = "";
    //}

}
