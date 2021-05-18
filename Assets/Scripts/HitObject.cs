////////////////////////////////////////////
//
// HitObject
//
// 타격으로 부서지는 오브젝트에서 작동하는 스크립트
// 20. 11. 15
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    #region 변수

    [HideInInspector]
    public int iHitCount;
    public int iMaxHP;

    public AudioClip _sound;

    #endregion


    #region 함수

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            iHitCount++;

            if (iHitCount == iMaxHP)
            {
                GameObject.Find("VCam").GetComponent<AudioSource>().PlayOneShot(_sound);
                this.gameObject.SetActive(false); 
            }
        }
    }

    #endregion


    #region 실행

    #endregion
}
