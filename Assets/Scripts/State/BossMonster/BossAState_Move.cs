////////////////////////////////////////////
//
// BossAState_Move
//
// BossA의 기본 움직임 상태에서 작동하는 스크립트
// 20. 11. 22
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_Move : IState
{
    #region 변수

    private GameObject m_BossGO;
    private BossMonster_A m_Boss;
    private Rigidbody2D m_rigidbody;
    private Boss_GroundCheck m_groudCheck;

    private Vector2 vMove;
    private float fTime;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public BossAState_Move(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();
        m_rigidbody = m_BossGO.GetComponent<Rigidbody2D>();
        m_groudCheck = m_BossGO.transform.Find("GroundCheck").GetComponent<Boss_GroundCheck>();

        vMove = new Vector2();
    }

    public void OperatorEnter()
    {
        // 달리기 애니메이션 재생

        fTime = 0;
    }

    public void OperatorUpdate()
    {
        if (!m_groudCheck._audioSource.isPlaying)
            m_groudCheck._audioSource.PlayOneShot(m_groudCheck._audioSource.clip);

        vMove = Vector2.zero;
        vMove.x = m_Boss.iDir * m_Boss.fSpeed;
        vMove.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = vMove;


        fTime += Time.deltaTime;
        if(fTime >= m_Boss.fAttackCoolTime)
        {
            m_Boss.FSM.SetState("attack");
        }
    }

    public void OperatorExit()
    {
        vMove = Vector2.zero;
        fTime = 0;
    }

    #endregion
}
