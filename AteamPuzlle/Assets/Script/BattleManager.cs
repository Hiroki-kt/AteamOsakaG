using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

    public CharaState chara;
    public CharaState no2;
    public CharaState no3;
    public CharaState no4;
    public EnemyState enemy;
    public GameObject OhakaPrefab;
    public GameObject no1obj;
    public GameObject no2obj;
    public GameObject no3obj;
    public GameObject no4obj;
    public GameObject enemyobj;
    public Text No1DamageText;
    public Text No2DamageText;
    public Text No3DamageText;
    public Text No4DamageText;
    public Text EnemyDamageText;
    public Image No1ActionImage;
    public Image No2ActionImage;
    public Image No3ActionImage;
    public Image No4ActionImage;
    public Image EnemyActionImage;

    [SerializeField]
    private Text _textCountdown;
    [SerializeField]
    private Image _imageMask;
    public static float EnemyHp;
    public static float PlayerHp;
    public static float No2Hp;
    public static float No3Hp;
    public static float No4Hp;

    public static float PlayerATKc;
    public static float No2ATKc;
    public static float No3ATKc;
    public static float No4ATKc;

    public static float PlayerDEFc;
    public static float No2DEFc;
    public static float No3DEFc;
    public static float No4DEFc;

    public static float PlayerSKLc;
    public static float No2SKLc;
    public static float No3SKLc;
    public static float No4SKLc;
    

    public enum BattleState
    {
        PlayerSet,
        PlayerBattle,
        PlayerDie,
        PlayerWin,
        Wait,
    }

    private BattleState currentState;
    //private float gametime = GameManager.GameTime;
    private float PlayerInterval;
    private float No2Interval;
    private float No3Interval;
    private float No4Interval;
    private float EnemyInterval;
    private float EnemyBigInterval;
    private float No1SkillGage;
    private float No2SkillGage;
    private float No3SkillGage;
    private float No4SkillGage;
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
    private bool PlayerisDead = false;
    private bool No2isDead = false;
    private bool No3isDead = false;
    private bool No4isDead = false;
    private bool partyisDead = false;
    public static float attackanim;
    public GameObject prefab;
    public GameObject prefab2;
    private int No1diecount = 0;
    private int No2diecount = 0;
    private int No3diecount = 0;
    private int No4diecount = 0;
    private bool Flash = true;
    private GameManager gameManager;
    private int e=0;

    // Use this for initialization
    void Start()
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

        PlayerATKc = 0;
        No2ATKc = 0;
        No3ATKc = 0;
        No4ATKc = 0;

        PlayerDEFc = 0;
        No2DEFc = 0;
        No3DEFc = 0;
        No4DEFc = 0;

        PlayerSKLc = 0;
        No2SKLc = 0;
        No3SKLc = 0;
        No4SKLc = 0;

        No1SkillGage = 0;
        No2SkillGage = 0;
        No3SkillGage = 0;
        No4SkillGage = 0;

        //EnemyHp = manticore.maxHp;
        //PlayerHp = no1.MaxHp;
        currentState = BattleState.PlayerSet;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInterval += Time.deltaTime;
        EnemyInterval += Time.deltaTime;
        EnemyBigInterval += Time.deltaTime;
        No2Interval += Time.deltaTime;
        No3Interval += Time.deltaTime;
        No4Interval += Time.deltaTime;
        No1SkillGage += Time.deltaTime;
        No2SkillGage += Time.deltaTime;
        No3SkillGage += Time.deltaTime;
        No4SkillGage += Time.deltaTime;
        //Debug.Log(Interval);

        switch (currentState)
        {
            case BattleState.PlayerSet:
                StartCoroutine(PlayerSet());
                break;
            case BattleState.PlayerBattle:
                PlayerBattle();
                Debug.Log("battle");
                break;
            case BattleState.PlayerWin:
                StartCoroutine(PlayerWin(() => GameClear()));
                break;
            case BattleState.PlayerDie:
                StartCoroutine(PlayerDie(() => GameOver()));
                break;
            case BattleState.Wait:
                Debug.Log("wait");
                break;
            default:
                break;

        }

    }

    //-------------------
    // Private Function
    //-------------------
    private IEnumerator PlayerSet()
    {
        yield return new WaitForSeconds(0.5f);
        currentState = BattleState.PlayerBattle;
    }

    private void PlayerBattle()
    {
        //Debug.Log("OK1");
        if (PlayerHp < 0 && No1diecount == 0)
        {
            PlayerisDead = true;
            //var no1obj = GameObject.Find("No1");
            Instantiate(OhakaPrefab, no1obj.transform.position, Quaternion.identity);
            no1obj.SetActive(false);
            No1diecount = 1;
            Debug.Log("No1Die");
        }
        if (No2Hp < 0 && No2diecount == 0)
        {
            No2isDead = true;
            //var no2obj = GameObject.Find("No2");
            Instantiate(OhakaPrefab, no2obj.transform.position, Quaternion.identity);
            no2obj.SetActive(false);
            No2diecount = 1;
            Debug.Log("No2Die");
        }
        if (No3Hp < 0 && No3diecount == 0)
        {
            No3isDead = true;
            //var no3obj = GameObject.Find("No3");
            Instantiate(OhakaPrefab, no3obj.transform.position, Quaternion.identity);
            no3obj.SetActive(false);
            No3diecount = 1;
            Debug.Log("No3Die");
            Instantiate(prefab2);
        }
        if (No4Hp < 0 && No4diecount == 0)
        {
            No4isDead = true;
            //var no4obj = GameObject.Find("No4");
            Instantiate(OhakaPrefab, no4obj.transform.position, Quaternion.identity);
            no4obj.SetActive(false);
            No4diecount = 1;
            Debug.Log("No4Die");
        }
        if (PlayerisDead && No2isDead && No3isDead && No4isDead)
        {
            partyisDead = true;
            Debug.Log("All Die");
        }
        //Debug.Log("OK2");
        //Debug.Log(partyisDead);
        //Debug.Log(EnemyHp);

        if (partyisDead == false && EnemyHp > 0)
        {
            //Debug.Log("OK");
            //PlayerATK
            if (PlayerInterval > PlayerSPD && PlayerisDead == false)
            {
                PlayerAttack();
                PlayerInterval = 0;
            }
            if (EnemyInterval > EnemySPD)
            {
                EnemyAttack();

            }
            if (No2Interval >= No2SPD && No2isDead == false)
            {
                No2Attack();
                //Debug.Log("Hello");
                No2Interval = 0;
            }
            if (No3Interval > No3SPD && No3isDead == false)
            {
                No3Attack();
                //No3Interval = 0;
            }
            if (No4Interval > No4SPD && No4isDead == false)
            {
                No4Attack();
                //No4Interval = 0;
            }
            if (EnemyBigInterval > EnemyBigSPD)
            {
                EnemyBigAttack();
                EnemyBigInterval = 0;
            }
            if (No1SkillGage > PlayerSKL - PlayerSKLc && PlayerisDead == false)
            {
                Debug.Log("No1SKILL");
                PlayerSkill();
                No1SkillGage = 0;
            }
            if (No2SkillGage > No2SKL - No2SKLc && No2isDead == false)
            {
                Debug.Log("No2SKILL");
                No2Skill();
                No2SkillGage = 0;
            }
            if (No3SkillGage > No3SKL - No3SKLc && No3isDead == false)
            {
                Debug.Log("No3SKILL");
                No3Skill();
                No3SkillGage = 0;
            }
            if (No4SkillGage >= No4SKL - No4SKLc && No4isDead == false)
            {
                Debug.Log("No4SKILL");

                for ( int k=0 ; e == 0; e++)
                {
                    No4Skill();
                }
                if (No4SkillGage > No4SKL-No4SKLc +4)
                {
                    No4SkillGage = 0;
                    Debug.Log(No4SkillGage+"No4SkillGage");
                    movieplayer.kkesu();
                    e = 0;
                }
            }
        }
        else if (partyisDead)
        {
            currentState = BattleState.PlayerDie;
        }
        else if (EnemyHp <= 0)
        {
            currentState = BattleState.PlayerWin;
        }
    }

    private IEnumerator PlayerWin(Action endCallBack)
    {
        yield return new WaitForSeconds(2f);
        endCallBack();
    }

    private IEnumerator PlayerDie(Action endCallBack)
    {
        yield return new WaitForSeconds(2f);
        endCallBack();
    }

    /// <summary>
    /// Chara attack.
    /// </summary>

    // No1(攻撃なし)
    private void PlayerAttack()
    {
        var Damage = PlayerATK + PlayerATKc - EnemyDEF;
        EnemyHp -= Damage;
        currentState = BattleState.Wait;
        StartCoroutine(SetDamageEffect(enemyobj, EnemyDamageText, Damage, EnemyActionImage, () => currentState = BattleState.PlayerBattle));
        //Debug.Log(Damage);
        //Debug.Log("Player attack");
        //Debug.Log(EnemyHp);
    }

    //No2
    private void No2Attack()
    {
        var Damage = No2ATK + No2ATKc - EnemyDEF;
        if (No2Interval >= No2SPD)
        {
            EnemyHp -= Damage;
            currentState = BattleState.Wait;
            StartCoroutine(SetDamageEffect(enemyobj, EnemyDamageText, Damage, EnemyActionImage, () => currentState = BattleState.PlayerBattle));
            //float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //StartCoroutine(Flashing());
            No3Interval = 0;
            //Debug.Log(Damage);
        }
        Instantiate(prefab);
        //Debug.Log("No2 attack");
        //Debug.Log(EnemyHp);
    }

    //No3
    private void No3Attack()
    {
        var Damage = No3ATK + No3ATKc - EnemyDEF;
        if (No3Interval >= No3SPD + 0.8)
        {
            EnemyHp -= Damage;
            currentState = BattleState.Wait;
            StartCoroutine(SetDamageEffect(enemyobj, EnemyDamageText, Damage, EnemyActionImage, () => currentState = BattleState.PlayerBattle));
            //float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //StartCoroutine(Flashing());
            No3Interval = 0;
            //Debug.Log(Damage);
        }

        beeani.beea = 1f;
        beeani.bee();
        beeani.beea = 0f;
        //Debug.Log("No3 attack");
        //Debug.Log(EnemyHp);
    }

    //No4
    private void No4Attack()
    {
        var Damage = No4ATK + No4ATKc - EnemyDEF;
        if (No4Interval >= No4SPD + 1)
        {
            EnemyHp -= Damage;
            No4Interval = 0;
            currentState = BattleState.Wait;
            StartCoroutine(SetDamageEffect(enemyobj, EnemyDamageText, Damage, EnemyActionImage, () => currentState = BattleState.PlayerBattle));
            //var enemyobj = GameObject.Find("manticore");
            //float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //enemyobj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
            //Debug.Log(Damage);
        }
        //kani.kanim=1f;
        kani.kuwagata();
        // kani.kanim = 0f;
        //Debug.Log("No4 attack");
        //Debug.Log(EnemyHp);
    }

    /// <summary>
    /// Enemies the attack.
    /// </summary>

    // 敵小攻撃
    // 最前線の敵単体
    private void EnemyAttack()
    {
        var attacktarget = Setattacktarget();
        var Damage = EnemyATK - (PlayerDEF + PlayerDEFc);
        if (EnemyInterval >= EnemySPD + 0.7)
        {
            switch (attacktarget)
            {
                case 1:
                    PlayerHp -= Damage;
                    var level = Mathf.Abs(Mathf.Sin(Time.time * 10));
                    currentState = BattleState.Wait;
                    StartCoroutine(SetDamageEffect(no1obj, No1DamageText, Damage, No1ActionImage, () => currentState = BattleState.PlayerBattle));
                    //var no1obj = GameObject.Find("No1");
                    //no1obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
                    EnemyInterval = 0;
                    //Debug.Log(Damage);
                    break;
                case 2:
                    No2Hp -= Damage;
                    var level2 = Mathf.Abs(Mathf.Sin(Time.time * 10));
                    currentState = BattleState.Wait;
                    StartCoroutine(SetDamageEffect(no2obj, No2DamageText, Damage, No2ActionImage, () => currentState = BattleState.PlayerBattle));
                    //no2obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level2);
                    EnemyInterval = 0;
                    //Debug.Log(Damage);
                    break;
                case 3:
                    No3Hp -= Damage;
                    float level3 = Mathf.Abs(Mathf.Sin(Time.time * 10));
                    currentState = BattleState.Wait;
                    StartCoroutine(SetDamageEffect(no3obj, No3DamageText, Damage, No3ActionImage, () => currentState = BattleState.PlayerBattle));
                    //No3obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level3);
                    EnemyInterval = 0;
                    //Debug.Log(Damage);
                    break;
                case 4:
                    No4Hp -= Damage;
                    float level4 = Mathf.Abs(Mathf.Sin(Time.time * 10));
                    currentState = BattleState.Wait;
                    StartCoroutine(SetDamageEffect(no4obj, No4DamageText, Damage, No4ActionImage, () => currentState = BattleState.PlayerBattle));
                    //No4obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level4);
                    EnemyInterval = 0;
                    //Debug.Log(Damage);
                    break;
            }
        }
        enemyani.ene();
        //Debug.Log("Enemy attack");
        //Debug.Log(PlayerHp);
        //Debug.Log(Damage);
    }

    // 敵大攻撃
    // 最前線の敵単体
    private void EnemyBigAttack()
    {
        var attacktarget = Setattacktarget();
        var Damage = 1.5f * EnemyATK - (PlayerDEF + PlayerDEFc);
        switch (attacktarget)
        {
            case 1:
                PlayerHp -= Damage;
                float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
                currentState = BattleState.Wait;
                StartCoroutine(SetDamageEffect(no1obj, No1DamageText, Damage, No1ActionImage, () => currentState = BattleState.PlayerBattle));
                //no1obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
                //Debug.Log(Damage);
                break;
            case 2:
                No2Hp -= Damage;
                float level2 = Mathf.Abs(Mathf.Sin(Time.time * 10));
                currentState = BattleState.Wait;
                StartCoroutine(SetDamageEffect(no2obj, No2DamageText, Damage, No2ActionImage, () => currentState = BattleState.PlayerBattle));
                //no2obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level2);
                //Debug.Log(Damage);
                break;
            case 3:
                No3Hp -= Damage;
                float level3 = Mathf.Abs(Mathf.Sin(Time.time * 10));
                currentState = BattleState.Wait;
                StartCoroutine(SetDamageEffect(no3obj, No3DamageText, Damage, No3ActionImage, () => currentState = BattleState.PlayerBattle));
                //no3obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level3);
                //Debug.Log(Damage);
                break;
            case 4:
                No4Hp -= Damage;
                float level4 = Mathf.Abs(Mathf.Sin(Time.time * 10));
                currentState = BattleState.Wait;
                StartCoroutine(SetDamageEffect(no4obj, No4DamageText, Damage, No4ActionImage, () => currentState = BattleState.PlayerBattle));
                //no4obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level4);
                //Debug.Log(Damage);
                break;
        }
        enemyani.ene();
        Debug.Log("Enemy Big attack");
        //Debug.Log(PlayerHp);
        //Debug.Log(Damage);
    }

    /// <summary>
    /// Skill.
    /// </summary>

    // No1
    private void PlayerSkill(){}

    // NO2 スキル回復するよ
    private void No2Skill()
    {
        //Debug.Log("OK0");
        //var Damage = No2ATK + No2ATKc - EnemyDEF;
        var cure = 500;
        if (No2SkillGage >= No2SKL - No2SKLc)
        {
            //Debug.Log("OK1");
            if(PlayerisDead == false)
            {
                PlayerHp += cure;
                //Debug.Log("No1cure");
                if (PlayerHp > chara.chara.baseHp)
                {
                    PlayerHp = chara.chara.baseHp;
                }
            }
            if (No2isDead == false)
            {
                No2Hp += cure;
                //Debug.Log("No2cure");
                if (No2Hp > no2.chara.baseHp)
                {
                    No2Hp = no2.chara.baseHp;
                }
            }
            if (No3isDead == false)
            {
                No3Hp += cure;
                //Debug.Log("No3cure");
                if (No3Hp > no3.chara.baseHp)
                {
                    No3Hp = no3.chara.baseHp;
                }
            }
            if (No4isDead == false)
            {
                No4Hp += cure;
                //Debug.Log("No4cure");
                if (No4Hp > no4.chara.baseHp)
                {
                    No4Hp = no4.chara.baseHp;
                }
            }
            No2SkillGage = 0;
            //Debug.Log(Damage);
        }
        Instantiate(prefab);
        //Debug.Log("No2 attack");
        //Debug.Log(EnemyHp);
    }

    // No3 
    private void No3Skill(){}

    // No4
    private void No4Skill(){
        movieplayer.kvido();
        var Damage = 3 * (No4ATK + No4ATKc) - EnemyDEF;
        currentState = BattleState.Wait;
        StartCoroutine(SetDamageEffect(enemyobj, EnemyDamageText, Damage, EnemyActionImage, () => currentState = BattleState.PlayerBattle));
    }

    /// <summary>
    /// 敵の攻撃目標を索敵.
    /// </summary>

    // 敵の攻撃目標
    private int Setattacktarget()
    {
        if (PlayerisDead && No4isDead && No3isDead)
        {
            return 2;
        }
        else if (PlayerisDead && No4isDead)
        {
            return 3;
        }
        else if (PlayerisDead)
        {
            return 4;
        }
        else
        {
            return 1;
        }
    }

    // パーティー全滅
    private void PartyisDead()
    {
    }

    // 点滅→使っていない
    private IEnumerator Flashing()
    {
        //var enemyobj = GameObject.Find("manticore");
        while (Flash)
        {
            yield return new WaitForSeconds(1.0f);
            enemyobj.GetComponent<SpriteRenderer>().enabled = !enemyobj.GetComponent<SpriteRenderer>().enabled;
        }
    }


    // GameOver
    // コンテニューorリタイア追加したい
    private void GameOver()
    {
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "GameOver";

        if (Input.GetMouseButtonDown(0))
        {
            _textCountdown.gameObject.SetActive(false);
            _imageMask.gameObject.SetActive(false);
            SceneManager.LoadScene("ResultSceneBud");
        }

    }

    // GameClear
    // 花吹雪とかおめでたい感じを追加したい
    private void GameClear()
    {
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "GameClear";

        if (Input.GetMouseButtonDown(0))
        {
            _textCountdown.gameObject.SetActive(false);
            _imageMask.gameObject.SetActive(false);
            SceneManager.LoadScene("ResultSceneGood");

        }
    }

    private IEnumerator SetDamageEffect(GameObject Object, Text text, float Damage,Image image, Action endCallBack){
        Object.gameObject.SetActive(false);
        text.text = ((int)Damage).ToString() + "ダメ";
        image.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Object.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        endCallBack();
    }
}