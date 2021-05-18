////////////////////////////////////////////
//
// BossAState_Rush
//
// 보스의 돌진 패턴에서 작동하는 스크립트
// 20. 12. 05
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_Rush : IState
{
    #region 변수

    private GameObject m_BossGO;
    private BossMonster_A m_Boss;

    private Rigidbody2D m_rigidbody;
    private Transform m_TFEffect;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public BossAState_Rush(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();

        m_rigidbody = m_BossGO.GetComponent<Rigidbody2D>();
        m_TFEffect = m_BossGO.transform.Find("Effect");
    }

    public void OperatorEnter()
    {
        //m_rigidbody.gravityScale = 0;
        m_Boss._animator.SetTrigger("Rush");

        // 오디오 재생
        m_Boss._audioSource.PlayOneShot(m_Boss._sound.mapSound[2]);

        m_Boss.iDamage = 20;

        if (m_Boss.iOldDir == 1)
            m_TFEffect.localPosition = new Vector2(0.81f, -0.23f);
        else if (m_Boss.iOldDir == -1)
            m_TFEffect.localPosition = new Vector2(-0.81f, -0.23f);
    }

    public void OperatorUpdate()
    {
        return;
    }

    public void OperatorExit()
    {
        m_rigidbody.velocity = Vector2.zero;
        m_Boss._animator.ResetTrigger("Rush");
    }

    #endregion
}
