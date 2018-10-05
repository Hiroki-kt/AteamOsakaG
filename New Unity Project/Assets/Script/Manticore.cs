using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manticore : MonoBehaviour {

    //-----------------
    // public status
    //-----------------
	//敵のMaxHP
	public int maxHp = 8000;
	//敵のHP
	public int Hp;
	//敵の攻撃力
	public int ATK = 30;
    //防御力
    public float Guard = 30;
	//攻撃間隔
    public float ATKInterval = 10;
    //大攻撃間隔
    public float BigATKInterval = 35;


	//-----------------
    // private
    //-----------------
    private HPStatusUI hpStatusUI;

    // Use this for initialization
	void Start () {
		Hp = maxHp; // 初期HPをmaxHPにする
		hpStatusUI = GetComponent<HPStatusUI>();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Hp <= 0) {
			Debug.Log ("死亡しました");
			hpStatusUI.SetDisable ();
		}
	}
	public void SetHp(int hp){
		this.Hp = hp;

		//
		//hpStatusUI.UpdateHPValue ();

		if (hp <= 0) {
			hpStatusUI.SetDisable ();
		}
	}
			
	public int GetHp(){
		return Hp;
	}
	public int GetMaxHp(){
		return maxHp;
	}
	public void GetDamage(){
		Hp = Hp - 1;
	}
}
