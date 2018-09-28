using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPStatusUIPlayer : MonoBehaviour {

    //敵のステータス
    //private Manticore manticore;
    //
    private Slider hpSlider;
    private float MaxHp;
    public CharaState chara;

    // Use this for initialization
    void Start()
    {
        hpSlider = transform.Find("HPBar").GetComponent<Slider>();
        MaxHp = chara.chara.baseHp;
        hpSlider.value = MaxHp / MaxHp;
        //Debug.Log(hpSlider.value);

    }

    // Update is called once per frame
    void Update()
    {

    }
    //
    public void SetDisable()
    {
        gameObject.SetActive(false);
    }

    public void UpdateHPValue(float CurHP)
    {
        //Debug.Log("OK");
        hpSlider.value = CurHP / MaxHp;
        //Debug.Log(hpSlider.value);
    }

}
