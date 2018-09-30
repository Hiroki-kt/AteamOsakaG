using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class button_click : MonoBehaviour {

	public AudioClip clickSound;
	private AudioSource clickSource;

	// Use this for initialization
	void Start () {
		clickSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		

        
		//Debug.Log("click");
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			clickSource.PlayOneShot (clickSound);
		}

		if(Input.GetMouseButtonDown(0)){
				clickSource.PlayOneShot (clickSound);
			}

	}
}