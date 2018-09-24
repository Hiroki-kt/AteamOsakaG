using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

    public BaseEnemyStatus enemy;
    public HPStatusUI hPStatusUI;

    private float curHp;

	void Start () {
        curHp = enemy.baseHp;
	}
	
	void Update () {
        enemy.curHp = BattleManager.EnemyHp;
        curHp = BattleManager.EnemyHp;
        hPStatusUI.UpdateHPValue(curHp);
	}
}
