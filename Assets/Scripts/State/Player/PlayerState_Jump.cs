////////////////////////////////////////////
//
// PlayerState_Jump
//
// 플레이어의 점프 상태에서 작동하는 스크립트
// 20. 09. 16
////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Jump : IState
{
    #region 변수

    private GameObject m_playerGO;
    private Player m_player;
    private InputManager m_input;

    private Rigidbody2D m_rigidbody;

    private float fSpeed;
    private float fPower;
    private float fTime;

    #endregion

    #region 함수

    private void InputKeyboardWhenJump()
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

    public PlayerState_Jump(GameObject _input)
    {
        m_playerGO = _input;
        m_player = m_playerGO.GetComponent<Player>();
        m_input = InputManager.GetInstance();

        m_rigidbody = m_playerGO.GetComponent<Rigidbody2D>();

        fSpeed = m_player.fSpeed;
        fTime = 0;
    }

    void IState.OperatorEnter()
    {
        // 점프 애니메이션 재생
        // 한번만 재생되면 되므로 Trigger 사용하여 재생
        m_player._animator.SetTrigger("Jump");

        // 오디오 재생
        if(!m_player.bJump)
        m_player._audioSourceInSoundManager.PlayOneShot(m_player._SoundInSoundManager.mapSound[0]);

        fPower = m_player.fJumpPower;
        fTime = 0;

        m_rigidbody.gravityScale = m_player.fGravityScale;

        if (m_player.bJump)
        {
            //m_rigidbody.velocity = Vector2.zero;
            //m_rigidbody.AddForce(Vector2.up * fPower * 2, ForceMode2D.Impulse);
            m_player._audioSourceInSoundManager.PlayOneShot(m_player._SoundInSoundManager.mapSound[1]);
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, fPower * 3f);
        }
    }

    void IState.OperatorUpdate()
    {
        if (m_player.bOnGround)
        {
            fTime += Time.deltaTime;

            if (m_input.keyUp_Space || fTime >= 0.1f)
            {
                //m_rigidbody.velocity = Vector2.zero;
                m_rigidbody.AddForce(Vector2.up * fPower * (fTime * 30), ForceMode2D.Impulse);

                m_player.bOnGround = false;
            }
        }
        else if (m_rigidbody.velocity.y > 0 && !m_player.bWall)
        {
            if (m_input.key_A || m_input.key_D)
            {
                InputKeyboardWhenJump();
            }

            if (m_input.keyDown_Space && !m_player.bJump)
            {
                m_player._audioSourceInSoundManager.PlayOneShot(m_player._SoundInSoundManager.mapSound[1]);

                m_player.bJump = true;
                //m_rigidbody.AddForce(Vector2.up * fPower, ForceMode2D.Impulse);
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, fPower * 3f);
            }

            if (m_input.keyDown_LeftControl)
            {
                Player.FSM.SetState("dash");
                return;
            }
        }
        else if (m_rigidbody.velocity.y <= 0)
        {
            Player.FSM.SetState("fall");
            return;
        }
    }

    void IState.OperatorExit()
    {
        fTime = 0;
    }

    #endregion
}
