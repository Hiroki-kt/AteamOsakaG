using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaState : MonoBehaviour {

    public BaseCharaStatus chara;
    public HPStatusUIPlayer hPStatusUI;

    private float curHp;
    private string name;

	void Start () {
        curHp = chara.baseHp;
        name = chara.name;
	}
	
	void Update () {
        switch(name){
            case "No1":
                chara.curHp = BattleManager.PlayerHp;
                curHp = BattleManager.PlayerHp;
                hPStatusUI.UpdateHPValue(curHp);
                break;
            case "No2":
                chara.curHp = BattleManager.No2Hp;
                curHp = BattleManager.No2Hp;
                hPStatusUI.UpdateHPValue(curHp);
                break;
            case "No3":
                chara.curHp = BattleManager.No3Hp;
                curHp = BattleManager.No3Hp;
                hPStatusUI.UpdateHPValue(curHp);
                break;
            case "No4":
                chara.curHp = BattleManager.No4Hp;
                curHp = BattleManager.No4Hp;
                hPStatusUI.UpdateHPValue(curHp);
                break;
        }
    }
}
