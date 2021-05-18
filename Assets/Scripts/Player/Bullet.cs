////////////////////////////////////////////
//
// Bullet
//
// 공격 구체에서 발사되는 탄환에서 작동하는 스크립트
// 20. 10. 04
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Bullet : MonoBehaviour
{
    #region 변수

    public AttackPoint AP;

    private Animator m_animator;
    private Collider2D m_collider;
    private Rigidbody2D m_rigidbody;
    private GameObject m_Trail;

    private Transform m_TF;

    private AudioSource _audioSource;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 터지는 애니메이션 재생
        if (collision.CompareTag("Ground") || 
            collision.CompareTag("Enemy") ||
            collision.CompareTag("Wall")||
            collision.CompareTag("PuzzleObject"))
        {
            m_Trail.SetActive(false);
            m_collider.enabled = false;
            m_rigidbody.velocity = Vector2.zero;
            m_animator.SetTrigger("hit");

            m_TF.Find("Effect").gameObject.SetActive(true);

            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }

    public void AfterEffect()
    {
        m_TF.Find("Effect").gameObject.SetActive(true);

        // 애니메이션 출력이 끝나면 다시 pulling
        AP.PushBullet(this.gameObject);
    }

    #endregion


    #region 실행

    private void Awake()
    {
        m_TF = this.transform;
        m_Trail = m_TF.GetChild(0).gameObject;
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        m_Trail.SetActive(true);
        m_collider.enabled = true;
        m_rigidbody.AddForce(AP.vBulletDest * 50, ForceMode2D.Impulse);
    }

    private void Update()
    {
        // 최대 사거리를 벗어나면 총알이 자동으로 풀링됨
        if (Vector2.Distance(m_TF.position, AP.transform.position) > 20)
            AP.PushBullet(this.gameObject);
    }

    #endregion
}
