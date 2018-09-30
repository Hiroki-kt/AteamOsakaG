using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rottest : MonoBehaviour {


    //Transform target;
    float speed = 1f;
    float step;
    private Quaternion currentrot;

    void Start()
    {
        //target = GameObject.Find("Cube").transform;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {

            StartCoroutine(rot());
        }

        //他のオブジェクトと回転を同期する場合
        //step = speed * 2 * Time.deltaTime;
        //transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, step);
    }

    public IEnumerator rot(){
        //指定した方向にゆっくり回転する場合
        step = speed * Time.deltaTime;
        while(transform.rotation != Quaternion.Euler(0,0,90f)){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 90f), step);
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("OK");

    }
}
