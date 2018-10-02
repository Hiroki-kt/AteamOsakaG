using UnityEngine;
using System.Collections;

public class test6 : MonoBehaviour
{
    private float nextTime;
    public float interval = 1.0f;// 点滅周期'
    //public float test;

    // Use this for initialization
    void Start()
    {
        nextTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;

            nextTime += interval;
        }
    }
}
