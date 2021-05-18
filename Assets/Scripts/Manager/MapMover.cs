////////////////////////////////////////////
//
// MapMover
//
// 플레이어가 맵을 이동할 때 작동하는 스크립트
// 20. 10. 25
////////////////////////////////////////////
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMover : MonoBehaviour
{
    #region 변수

    public Transform NextMapTF;

    private Transform m_NextMapParentTF;
    private Transform m_VCamTF;
    private Image m_BlackMask;
    private Player m_player;
    private PastObjectManager m_PO;
    private Color m_vColor;
    private float fTime;

    private Vector2 vPoint;

    #endregion


    #region 함수

    public IEnumerator ChangeMap()
    {
        // FadeOut
        while(true)
        {
            m_player.bMapMoving = true;
            m_BlackMask.gameObject.SetActive(true);
            yield return null;

            fTime += Time.deltaTime * 7;

            m_vColor.a = Mathf.Lerp(0, 1, fTime);            

            if(m_BlackMask.color.a >= 1)
            {
                fTime = 0;
                m_vColor.a = 1;
                m_BlackMask.color = m_vColor;
                yield return null;
                break;
            }

            m_BlackMask.color = m_vColor;
            yield return null;
        }

        // Map Move
        //m_player.transform.position = NextMapTF.position + new Vector3(-(iPoint * 2), 0, 0);

        // 만약 캐릭터가 죽음에서 부활중이면 세이브포인트로 이동함
        if (m_player.bIsShowCut)
            m_player.transform.position = m_NextMapParentTF.Find("SavePoint").position;
        else
            m_player.transform.position = NextMapTF.position - new Vector3(vPoint.x * 1.5f, vPoint.y * 1.5f);
        m_VCamTF.GetComponent<CinemachineConfiner>().m_BoundingShape2D
            = m_NextMapParentTF.Find("confiner").GetComponent<PolygonCollider2D>();
        m_PO.goPastObject = m_NextMapParentTF.Find("PastObject").gameObject;

        // 배경음이 전 맵과 다르면 교체
        if (m_VCamTF.GetComponent<AudioSource>().clip !=
            m_NextMapParentTF.Find("Sound").GetComponent<Sound>().mapSound[0])
        {
            m_VCamTF.GetComponent<AudioSource>().clip =
                  m_NextMapParentTF.Find("Sound").GetComponent<Sound>().mapSound[0];
            m_VCamTF.GetComponent<AudioSource>().Play();
        }

        // 죽은 후 부활
        if (m_player.bIsShowCut)
        {
            Player.FSM.SetState("idle");
            m_player.Reseraction();            
            m_player.bIsShowCut = false;
        }

        yield return null;

        // FadeIn
        while (true)
        {
            fTime += Time.deltaTime;

            m_vColor.a = Mathf.Lerp(1, 0, fTime);

            m_BlackMask.color = m_vColor;

            if (m_BlackMask.color.a <= 0)
            {
                fTime = 0;
                m_vColor.a = 0;
                m_BlackMask.color = m_vColor;
                break;
            }
            yield return null;
        }

        m_player.bMapMoving = false;
        m_player._animator.ResetTrigger("Sleep");
        m_BlackMask.gameObject.SetActive(false);
        yield return null;
    }

    public void GoToSavePoint()
    {
        m_NextMapParentTF = m_player._TFSavePoint;
        StartCoroutine(ChangeMap());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !m_player.bMapMoving)
        {
            vPoint = collision.transform.position - transform.position;

            StartCoroutine(ChangeMap());
        }
    }

    #endregion


    #region 실행

    private void Start()
    {
        if (transform.parent.name != "Player")
            m_NextMapParentTF = NextMapTF.parent.parent;
        m_VCamTF = GameObject.Find("VCam").transform;
        m_player = Player.GetInstance();
        m_PO = PastObjectManager.GetInstance();
        m_vColor = new Color(0, 0, 0, 0);
        fTime = 0;

        vPoint = new Vector2();

        m_BlackMask = GameObject.Find("Canvas").transform.Find("Fade").GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (m_player == null) Player.GetInstance();
    }

    #endregion
}
