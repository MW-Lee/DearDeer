////////////////////////////////////////////
//
// Player_GroundCheck
//
// 플레이어가 땅에 있는지 확인하는 Collider에서 작동하는 스크립트
// 20. 09. 16
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundCheck : MonoBehaviour
{
    #region 변수

    public AudioSource _audioSource;
    public Sound _sound;

    private Player      m_player;
    private Rigidbody2D m_rigidbody;
    private Collider2D  m_collider;

    #endregion


    #region 함수

    private void GroundCheck()
    {
        //RaycastHit2D hit = Physics2D.Raycast(m_rigidbody.position, Vector2.down, 0.77f, LayerMask.GetMask("Ground"));
        if (m_collider == null && 
            /*hit.collider == null &&*/ 
            Player.FSM.CurrentState != Player.FSM.GetState("fall") &&
            Player.FSM.CurrentState != Player.FSM.GetState("wall") &&
            Player.FSM.CurrentState != Player.FSM.GetState("dash") &&
            Player.FSM.CurrentState != Player.FSM.GetState("jump") &&
            Player.FSM.CurrentState != Player.FSM.GetState("hit"))
        {
            m_player.bOnGround = false;
            Player.FSM.SetState("fall");
            return;
        }
        else if (Player.FSM.CurrentState == Player.FSM.GetState("fall") &&
            (m_collider != null /* || hit.collider != null */) && 
            !m_player.bDownJump && 
            !m_player.bOnGround)
        {
            m_player._audioSourceInSoundManager.PlayOneShot(m_player._SoundInSoundManager.mapSound[3]);
            m_player.bOnGround = true;
        }
    }

    public void PlayWalkSound()
    {
        int i = Random.Range(0, _sound.mapSound.Length - 1);

        _audioSource.PlayOneShot(_sound.mapSound[i]);
    }

    #endregion


    #region 실행

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("ThinGround"))
            m_collider = collision;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("ThinGround"))
            m_collider = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(collision.CompareTag("ThinGround") && Player.FSM.CurrentState == Player.FSM.GetState("jump"))
        //{
        //    m_player.transform.GetChild(0).GetComponent<CapsuleCollider2D>().isTrigger = false;
        //}

        if (collision.CompareTag("Ground") || collision.CompareTag("ThinGround"))
            m_collider = null;
    }

    private void Start()
    {
        m_player = transform.GetComponentInParent<Player>();
        m_rigidbody = transform.GetComponentInParent<Rigidbody2D>();
        m_collider = null;

        _audioSource = GetComponent<AudioSource>();
        _sound = GetComponent<Sound>();
    }

    private void Update()
    {
        GroundCheck();
    }

    #endregion
}
