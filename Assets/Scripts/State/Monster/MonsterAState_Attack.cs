////////////////////////////////////////////
//
// MonsterAState_Attack
//
// Monster_A의 공격 상태에서 작동하는 스크립트
// 20. 10. 20
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterAState_Attack : IState
{
    #region 변수

    private GameObject m_monsterGO;
    private Transform m_TF;
    private Monster_A m_monster;
    private Rigidbody2D m_rigidbody;

    private Vector2 m_vMove;
    private float fDest;
    private float fSpeed;
    private int iDir;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public MonsterAState_Attack(GameObject _input)
    {
        m_monsterGO = _input;
        m_monster = m_monsterGO.GetComponent<Monster_A>();
        m_TF = m_monsterGO.transform;
        m_rigidbody = m_monsterGO.GetComponent<Rigidbody2D>();

        m_vMove = new Vector2();
        fDest = 0;
        fSpeed = m_monster.fSpeed;
    }

    public void OperatorEnter()
    {
        m_monster._animator.SetBool("Attack", true);

        fDest = m_monster.vDest.x;
        iDir = m_monster.iWhereisPlayer;
    }

    public void OperatorUpdate()
    {
        m_vMove.x = iDir * fSpeed;
        m_vMove.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = m_vMove;

        // 목적지점이 몬스터의 왼쪽에 있을 때
        if(iDir == -1)
        {
            m_monsterGO.GetComponent<SpriteRenderer>().flipX = false;
            if (m_TF.position.x <= fDest)
                m_monster.FSM.SetState("idle");
        }
        // 목적지점이 몬스터의 오른쪽에 있을 때
        else if(iDir == 1)
        {
            m_monsterGO.GetComponent<SpriteRenderer>().flipX = true;
            if (m_TF.position.x >= fDest)
                m_monster.FSM.SetState("idle");
        }
    }

    public void OperatorExit()
    {
        m_monster._animator.SetBool("Attack", false);

        fDest = 0;
    }

    #endregion
}
