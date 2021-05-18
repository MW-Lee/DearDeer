////////////////////////////////////////////
//
// PlayerState_Wall
//
// 플레이어가 벽에 붙어있을 때 작동하는 스크립트
// 20. 09. 26
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Wall : IState
{
    #region 변수

    private GameObject   m_playerGO;
    private Player       m_player;
    private InputManager m_input;

    private Rigidbody2D  m_rigidbody;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public PlayerState_Wall(GameObject _input)
    {
        m_playerGO = _input;
        m_player = m_playerGO.GetComponent<Player>();
        m_input = InputManager.GetInstance();

        m_rigidbody = m_playerGO.GetComponent<Rigidbody2D>();
    }

    public void OperatorEnter()
    {
        m_player._animator.SetBool("Wall", true);

        m_rigidbody.gravityScale = 0.5f;

        m_player.bJump = false;
    }

    public void OperatorUpdate()
    {
        // 벽이 왼쪽에 있을 때
        //if (m_player.iWhereisWall == -1)
        //{
        //    if (m_input.key_A)
        //    {
        //        m_player._animator.SetBool("Wall", true);
        //        m_rigidbody.velocity = Vector2.down * 0.1f;
        //    }
        //    else if (m_input.keyUp_A)
        //    {
        //        m_player._animator.SetBool("Wall", false);
        //        if (m_player.bOnGround)
        //        {
        //            Player.FSM.SetState("idle");
        //            return;
        //        }
        //        else
        //        {
        //            Player.FSM.SetState("fall");
        //            return;
        //        }
        //    }
        //}
        //else
        //{
        //    if (m_input.key_D)
        //    {
        //        m_player._animator.SetBool("Wall", true);
        //        m_rigidbody.velocity = Vector2.down * 0.1f;
        //    }
        //    else if (m_input.keyUp_D)
        //    {
        //        m_player._animator.SetBool("Wall", false);
        //        if (m_player.bOnGround)
        //        {
        //            Player.FSM.SetState("idle");
        //            return;
        //        }
        //        else
        //        {
        //            Player.FSM.SetState("fall");
        //            return;
        //        }
        //    }
        //}

        m_rigidbody.velocity = Vector2.down * 0.1f;

        if (m_input.keyUp_A || m_input.keyUp_D)
        {
            if (m_player.bOnGround)
            {
                Player.FSM.SetState("idle");
                return;
            }
            else
            {
                Player.FSM.SetState("fall");
                return;
            }
        }
    }

    public void OperatorExit()
    {
        m_player._animator.SetBool("Wall", false);
    }

    #endregion
}
