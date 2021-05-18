////////////////////////////////////////////
//
// Boss_GroundCheck
//
// Boss의 GroundCheck 에서 작동하는 스크립트
// 20. 11. 21
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_GroundCheck : MonoBehaviour
{
    #region 변수

    public BossMonster_A m_Boss;

    public AudioSource _audioSource;

    #endregion

    #region 실행

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && m_Boss.bAttack)
        {
            m_Boss.bOnGround = true;
            return;
        }

        if (collision.CompareTag("Ground") || collision.CompareTag("ThinGround"))
        {
            m_Boss.bOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("ThinGround"))
        {
            m_Boss.bOnGround = false;
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    #endregion
}
