////////////////////////////////////////////
//
// BossAState_Crash
//
// BossA 패턴 중 내려찍을 때 작동하는 스크립트
// 20. 12. 03
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_Crash : IState
{
    #region 변수

    private GameObject m_BossGO;
    private BossMonster_A m_Boss;

    private Transform m_TFSprite;
    private Transform m_TFEffect;

    private Rigidbody2D m_rigidbody;
    private Vector3 m_vCrash;

    private ShakeCamera m_shakecam;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public BossAState_Crash(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();
        m_TFSprite = m_BossGO.transform.Find("Sprite");
        m_TFEffect = m_BossGO.transform.Find("Effect");

        m_rigidbody = m_BossGO.GetComponent<Rigidbody2D>();

        m_shakecam = ShakeCamera.GetInstance();
    }

    public void OperatorEnter()
    {
        m_vCrash = m_Boss.vDest;
        m_vCrash = (m_vCrash - m_Boss.transform.position).normalized;

        //float angle = Quaternion.FromToRotation(Vector3.up, m_vCrash).eulerAngles.z;
        //m_TFSprite.localRotation = new Quaternion(0, 0, angle, 1);

        m_TFSprite.right = m_vCrash;

        if (m_Boss.iDir == -1)
        {
            m_TFSprite.GetComponent<SpriteRenderer>().flipX = false;
            m_TFSprite.GetComponent<SpriteRenderer>().flipY = true;
        }

        m_Boss._audioSource.PlayOneShot(m_Boss._sound.mapSound[4]);
        m_rigidbody.AddForce(m_vCrash * 50, ForceMode2D.Impulse);

        m_Boss.iDamage = 35;
    }

    public void OperatorUpdate()
    {
        if (m_Boss.bDie)
        {
            m_Boss.FSM.SetState("die");
            return;
        }

        if (m_Boss.bOnGround)
        {
            // 충격 애니메이션 재생
            m_Boss._animator.SetTrigger("Crash");

            m_rigidbody.velocity = Vector2.zero;
            m_TFEffect.localPosition = new Vector2(0.07f, 0.34f);
            m_shakecam.ShakeCam(10f, 0.5f);
        }
    }

    public void OperatorExit()
    {
        m_Boss._animator.ResetTrigger("Crash");
    }

    #endregion
}
