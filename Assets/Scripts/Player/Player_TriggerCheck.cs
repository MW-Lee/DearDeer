////////////////////////////////////////////
//
// Player_TriggerCheck
//
// 플레이어의 몸에있는 Capsule trigger에서 작동하는 스크립트
// 20. 10. 24
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TriggerCheck : MonoBehaviour
{
    #region 변수

    private Player m_player;
    private Transform m_ParentTF;

    #endregion


    #region 함수

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("ThinGround") && !m_ParentTF.GetComponent<CapsuleCollider2D>().enabled)
        {
            m_ParentTF.GetComponent<CapsuleCollider2D>().enabled = true;

            if (m_player.bDownJump)
            {
                m_player.bDownJump = false;
            }
        }
    }

    #endregion


    #region 실행

    private void Start()
    {
        m_player = GetComponentInParent<Player>();
        m_ParentTF = transform.parent;
    }

    #endregion
}
