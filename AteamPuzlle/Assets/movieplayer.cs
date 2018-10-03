using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class movieplayer : MonoBehaviour {

    public VideoPlayer mPlayer;
    public static int mi = 0;
    public static int me = 0;
    public GameObject vip;
    public CharaState No4;
    public EnemyState Enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mi==1) {
            vip.SetActive(true);
            mPlayer.time = 1f;
            
            mPlayer.Play();
           
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
    public static void kvido()
    {
        mi = 1;
        Debug.Log(mi + "m");
       

    }
    public static void kkesu()
    {
        me = 1;
    }
}
