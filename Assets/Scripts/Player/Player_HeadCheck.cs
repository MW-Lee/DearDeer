////////////////////////////////////////////
//
// Player_HeadCheck
//
// 플레이어가 점프 시 통과 가능한지 확인하는
// Collider에서 작동하는 스크립트
// 20. 10. 18
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HeadCheck : MonoBehaviour
{
    #region 변수

    private Player m_player;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ThinGround"))
        {
            m_player.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else if(collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            m_player.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ThinGround"))
        {
            if (m_player.GetComponent<Rigidbody2D>().velocity.y < 0)
                m_player.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    #endregion


    #region 실행

    private void Start()
    {
        m_player = transform.GetComponentInParent<Player>();
    }

    #endregion
}
