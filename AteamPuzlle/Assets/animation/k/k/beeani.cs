using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeani : MonoBehaviour {

    Animator animator;
   
    public static float i = 0;

    public  static float beea = 0;
	// Use this for initialization
	void Start () {
        beea = 0;
        animator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (i== 1)
            {
                 animator .SetTrigger("bee");
            i = 0;
            }
	}
    public static void bee()
    {
        Debug.Log(beea);
        i = 1;
       
        
        //animator.SetTrigger("Jump");
       
    }
}
