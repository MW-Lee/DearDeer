////////////////////////////////////////////
//
// PlayerState_Hit
//
// 플레이어가 피격당했을 때 작동하는 스크립트
// 20. 11. 27
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Hit : IState
{
    #region 변수

    private GameObject m_playerGO;
    private Player m_player;
    private InputManager m_input;

    private Rigidbody2D m_rigidbody;
    private SpriteRenderer m_spriterenderer;

    private ShakeCamera m_shakecam;

    private float fTime = 0;
    private bool bChangeColor = false;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public PlayerState_Hit(GameObject _input)
    {
        m_playerGO = _input;
        m_player = m_playerGO.GetComponent<Player>();
        m_input = InputManager.GetInstance();
        m_rigidbody = m_playerGO.GetComponent<Rigidbody2D>();
        m_spriterenderer = m_playerGO.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        m_shakecam = ShakeCamera.GetInstance();
    }

    public void OperatorEnter()
    {
        m_player._animator.SetTrigger("Hit");

        m_player._audioSource.PlayOneShot(m_player._Sound.mapSound[0]);

        m_player.iDir = m_player.iWhereisPoint;

        m_rigidbody.AddForce(new Vector2(-m_player.iWhereisPoint * 7, 5), ForceMode2D.Impulse);
        m_player.binvincibility = true;
        bChangeColor = false;
        fTime = 0;

        m_shakecam.ShakeCam(5f, 0.2f);
    }

    public void OperatorUpdate()
    {
        fTime += Time.deltaTime;

        if(bChangeColor)
        {
            bChangeColor = false;
            m_spriterenderer.color = Color.black;
        }
        else
        {
            bChangeColor = true;
            m_spriterenderer.color = Color.white;
        }

        if(fTime >= 0.5f)
        {
            if(m_player.iHP <= 0)
            {
                Player.FSM.SetState("die");
                return;
            }

            if(m_input.key_A || m_input.key_D)
            {
                Player.FSM.SetState("run");
                return;
            }

            if (!m_player.bOnGround)
            {
                Player.FSM.SetState("fall");
                return;
            }

            Player.FSM.SetState("idle");
            return;
        }
    }

    public void OperatorExit()
    {
        m_spriterenderer.color = Color.white;
        m_player.binvincibility = false;
        bChangeColor = false;
        fTime = 0;
    }

    #endregion
}
