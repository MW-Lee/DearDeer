////////////////////////////////////////////
//
// Monster
//
// Monster의 최상위 클래스
// 20. 11. 06
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public abstract void ChangeState(string _input);

    public Animator _animator;

    public int iMaxHP;
    public int iDamage;

    public float fMoveCoolTime;
    public float fAttackCoolTime;
    public float fSpeed;
    public float fAttackSpeed;

    public Vector2 vDest;

    public int iDir;
    [HideInInspector]
    public int iWhereisPlayer;
    [HideInInspector]
    public bool bAttack;
    [HideInInspector]
    public int ihit;
}
