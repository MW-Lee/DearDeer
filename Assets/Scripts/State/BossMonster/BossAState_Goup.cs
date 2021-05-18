////////////////////////////////////////////
//
// BossAState_Goup
//
// BossA 패턴 중 위로 올라갈 때 작동하는 스크립트
// 20. 12. 03
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_Goup : IState
{
    #region 변수

    private GameObject m_BossGO;
    private BossMonster_A m_Boss;

    private Rigidbody2D m_rigidbody;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public BossAState_Goup(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();
        m_rigidbody = m_BossGO.GetComponent<Rigidbody2D>();
    }

    public void OperatorEnter()
    {
        // 올라가는 이펙트
        m_Boss._animator.SetTrigger("GoUp");

        m_Boss._audioSource.PlayOneShot(m_Boss._sound.mapSound[3]);

        m_rigidbody.velocity = Vector2.zero;
        m_Boss.bAttack = true;
        m_rigidbody.gravityScale = 0;
    }

    public void OperatorUpdate()
    {
        if (m_Boss.bDie)
        {
            m_Boss.FSM.SetState("die");
            return;
        }
    }

    public void OperatorExit()
    {
        m_Boss._animator.ResetTrigger("GoUp");
    }

    #endregion
}
