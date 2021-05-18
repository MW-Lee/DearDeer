////////////////////////////////////////////
//
// BossAState_Jump
//
// BossA의 점프 패턴에서 작동하는 스크립트
// 20. 11. 22
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_Jump : IState
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

    public BossAState_Jump(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();
        m_rigidbody = m_BossGO.GetComponent<Rigidbody2D>();

        vMove = new Vector2();
    }

    public void OperatorEnter()
    {
        // 점프 애니메이션 재생
        m_Boss._animator.SetTrigger("Jump");

        // 오디오 재생
        m_Boss._audioSource.PlayOneShot(m_Boss._sound.mapSound[0]);

        m_rigidbody.AddForce(Vector2.up * 13, ForceMode2D.Impulse);
    }

    public void OperatorUpdate()
    {
        vMove = Vector2.zero;
        vMove.x = m_Boss.iDir * m_Boss.fSpeed;
        vMove.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = vMove;


        if (m_rigidbody.velocity.y <= 0)
        { 
            m_Boss.FSM.SetState("fall");
            return;
        }
    }

    public void OperatorExit()
    {
        m_Boss._animator.ResetTrigger("Jump");
    }

    #endregion
}
