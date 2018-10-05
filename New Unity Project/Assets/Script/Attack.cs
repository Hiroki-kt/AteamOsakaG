using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour {

	[SerializeField]
	private Board board;
	[SerializeField]
	private Manticore manticore;
	private GameManager gamemanager;

	public void Onclick(){
		Debug.Log("OK");
	}

}
