////////////////////////////////////////////
//
// BossAState_3Cut
//
// BossMoster_A에서 작동하는 스크립트
// 20. 11. 21
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_3Cut : IState
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

    public BossAState_3Cut(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();
        m_rigidbody = m_BossGO.GetComponent<Rigidbody2D>();
        m_TFEffect = m_BossGO.transform.Find("Effect");
    }

    void IState.OperatorEnter()
    {
        // 3연격 ani 재생
        m_Boss._animator.SetTrigger("3Cut");

        // 오디오 재생
        m_Boss._audioSource.PlayOneShot(m_Boss._sound.mapSound[1]);

        m_Boss.iOldDir = m_Boss.iDir;
        m_Boss.bAttack = true;
        m_Boss.iDamage = 20;

        if (m_Boss.iOldDir == 1)
            m_TFEffect.localPosition = new Vector2(0.81f, -0.23f);
        else if (m_Boss.iOldDir == -1)
            m_TFEffect.localPosition = new Vector2(-0.81f, -0.23f);
    }

    void IState.OperatorUpdate()
    {
        if(m_Boss.bDie)
        {
            m_Boss.FSM.SetState("die");
            return;
        }    
    }

    void IState.OperatorExit()
    {
        m_rigidbody.velocity = Vector2.zero;
    }

    #endregion
}
