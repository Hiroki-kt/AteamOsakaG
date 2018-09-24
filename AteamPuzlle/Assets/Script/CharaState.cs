using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaState : MonoBehaviour {

    public BaseCharaStatus chara;
    public HPStatusUIPlayer hPStatusUI;

    private float curHp;

	void Start () {
        curHp = chara.baseHp;
	}
	
	void Update () {
        chara.curHp = BattleManager.PlayerHp;
        curHp = BattleManager.PlayerHp;
        hPStatusUI.UpdateHPValue(curHp);
	}
}
