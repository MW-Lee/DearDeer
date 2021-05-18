////////////////////////////////////////////
//
// MonsterAState_Idle
//
// Monster_A의 기본 상태에서 작동하는 스크립트
// 20. 10. 20
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAState_Idle : IState
{
    #region 변수

    private GameObject m_monsterGO;
    private Monster_A m_monster;
    private Rigidbody2D m_rigidbody;

    private float fTime;
    private float fMoveMaxTime;
    private float fAtkMaxTime;

    private bool bAttack;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public MonsterAState_Idle(GameObject _input)
    {
        m_monsterGO = _input;
        m_monster = m_monsterGO.GetComponent<Monster_A>();
        m_rigidbody = m_monsterGO.GetComponent<Rigidbody2D>();

        fTime = 0;
        fMoveMaxTime = m_monster.fMoveCoolTime;
        fAtkMaxTime = m_monster.fAttackCoolTime;
    }

    public void OperatorEnter()
    {
        // idle 애니메이션 재생
        

        m_rigidbody.velocity = Vector2.zero;
        bAttack = m_monster.bAttack;

        fMoveMaxTime = m_monster.fMoveCoolTime;
        fAtkMaxTime = m_monster.fAttackCoolTime;
    }

    public void OperatorUpdate()
    {
        fTime += Time.deltaTime;

        if (!bAttack)
        {
            // 공격 중이 아닐 때
            if (fTime >= fMoveMaxTime)
                m_monster.FSM.SetState("move");
        }
        else
        {
            // 공격 중일 때
            if (fTime >= fAtkMaxTime)
                m_monster.FSM.SetState("attack");
        }
    }

    public void OperatorExit()
    {
        fTime = 0;
        bAttack = false;
    }

    #endregion
}
