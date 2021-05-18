////////////////////////////////////////////
//
// PlayerState_Run
//
// 플레이어가 움직이는 상태에서 작동하는 스크립트
// 20. 09. 16
////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Run : IState
{
    #region 변수

    private GameObject m_playerGO;
    private Player m_player;
    private InputManager m_input;
    private Player_GroundCheck m_groundCheck;

    private Rigidbody2D m_rigidbody;
    private Vector2 m_vMove;

    private float m_fSpeed;

    #endregion


    #region 함수

    private void CheckState()
    {
        if (!m_input.keyDouble_A && !m_input.keyDouble_D &&
            !m_input.key_A && !m_input.key_D)
        {
            Player.FSM.SetState("idle");
            return;
        }

        if (m_input.keyDown_Space)
        {
            Player.FSM.SetState("jump");
            return;
        }

        if (m_input.keyDown_S)
        {
            Player.FSM.SetState("down");
            return;
        }

        if (m_input.keyDown_LeftControl)
        {
            Player.FSM.SetState("dash");
            return;
        }
    }

    #endregion


    #region 실행

    public PlayerState_Run(GameObject _input)
    {
        m_playerGO = _input;
        m_player = _input.GetComponent<Player>();
        m_input = InputManager.GetInstance();
        m_groundCheck = m_playerGO.transform.Find("GroundCheck").GetComponent<Player_GroundCheck>();

        m_rigidbody = m_playerGO.GetComponent<Rigidbody2D>();
        m_vMove = new Vector2();
    }

    public void OperatorEnter()
    {
        // 움직이는 애니메이션 재생
        m_player._animator.SetBool("Move", true);

        m_fSpeed = m_playerGO.GetComponent<Player>().fSpeed;
    }

    public void OperatorUpdate()
    {
        //Vector2 moveVector = new Vector2
        //{
        //    x = Input.GetAxis("Horizontal") * m_fSpeed,
        //    y = m_rigidbody.velocity.y
        //};
        //if (Math.Abs(m_vMove.x) >= Math.Abs(Input.GetAxisRaw("Horizontal") * m_fSpeed))
        //{
        //    m_vMove.x = Input.GetAxisRaw("Horizontal") * m_fSpeed;
        //}

        if (!m_groundCheck._audioSource.isPlaying)
        {
            m_groundCheck.PlayWalkSound();
        }

        m_vMove = Vector2.zero;
        m_vMove.x = (m_player.iDir * m_fSpeed) + m_player.vWindDir.x;
        m_vMove.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = m_vMove;

        CheckState();
    }

    public void OperatorExit()
    {
        m_player._animator.SetBool("Move", false);

        //m_rigidbody.velocity = Vector2.zero;
        m_vMove = Vector2.zero;
    }

    #endregion
}
