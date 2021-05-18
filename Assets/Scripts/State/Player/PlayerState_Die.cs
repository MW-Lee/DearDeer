////////////////////////////////////////////
//
// PlayerState_Die
//
// 플레이어가 죽었을 때 작동하는 스크립트
// 20. 12. 07
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Die : IState
{
    #region 변수

    public GameObject gGameOver;

    private GameObject m_playerGO;
    private Player m_player;
    private InputManager m_input;

    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider;

    #endregion


    #region 함수

    #endregion


    #region 실행

    public PlayerState_Die(GameObject _input)
    {
        m_playerGO = _input;
        m_player = m_playerGO.GetComponent<Player>();
        m_input = InputManager.GetInstance();
        m_rigidbody = m_playerGO.GetComponent<Rigidbody2D>();
        m_collider = m_playerGO.GetComponent<Collider2D>();
    }

    public void OperatorEnter()
    {
        // 죽는 애니메이션 재생
        m_player._animator.SetBool("Die", true);
        // 죽는 오디오 재생
        m_player._audioSource.PlayOneShot(m_player._Sound.mapSound[1]);

        m_player.transform.Find("Attack").gameObject.SetActive(false);
        m_collider.isTrigger = true;
        m_rigidbody.velocity = Vector2.zero;
        m_rigidbody.gravityScale = 0;

        m_player.transform.Find("MapMover").gameObject.SetActive(true);

        // 게임 오버창 활성화
        m_player.bIsShowCut = true;
        //gGameOver.SetActive(true);
    }

    public void OperatorUpdate()
    {
        return;
    }

    public void OperatorExit()
    {
        m_player.transform.Find("Attack").gameObject.SetActive(true);
        m_collider.isTrigger = false;
        m_rigidbody.gravityScale = m_player.fGravityScale;
        m_player._animator.SetBool("Die", false);
    }

    #endregion
}
