using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyani : MonoBehaviour {

    Animator animator;
   
    private static float p = 0;

    public  static float enea = 0;
	// Use this for initialization
	void Start () {
        enea = 0;
        animator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (p== 1)
            {
            p= 0;
                 animator .SetTrigger("enemy");
            p = 0;
            }
        p= 0;
	}
    public static void ene()
    {
        //Debug.Log(enea);
        p = 1;
       
        
        //animator.SetTrigger("Jump");
       
    }
}
