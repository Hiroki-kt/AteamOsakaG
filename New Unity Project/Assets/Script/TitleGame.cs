using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void Click(){
        StartCoroutine(StartClick());
    }

    private IEnumerator StartClick(){
        SceneManager.LoadScene("MypageScene");
        yield return new WaitForSeconds(2f);
    }
}
