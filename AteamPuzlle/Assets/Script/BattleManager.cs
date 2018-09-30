using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

    public CharaState chara;
    public CharaState no2;
    public CharaState no3;
    public CharaState no4;
    public EnemyState enemy;
    public static float EnemyHp;
    public static float PlayerHp;
    public static float No2Hp;
    public static float No3Hp;
    public static float No4Hp;

    public enum BattleState
    {
        PlayerSet,
        PlayerBattle,
        PlayerDie,
        PlayerWin,
    }

    private BattleState currentState;
    private float gametime = GameManager.GameTime;
    private float PlayerInterval;
    private float No2Interval;
    private float No3Interval;
    private float No4Interval;
    private float EnemyInterval;
    private float EnemyBigInterval;
    private float SkillGage;
    //private No1 no1;
    //private Manticore manticore;
    private float EnemyATK;
    private float PlayerATK;
    private float EnemySPD;
    private float PlayerSPD;
    private float EnemyDEF;
    private float PlayerDEF;
    private float EnemyBigSPD;
    private float PlayerSKL;
    private float No2ATK;
    private float No3ATK;
    private float No4ATK;
    private float No2SPD;
    private float No3SPD;
    private float No4SPD;
    private float No2DEF;
    private float No3DEF;
    private float No4DEF;
    private float No2SKL;
    private float No3SKL;
    private float No4SKL;
    public static float attackanim;
    public GameObject prefab;

    // Use this for initialization
    void Start () 
    {
        //Debug.Log("BattleStart");
        // ステータス初期化
        EnemyHp = enemy.enemy.baseHp;
        PlayerHp = chara.chara.baseHp;
        EnemyATK = enemy.enemy.baseATK;
        PlayerATK = chara.chara.baseATK;
        EnemyDEF = enemy.enemy.baseDEF;
        PlayerDEF = chara.chara.baseDEF;
        EnemySPD = enemy.enemy.baseSPD;
        PlayerSPD = chara.chara.baseSPD;
        EnemyBigSPD = enemy.enemy.baseSKL;
        PlayerSKL = chara.chara.baseSKL;

        No2Hp = no2.chara.baseHp;
        No3Hp = no3.chara.baseHp;
        No4Hp = no4.chara.baseHp;
        No2ATK = no2.chara.baseATK;
        No3ATK = no3.chara.baseATK;
        No4ATK = no4.chara.baseATK;
        No2DEF = no2.chara.baseDEF;
        No3DEF = no3.chara.baseDEF;
        No4DEF = no4.chara.baseDEF;
        No2SPD = no2.chara.baseSPD;
        No3SPD = no3.chara.baseSPD;
        No4SPD = no4.chara.baseSPD;
        No2SKL = no2.chara.baseSKL;
        No3SKL = no3.chara.baseSKL;
        No4SKL = no4.chara.baseSKL;

        PlayerInterval = 0;
        EnemyInterval = 0;
        No2Interval = 0;
        No3Interval = 0;
        No4Interval = 0;
        SkillGage = 0;

        //EnemyHp = manticore.maxHp;
        //PlayerHp = no1.MaxHp;
        currentState = BattleState.PlayerSet;
	}
	
	// Update is called once per frame
	void Update () 
    {
        PlayerInterval += Time.deltaTime;
        EnemyInterval += Time.deltaTime;
        EnemyBigInterval += Time.deltaTime;
        No2Interval += Time.deltaTime;
        No3Interval += Time.deltaTime;
        No4Interval += Time.deltaTime;
        //Debug.Log(Interval);

        switch (currentState)
        {
            case BattleState.PlayerSet:
                PlayerSet();
                break;
            case BattleState.PlayerBattle:
                PlayerBattle();
                break;
            case BattleState.PlayerWin:
                PlayerWin();
                break;
            case BattleState.PlayerDie:
                PlayerDie();
                break;
            default:
                break;

        }

    }

    //-------------------
    // Private Function
    //-------------------
    private void PlayerSet()
    {
        currentState = BattleState.PlayerBattle;
    }

    private void PlayerBattle()
    {
        if(PlayerHp > 0 && EnemyHp >0)
        {
            //PlayerATK
            if(PlayerInterval > PlayerSPD){
                PlayerAttack();
                PlayerInterval = 0;
            }
            else if (EnemyInterval > EnemySPD){
                EnemyAttack();
               
            }
            if (No2Interval >= No2SPD)
            {
                No2Attack();
                Debug.Log("Hello");
                No2Interval = 0;
            }
            else if (No3Interval > No3SPD)
            {
                No3Attack();
                //No3Interval = 0;
            }
            else if (No4Interval > No4SPD)
            {
                No4Attack();
                //No4Interval = 0;
            }
            else if(EnemyBigInterval > EnemyBigSPD){
                EnemyBigAttack();
                EnemyBigInterval = 0;
            }
            else if(SkillGage > PlayerSKL){
                PlayerSkill();
                SkillGage = 0;
            }
        }
        else if(PlayerHp < 0)
        {
            currentState = BattleState.PlayerDie;
        }
        else if(EnemyHp < 0)
        {
            currentState = BattleState.PlayerWin;
        }
    }

    private void PlayerWin()
    {
        SceneManager.LoadScene("ResultSceneGood");
    }

    private void PlayerDie()
    {
        SceneManager.LoadScene("ResultSceneBud");
    }

    // 変更する場所------------------------------
    // 此処から先がそれぞれのキャラ（敵も含む）の攻撃 -----------------
    // No1
    private void PlayerAttack()
    {
        var Damage = PlayerATK - EnemyDEF;
        EnemyHp -= Damage;
        //Debug.Log("Player attack");
        //Debug.Log(EnemyHp);
    }
    
    private void PlayerSkill(){}

    // 敵
    private void EnemyAttack()
    {
        var Damage = EnemyATK - PlayerDEF;
        if (EnemyInterval >= EnemySPD+0.7)
       {
            PlayerHp -= Damage;
            EnemyInterval = 0;
        }
        enemyani.ene();
        //Debug.Log("Enemy attack");
        //Debug.Log(PlayerHp);
        //Debug.Log(Damage);
    }
    private void EnemyBigAttack()
    {
        var Damage = 1.5f * EnemyATK - PlayerDEF;
        PlayerHp -= Damage;
        //Debug.Log("Enemy Big attack");
        //Debug.Log(PlayerHp);
        //Debug.Log(Damage);
    }

    // NO2
    private void No2Attack()
    {
        var Damage = No2ATK - EnemyDEF;
        if (No2Interval >= No2SPD+1) { 
        EnemyHp -= Damage;
        }
        Instantiate(prefab);
        //Debug.Log("No2 attack");
        //Debug.Log(EnemyHp);
    }

    //No3
    private void No3Attack()
    {
        var Damage = No3ATK - EnemyDEF;
        if (No3Interval >= No3SPD + 0.8)
        {
            EnemyHp -= Damage;
            No3Interval = 0;
        }
        
        beeani.beea = 1f;
        beeani.bee();
        beeani .beea = 0f;
        //Debug.Log("No3 attack");
        //Debug.Log(EnemyHp);
    }
    //No4
    private void No4Attack()
    {
        var Damage = No4ATK - EnemyDEF;
        if (No4Interval >= No4SPD + 1)
        {
            EnemyHp -= Damage;
            No4Interval = 0;
        }
        //kani.kanim=1f;
        kani.kuwagata();
       // kani.kanim = 0f;
        //Debug.Log("No4 attack");
        //Debug.Log(EnemyHp);
    }
}
