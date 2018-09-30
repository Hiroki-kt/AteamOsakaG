using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kani : MonoBehaviour {

    Animator animator;
   
    public static float i = 0;

    public  static float kanim = 0;
	// Use this for initialization
	void Start () {
        kanim = 0;
        animator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (i== 1)
            {
                 animator .SetTrigger("Speed");
            i = 2;
            

            Debug.Log(i);
        }/*if (i == 2)
            {
                animator.ResetTrigger("Speed");
            }*/
         i = 0;
	}
    public static void kuwagata()
    {
        Debug.Log(i);
        i = 1;
       
        
        //animator.SetTrigger("Jump");
       
    }
}
