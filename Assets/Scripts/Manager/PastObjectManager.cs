////////////////////////////////////////////
//
// PastObjectManager
//
// 오브젝트들을 관리해주는 매니저
// 20. 10. 19
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastObjectManager : MonoBehaviour
{
    #region 변수

    // 다른 스크립트에서 입력을 불러다 사용할 instance
    private static PastObjectManager instance;

    public GameObject goPastSeeMask;
    public GameObject goPastObject;

    public bool bPastSee = false;

    #endregion


    #region 함수

    /// <summary>
    /// 다른 스크립트에서 사용하기 위한 instance 반환 함수
    /// </summary>
    /// <returns>InputManager의 instance</returns>
    public static PastObjectManager GetInstance()
    {
        if (instance != null)
            return instance;
        else
        {
            Debug.Log("There is no PastObjectManager");
            return null;
        }
    }


    public void FindPastObject()
    {
        // 현재 활동중인 맵에서 PastObject를 찾아서 가져와야 함

        
    }

    public void ActivatePastSee()
    {
        // 과거투시 이펙트가 실행되어야 함
        bPastSee ^= true;

        goPastSeeMask.SetActive(bPastSee);
        goPastObject.SetActive(bPastSee);
        return;
    }

    #endregion


    #region 실행

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        bPastSee = false;
    }

    #endregion
}
