////////////////////////////////////////////
//
// BossAState_AttackEnd
//
// BossA의 한 패턴이 끝난 후에 작동하는 스크립트
// 20. 12. 02
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_AttackEnd : IState
{
    #region 변수

    private GameObject m_BossGO;
    private BossMonster_A m_Boss;
    private Rigidbody2D m_rigidbody;
    private Transform m_TFEffect;
    private Transform m_TFSprite;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public BossAState_AttackEnd(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();
        m_rigidbody = m_BossGO.GetComponent<Rigidbody2D>();
        m_TFEffect = m_BossGO.transform.Find("Effect");
        m_TFSprite = m_BossGO.transform.Find("Sprite");
    }

    public void OperatorEnter()
    {
        // 납도 ani 재생
        m_Boss._animator.SetTrigger("AtkEnd");

        m_rigidbody.velocity = Vector2.zero;
        m_rigidbody.gravityScale = 3f;

        m_Boss.iOldDir = 0;
        m_TFEffect.localPosition = Vector2.zero;

        m_TFSprite.GetComponent<SpriteRenderer>().flipY = false;
        m_TFSprite.localRotation = Quaternion.identity;

        m_Boss.iDamage = 10;
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
        m_rigidbody.velocity = Vector2.zero;
        m_Boss.bAttack = false;
        m_Boss._animator.ResetTrigger("AtkEnd");
    }

    #endregion
}
