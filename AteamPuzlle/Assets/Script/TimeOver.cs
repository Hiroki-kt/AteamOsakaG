using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeOver : MonoBehaviour {

    void Start()
    {
        //Debug.Log("OK");
    }

    void Update () 
    {
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("OK2");
            SceneManager.LoadScene("TitleScene");
        }
        //else { Debug.Log("NG"); }

    }
}
