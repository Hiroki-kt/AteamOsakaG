using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class moplayer : MonoBehaviour {

    public VideoPlayer mPlayer;
    public static int m = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m==1) {
            mPlayer.time = 0f;

            mPlayer.Play();
        }
	}
    public static void kvido()
    {
        m = 1;
       // Debug.Log(m+"m");
    }
}
