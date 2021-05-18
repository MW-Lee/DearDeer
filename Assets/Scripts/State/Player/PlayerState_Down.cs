////////////////////////////////////////////
//
// PlayerState_Down
//
// 플레이어가 아래로 숙이고 있는 동안 작동하는 스크립트
// 20. 09. 21
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerState_Down : IState
{
    #region 변수

    private GameObject      m_playerGO;
    private Player          m_player;
    private InputManager    m_input;

    private Rigidbody2D     m_rigidbody;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public PlayerState_Down(GameObject _input)
    {
        m_playerGO = _input;
        m_player = m_playerGO.GetComponent<Player>();
        m_input = InputManager.GetInstance();

        m_rigidbody = m_playerGO.GetComponent<Rigidbody2D>();
    }

    void IState.OperatorEnter()
    {
        // 앉는 애니메이션 재생
        m_player._animator.SetBool("Down", true);

        m_rigidbody.velocity = Vector2.zero;
    }

    void IState.OperatorUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(m_rigidbody.position, Vector2.down, 0.85f, LayerMask.GetMask("ThinGround"));
        //Debug.Log(hit.collider.name);
        
        if (m_input.keyDown_Space /* && hit.collider.CompareTag("ThinGround") */)
        {
            m_playerGO.transform.GetComponent<CapsuleCollider2D>().enabled = false;
            m_player.bDownJump = true;
            m_player.bOnGround = false;
            Player.FSM.SetState("fall");
            return;
        }

        if (m_input.keyUp_S)
            Player.FSM.SetState("idle");
        return;
    }

    void IState.OperatorExit()
    {
        m_player._animator.SetBool("Down", false);
    }

    #endregion
}
