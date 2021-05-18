////////////////////////////////////////////
//
// AttackPoint
//
// 플레이어의 공격 구체에서 작동하는 스크립트
// 20. 10. 04
////////////////////////////////////////////
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    #region 변수

    public Transform PlayerTF;
    public GameObject Bullet;
    public List<GameObject> BulletListPulling;

    public Vector3 vBulletDest;

    private InputManager m_input;
    public Animator _animator;

    private AudioSource _audioSource;

    #endregion


    #region 함수

    //
    // 공격시 나갈 탄환 관련 함수
    //
    public void InitPool()
    {
        BulletListPulling = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        { 
            GameObject temp = Instantiate(Bullet);
            PushBullet(temp);
        }
    }

    public void PushBullet(GameObject _obj)
    {
        BulletListPulling.Add(_obj);
        _obj.GetComponent<Bullet>().AP = this;
        _obj.transform.SetParent(this.transform);
        _obj.transform.localPosition = Vector3.zero;
        _obj.SetActive(false);
    }

    public GameObject PopBullet()
    {
        if(BulletListPulling.Count > 0)
        {
            GameObject temp = BulletListPulling[0];
            BulletListPulling.RemoveAt(0);

            return temp;
        }
        else
        {
            return Instantiate(Bullet);
        }
    }

    public void ClearPool()
    {
        BulletListPulling.Clear();

        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion


    #region 실행

    private void Start()
    {
        InitPool();

        m_input = InputManager.GetInstance();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (m_input.keyDown_LeftMouse)
        {
            if (PlayerTF.GetComponent<Player>().bDialog || PlayerTF.GetComponent<Player>().bIsShowCut)
                return;

            //if (m_EnemyList.Count == 0)
            //    Debug.Log("There is no enemy");
            //else
            //{
            //    vBulletDest = Input.mousePosition - transform.position;
            //    PopBullet().SetActive(true);
            //}

            // 소리 재생
            _audioSource.PlayOneShot(_audioSource.clip, 0.5f);
            // 마우스의 좌표를 구하는 법 (2D)
            var Point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            // 마우스 좌표 - 공격개체 좌표 = 공격개체가 마우스를 바라보는 방향벡터
            vBulletDest = Point - transform.position;
            // 구해진 벡터를 normalize 시켜서 방향벡터로 바꿈
            vBulletDest = vBulletDest.normalized;
            // Pulling 되어있는 총알 오브젝트 하나를 꺼내서 발사
            PopBullet().SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        ClearPool();
    }

    #endregion
}
