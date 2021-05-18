////////////////////////////////////////////
//
// Trap
//
// 함정에서 작동하는 스크립트
// 20. 12. 07
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType
{
    // 가시
    Thorn,

    // 장애물
    Obstacle,


    MAX
}

public class Trap : MonoBehaviour
{
    #region 변수

    public TrapType eType;
    public GameObject gTrapObject;

    public int iDamage;
    public bool bDestroy = false;

    private float fTime;
    private bool bActive = false;

    #endregion


    #region 함수

    public void ObjectControl()
    {
        bActive ^= true;

        gTrapObject.SetActive(bActive);
    }

    public void ObjectDestroy()
    {
        bDestroy = true;
    }

    #endregion


    #region 실행

    #endregion
}
