////////////////////////////////////////////
//
// PlayerState_Idle
//
// 플레이어의 기본 상태에서 작동하는 스크립트
// 20. 09. 04
////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : IState
{
    #region 변수

    private GameObject              m_playerGO;
    private Player                  m_player;
    private InputManager            m_input;

    private Rigidbody2D             m_rigidbody;

    #endregion


    #region 함수

    private void CheckState()
    {
        // 이동관련 키가 눌려 이동으로 상태 변경
        if (m_input.key_A || m_input.key_D)
        {
            Player.FSM.SetState("run");
            return;
        }

        // 점프 키가 눌려 점프로 상태 변경
        if (m_input.keyDown_Space)
        {
            Player.FSM.SetState("jump");
            return;
        }

        // 앉기 키가 눌려 앉기로 상태 변경
        if (m_input.keyDown_S)
        {
            Player.FSM.SetState("down");
            return;
        }
    }

    #endregion


    #region 실행

    public PlayerState_Idle(GameObject _input)
    {
        m_playerGO = _input;
        m_player = m_playerGO.GetComponent<Player>();
        m_input = InputManager.GetInstance();

        m_rigidbody = m_playerGO.GetComponent<Rigidbody2D>();
    }

    public void OperatorEnter()
    {
        // 가만히 서 있는 애니메이션 재생
        // 아무런 조건이 충족되는게 없으면 Idle 상태로 돌입함
        m_rigidbody.velocity = Vector2.zero;
        return;
    }

    public void OperatorUpdate()
    {
        // 기본 상태는 움직이지 않으므로 velocity를 zero로 고정
        

        // 현재 상태에서 작업이 끝난 후
        // 키 입력에 따라 상태가 변해야 하는지 확인
        CheckState();
    }

    public void OperatorExit()
    {
        return;
    }

    #endregion
}