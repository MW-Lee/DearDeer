////////////////////////////////////////////
//
// BossAState_Fall
//
// BossA의 점프 패턴에서 작동하는 스크립트
// 20. 11. 22
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_Fall : IState
{
    #region 변수

    private GameObject m_BossGO;
    private BossMonster_A m_Boss;
    private Rigidbody2D m_rigidbody;

    private Vector2 vMove;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public BossAState_Fall(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();
        m_rigidbody = m_BossGO.GetComponent<Rigidbody2D>();
    }

    public void OperatorEnter()
    {
        // 낙하 애니메이션 재생
        m_Boss._animator.SetBool("Fall", true);
    }

    public void OperatorUpdate()
    {
        vMove = Vector2.zero;
        vMove.x = m_Boss.iDir * m_Boss.fSpeed;
        vMove.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = vMove;


        if (m_Boss.bOnGround)
        {
            m_Boss.FSM.SetState("move");
        }
    }

    public void OperatorExit()
    {
        m_Boss._animator.SetBool("Fall", false);
    }

    #endregion
}
