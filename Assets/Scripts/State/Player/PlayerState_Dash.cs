////////////////////////////////////////////
//
// PlayerState_Dash
//
// 플레이어가 대쉬할 때 작동하는 스크립트
// 20. 09. 27
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerState_Dash : IState
{
    #region 변수

    private GameObject m_playerGO;
    private Player m_player;

    private Rigidbody2D m_rigidbody;

    private float ftempGravity;
    private float fTime;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public PlayerState_Dash(GameObject _input)
    {
        m_playerGO = _input;
        m_player = m_playerGO.GetComponent<Player>();
        m_rigidbody = m_playerGO.GetComponent<Rigidbody2D>();
    }

    public void OperatorEnter()
    {
        ftempGravity = m_rigidbody.gravityScale;
        m_player._animator_skill.SetInteger("Skill", 1);

        m_player._audioSourceInSoundManager.PlayOneShot(m_player._SoundInSoundManager.mapSound[2]);

        m_rigidbody.gravityScale = 0f;
        fTime = 0;
    }

    public void OperatorUpdate()
    {
        fTime += Time.deltaTime;

        if (fTime <= 0.1f)
        {
            m_rigidbody.velocity = Vector2.right * m_player.iDir * 60;
        }
        else if(fTime > 0.1f)
        {
            if (!m_player.bOnGround)
            { 
                Player.FSM.SetState("fall");
                return;
            }
            else
            {
                if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    Player.FSM.SetState("run");
                    return;
                }
                else
                {
                    Player.FSM.SetState("idle");
                    return;
                }
            }
            
        }
    }

    public void OperatorExit()
    {
        fTime = 0;
        m_rigidbody.velocity = Vector2.zero;
        m_rigidbody.gravityScale = ftempGravity;
    }

    #endregion
}
