////////////////////////////////////////////
//
// Token
//
// 토큰에서 작동하는 스크립트
// 20. 10. 26
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    #region 변수

    public AudioClip _audioClip;
    public GameObject gParticle;

    #endregion


    #region 함수

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameObject.Find("VCam").GetComponent<AudioSource>().PlayOneShot(_audioClip);
            gParticle.SetActive(true);

            gameObject.SetActive(false);
        }
    }

    #endregion
}
