////////////////////////////////////////////
//
// NPC
//
// NPC에서 작동하는 스크립트
// 20. 12. 04
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    #region 변수

    public Animator _animator;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.transform.parent.Find("Attack").GetComponent<AttackPoint>()._animator.SetBool("Interaction", true);
        _animator.SetBool("Talk", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       // collision.transform.parent.Find("Attack").GetComponent<AttackPoint>()._animator.SetBool("Interaction", false);
        _animator.SetBool("Talk", false);
    }

    #endregion


    #region 실행

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    #endregion
}
