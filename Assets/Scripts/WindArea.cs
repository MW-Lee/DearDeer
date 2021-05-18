////////////////////////////////////////////
//
// WindArea
//
// 돌풍지역에서 작동하는 스크립트
// 20. 10. 18
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    #region 변수

    public Vector2 vWindDir;
    public int iWindPower;

    private PastObjectManager m_pastmanager;

    private GameObject PastOff;
    private GameObject PastOn;

    private bool bPast;

    #endregion


    #region 함수


    #endregion


    #region 실행

    private void Start()
    {
        m_pastmanager = PastObjectManager.GetInstance();

        PastOff = transform.GetChild(0).gameObject;
        PastOn = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        bPast = m_pastmanager.bPastSee;

        if(bPast)
        {
            PastOff.SetActive(false);
            PastOn.SetActive(true);
        }
        else
        {
            PastOff.SetActive(true);
            PastOn.SetActive(false);
        }
    }

    #endregion
}
