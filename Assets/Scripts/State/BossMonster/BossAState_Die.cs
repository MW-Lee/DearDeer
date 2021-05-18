////////////////////////////////////////////
//
// BossAState_Die
//
// BossA 가 사망할 때 작동하는 스크립트
// 20. 12. 08
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAState_Die : IState
{
    #region 변수

    private GameObject m_BossGO;
    private BossMonster_A m_Boss;
    private Rigidbody2D m_rigidbody;



    #endregion


    #region 함수

    #endregion


    #region 실행

    public BossAState_Die(GameObject _input)
    {
        m_BossGO = _input;
        m_Boss = m_BossGO.GetComponent<BossMonster_A>();
        m_rigidbody = m_BossGO.GetComponent<Rigidbody2D>();
    }

    public void OperatorEnter()
    {
        // 애니메이션 재생
        m_Boss._animator.SetBool("Die", true);

        m_Boss._audioSource.PlayOneShot(m_Boss._sound.mapSound[5]);

        m_BossGO.GetComponent<Collider2D>().isTrigger = true;
        m_rigidbody.velocity = Vector2.zero;
        m_rigidbody.gravityScale = 0;
    }

    public void OperatorUpdate()
    {
        
    }

    public void OperatorExit()
    {
        
    }

    #endregion
}
