////////////////////////////////////////////
//
// PlayerState_Fall
//
// 플레이어가 낙하하는 상태에서 작동하는 스크립트
// 20. 09. 16
////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class PlayerState_Fall : IState
{
    #region 변수

    private GameObject  m_playerGO;
    private Player      m_player;
    private InputManager m_input;

    private Rigidbody2D m_rigidbody;

    private float fGravityScale;
    private float fSpeed;

    #endregion


    #region 함수

    private void InputKeyboardWhenFall()
    {
        Vector2 moveVector = new Vector2
        {
            x = (m_player.iDir * fSpeed) + m_player.vWindDir.x,
            y = m_rigidbody.velocity.y
        };

        //if (Math.Abs(moveVector.x) >= Math.Abs(Input.GetAxisRaw("Horizontal") * fSpeed))
        //{
        //    moveVector.x = Input.GetAxisRaw("Horizontal") * fSpeed;
        //}

        m_rigidbody.velocity = moveVector;
    }

    #endregion


    #region 실행

    public PlayerState_Fall(GameObject _input)
    {
        m_playerGO = _input;
        m_player = m_playerGO.GetComponent<Player>();
        m_input = InputManager.GetInstance();

        m_rigidbody = m_playerGO.GetComponent<Rigidbody2D>();
        fGravityScale = m_player.fGravityScale;
        fSpeed = m_player.fSpeed;
    }


    void IState.OperatorEnter()
    {
        m_player._animator.SetBool("Fall", true);

        m_rigidbody.gravityScale = fGravityScale;

        //m_playerGO.transform.GetChild(0).GetComponent<CapsuleCollider2D>().isTrigger = false;
    }

    void IState.OperatorUpdate()
    {
        if (m_input.key_A || m_input.key_D)
        {
            InputKeyboardWhenFall();
        }

        if (m_input.keyDown_Space && !m_player.bJump)
        {
            m_player.bJump = true;
            Player.FSM.SetState("jump");
            return;
        }

        if (m_input.keyDown_LeftControl)
        {
            Player.FSM.SetState("dash");
            return;
        }

        if (m_player.bOnGround)
        {
            //m_rigidbody.gravityScale = 0;
            m_player.bJump = false;
            m_player.bWall = false;


            if (m_input.key_A || m_input.key_D)
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

        //RaycastHit2D hit = Physics2D.Raycast(m_rigidbody.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

        //if (hit.collider != null /* && m_rigidbody.velocity.y < 0 */)
        //{
        //    if(hit.collider.CompareTag("ThinGround") && m_player.bDownJump)
        //    {
        //        return;
        //    }

        //    m_player.bOnGround = true;
        //    m_rigidbody.gravityScale = 0;
        //}
    }

    void IState.OperatorExit()
    {
        m_player._animator.SetBool("Fall", false);

        if (m_player.bDownJump)
        {
            m_player.bDownJump = false;
            //m_playerGO.transform.GetChild(0).GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }

    #endregion
}
