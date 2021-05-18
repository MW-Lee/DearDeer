////////////////////////////////////////////
//
// MonsterAState_Move
//
// Monster_A의 이동 상태에서 작동하는 스크립트
// 20. 10. 20
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAState_Move : IState
{
    #region 변수

    private GameObject m_monsterGO;
    private Monster_A m_monster;
    private Rigidbody2D m_rigidbody;

    private Vector2 m_vMove;
    private float fSpeed;
    private float fTime;
    private float fMoveMaxTime;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public MonsterAState_Move(GameObject _input)
    {
        m_monsterGO = _input;
        m_monster = m_monsterGO.GetComponent<Monster_A>();
        m_rigidbody = m_monsterGO.GetComponent<Rigidbody2D>();

        m_vMove = new Vector2();
        fMoveMaxTime = m_monster.fMoveCoolTime;
    }

    public void OperatorEnter()
    {
        m_monster._animator.SetBool("Move", true);

        if (m_monster.iDir == 1)
            m_monsterGO.GetComponent<SpriteRenderer>().flipX = true;
        else
            m_monsterGO.GetComponent<SpriteRenderer>().flipX = false;

        fTime = 0;
        fSpeed = m_monster.fSpeed;
        fMoveMaxTime = m_monster.fMoveCoolTime;
    }

    public void OperatorUpdate()
    {
        fTime += Time.deltaTime;

        m_vMove.x = m_monster.iDir * fSpeed;
        m_vMove.y = 0;

        m_rigidbody.velocity = m_vMove;

        if (fTime >= fMoveMaxTime)
            m_monster.FSM.SetState("idle");
    }

    public void OperatorExit()
    {
        m_monster._animator.SetBool("Move", false);

        if (m_monster.iDir == 1)
            m_monster.iDir = -1;
        else
            m_monster.iDir = 1;

        fTime = 0;
        m_rigidbody.velocity = Vector2.zero;
    }

    #endregion
}
