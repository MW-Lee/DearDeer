////////////////////////////////////////////
//
// Player
//
// 플레이어를 동작시키는 스크립트
// 20. 09. 04
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 변수

    public static StateMachine<Player> FSM;
    private static Player instance;

    public Animator _animator;
    public Animator _animator_skill;
    public SpriteRenderer _spriterenderer_skill;
    public NPC _TalkNPC;
    public Transform _TFSavePoint;

    public float fSpeed;
    public float fJumpPower;
    public float fGravityScale;

    public UnityEngine.UI.Image _hpimage;
    public UnityEngine.UI.Image _mpimage;
    public int iHP;
    public int iMP;

    [HideInInspector]
    public int iWhereisPoint;
    [HideInInspector]
    public int iDir;
    [HideInInspector]
    public Vector2 vWindDir;
    [HideInInspector]
    public bool bJump;
    [HideInInspector]
    public bool bDownJump;
    [HideInInspector]
    public bool bOnGround;
    [HideInInspector]
    public bool bWall;
    [HideInInspector]
    public bool bMapMoving;
    [HideInInspector]
    public bool bBossFight = false;

    // 무적
    [HideInInspector]
    public bool binvincibility = false;

    // DialogWindow가 작동하기 위한 변수들
    [HideInInspector]
    public bool bDialog;
    [HideInInspector]
    public Queue<DialogStruct> qDialogSentences;
    public Quiz _quiz;

    // ShowCut을 위한 변수들
    [HideInInspector]
    public bool bIsShowCut;
    [HideInInspector]
    public GameObject gShowCut;

    private InputManager m_input;
    private PastObjectManager m_PO;
    private Rigidbody2D m_rigidbody;
    private SpriteRenderer m_spriterenderer;

    // 소지 사운드
    // 0 Hit
    // 1 Die
    public AudioSource _audioSource;
    public Sound _Sound;

    // 사운드 매니저 소지 사운드
    // 0 jump
    // 1 double jump
    // 2 dash
    // 3 landing
    public AudioSource _audioSourceInSoundManager;
    public Sound _SoundInSoundManager;

    #endregion


    #region 함수

    public static Player GetInstance()
    {
        if (instance != null)
            return instance;
        else
        {
            Debug.Log("There is no Player");
            return null;
        }
    }

    public void SetSavePoint(Transform _input)
    {
        _TFSavePoint = _input;
    }

    public void ReSpawn()
    {
        transform.Find("MapMover").GetComponent<MapMover>().GoToSavePoint();
    }

    public void Reseraction()
    {
        iHP = 100;
        iMP = 30;
        RefreshUI();

        if (bBossFight)
        {
            _TFSavePoint.Find("Kernoon").localPosition = new Vector2(6.16f, 0.55f);
        }
        else
        {
            _animator.SetTrigger("Sleep");
        }
    }

    private void RefreshDirection()
    {
        if (bIsShowCut) return;

        if (m_input.keyDouble_A)
        {
            iDir = -1;
            m_spriterenderer.flipX = true;
            return;
        }
        else if (m_input.keyDouble_D)
        {
            iDir = 1;
            m_spriterenderer.flipX = false;
            return;
        }

        if (m_input.key_A) iDir = -1;
        else if (m_input.key_D) iDir = 1;

        if (iDir == 1)
        {
            m_spriterenderer.flipX = false;
            _spriterenderer_skill.flipX = true;
        }
        else 
        { 
            m_spriterenderer.flipX = true;
            _spriterenderer_skill.flipX = false;
        }
    }

    private void RefreshUI()
    {
        _hpimage.fillAmount = iHP / 100f;
        _mpimage.fillAmount = iMP / 30f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌점이 어느쪽인지 확인함
        if (collision.contacts[0].point.x < transform.position.x)
            iWhereisPoint = -1;
        else
            iWhereisPoint = 1;

        switch (collision.collider.tag)
        {
            case "Wall":
                if (!bWall && !bOnGround)
                {
                    bWall = true;

                    if (iWhereisPoint == -1 && m_input.key_A)
                    {
                        FSM.SetState("wall");
                    }
                    else if(iWhereisPoint == 1 && m_input.key_D)
                    {
                        FSM.SetState("wall");
                    }
                }
                return;

            case "Enemy":
                if (!binvincibility)
                {
                    iHP -= collision.collider.GetComponent<Monster>().iDamage;
                    FSM.SetState("hit");                    
                }
                return;

            case "Boss":
                if (!binvincibility)
                {
                    iHP -= collision.transform.GetComponent<BossMonster_A>().iDamage;
                    FSM.SetState("hit");
                }
                return;

            case "Trap":
                if (!binvincibility)
                {
                    iHP -= collision.collider.GetComponent<Trap>().iDamage;
                    FSM.SetState("hit");
                }
                return;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CompareTag("Enemy") && !binvincibility)
            FSM.SetState("hit");
        return;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bWall = false;
    }

    #endregion


    #region 실행

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    private void Start()
    {
        // 각 상태를 생성하고 Dictionary에 저장하는 작업
        // 추후 사용하기 위해 플레이어의 Start, 즉 게임 시작시 생성한다.
        IState _idle    = new PlayerState_Idle(this.gameObject);
        IState _run     = new PlayerState_Run(this.gameObject);
        IState _jump    = new PlayerState_Jump(this.gameObject);
        IState _fall    = new PlayerState_Fall(this.gameObject);
        IState _down    = new PlayerState_Down(this.gameObject);
        IState _hit     = new PlayerState_Hit(this.gameObject);
        IState _wall    = new PlayerState_Wall(this.gameObject);
        IState _dash    = new PlayerState_Dash(this.gameObject);
        IState _die     = new PlayerState_Die(this.gameObject);

        FSM = new StateMachine<Player>(_idle);

        FSM.AddState("idle", _idle);
        FSM.AddState("run" , _run );
        FSM.AddState("jump", _jump);
        FSM.AddState("fall", _fall);
        FSM.AddState("down", _down);
        FSM.AddState("hit" , _hit );
        FSM.AddState("wall", _wall);
        FSM.AddState("dash", _dash);
        FSM.AddState("die" , _die);


        // 상태 외에 플레이어가 들고 있어야 하는 변수 값들 초기화
        m_input = InputManager.GetInstance();
        m_PO = PastObjectManager.GetInstance();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_spriterenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        _animator = GetComponent<Animator>();

        _TalkNPC = null;
        vWindDir = new Vector2();
        iDir = 1;
        //fSpeed = 3;
        bOnGround = true;
        bDownJump = false;
        bJump = false;
        bWall = false;
        bMapMoving = false;

        qDialogSentences = new Queue<DialogStruct>();
        _quiz = null;

        bDialog = false;

        _audioSource = GetComponent<AudioSource>();
        _audioSourceInSoundManager = transform.Find("SoundManager").GetComponent<AudioSource>();
        _Sound = GetComponent<Sound>();
        _SoundInSoundManager = transform.Find("SoundManager").GetComponent<Sound>();
    }

    private void Update()
    {
        // 실험
        //Debug.Log(FSM.CurrentState);

        // 특정 조건이 진행되고 있을 경우 아무것도 발동되면 안됨
        if (bDialog) return;

        RefreshDirection();
        RefreshUI();

        if (m_input.keyDown_E)
        {
            m_PO.ActivatePastSee();
        }

        FSM.CurrentState.OperatorUpdate();
    }

    #endregion
}
