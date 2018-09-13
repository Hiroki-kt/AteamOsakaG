using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPStatusUI : MonoBehaviour {

	//敵のステータス
	private Manticore manticore;
	//
	private Slider hpSlider;

	// Use this for initialization
	void Start () {
		//
		manticore = transform.root.GetComponent<Manticore>();
		//
		hpSlider = transform.Find("HPBar").GetComponent<Slider>();
		//
		hpSlider.value = (float)manticore.GetMaxHp()/(float)manticore.GetMaxHp();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//
	public void SetDisable(){
		gameObject.SetActive (false);
	}
	public void UpdateHPValue(){
		hpSlider.value = (float)manticore.GetHp()/(float)manticore.GetMaxHp();
	}
}
