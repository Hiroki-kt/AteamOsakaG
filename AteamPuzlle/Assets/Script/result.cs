using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class result : MonoBehaviour {

	void Start () {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
		
	}

	void Update () {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("MypageScene");
        }
    }
}
