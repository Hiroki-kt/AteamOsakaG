using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;

public class movieplayer : MonoBehaviour {

    public VideoPlayer mPlayer;
    public static int mi = 0;
    public static int me = 0;
    public GameObject vip;
    public CharaState No4;
    public EnemyState Enemy;

    public AudioSource audioSource;
    public AudioClip sound01;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mi==1) {
            vip.SetActive(true);
            mPlayer.time = 1f;
            
            mPlayer.Play();
            audioSource.PlayOneShot(sound01);

            mi = 0;
             Debug.Log(mi+"mi");

        }
        if (me == 1)
        {
            vip.SetActive(false);
            me = 0;
            var Damage = 3 * (No4.chara.baseATK + BattleManager.No4ATKc) - Enemy.enemy.baseDEF;
            BattleManager.EnemyHp -= Damage;
           
        }
	}
    public static IEnumerator kvido(Action endCallBack)
    {
        mi = 1;
        Debug.Log(mi + "m");
        yield return new WaitForSeconds(5f);
        endCallBack();

    }
    public static void kkesu()
    {
        me = 1;
    }
}
