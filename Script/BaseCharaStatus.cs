using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseCharaStatus 
{
    public string name;

    public enum Type
    {
        Red,
        Blue,
        Yellow,
        Greem,
        Purple,
    }

    public Type CharaType;
    public float baseHp;
    public float curHp;

    public float baseATK;
    public float curATK;
    public float baseDEF;
    public float curDEF;
    public float baseSKL;
    public float curSKL;
    public float baseSPD;
    public float curSPD;
}
