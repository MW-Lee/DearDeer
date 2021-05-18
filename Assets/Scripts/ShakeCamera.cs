////////////////////////////////////////////
//
// ShakeCamera
//
// 카메라를 흔들어주는 스크립트
// 20. 12. 05
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : MonoBehaviour
{
    #region 변수

    private static ShakeCamera instance;

    private CinemachineVirtualCamera m_cam;
    private CinemachineBasicMultiChannelPerlin m_perlin;
    private float fTime;

    #endregion


    #region 함수

    public static ShakeCamera GetInstance()
    {
        if (instance != null)
            return instance;
        else
        {
            Debug.Log("There is no ShakeCamera");
            return null;
        }
    }

    public void ShakeCam(float intensity, float time)
    {
        m_perlin.m_AmplitudeGain = intensity;
        fTime = time;
    }

    #endregion


    #region 실행

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        m_cam = GetComponent<CinemachineVirtualCamera>();
        m_perlin = m_cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (fTime > 0)
        {
            fTime -= Time.deltaTime;
            if(fTime <= 0)
            {
                m_perlin.m_AmplitudeGain = 0f;
            }
        }   
    }

    #endregion
}
