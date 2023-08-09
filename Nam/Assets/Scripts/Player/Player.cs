using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitController;

public class Player : MonoBehaviour, ISoundPlayable
{
    public bool isDied { get; private set; } = false;
    public static Player Instance { get { return instance; } }
    public Rigidbody rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public PlayerController Controller { get; private set; }
    public CapsuleCollider capsuleCollider { get; private set; }
    public StateMachine stateMachine { get; private set; }

    [SerializeField]
    private Transform weaponLeft;
    [SerializeField]
    private Transform weaponRight;

    private static Player instance;

    #region 캐릭터 스텟
    public float MaxHP { get { return maxHP; } }
    public float CurrentHP { get { return currentHP; } }
    public float MaxMP { get { return maxMP; } }
    public float CurrentMP { get { return currentMP; } }
    public float MaxST { get { return maxST; } }
    public float CurrentST { get { return currentST; } }

    public float moveSpeed { get; set; } = 3.0f;
    public float statusSpeed { get; set; } = 0.0f;

    public float CurrnetSpeed { get { return currentSpeed; } }

    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float maxMP;
    [SerializeField] protected float currentMP;
    [SerializeField] protected float maxST;
    [SerializeField] protected float currentST;

    [SerializeField] private float currentSpeed; //테스트용
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            rigidBody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            Controller = GetComponent<PlayerController>();

            InitStateMachine();
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
            DestroyImmediate(gameObject);
    }

    private void Update()
    {
        stateMachine?.UpdateState();

        currentSpeed = moveSpeed + statusSpeed; //테스트용
    }
    private void FixedUpdate()
    {
        stateMachine?.FixedUpdateState();
    }

    public void OnUpdateStats(float maxHP, float currentHP, float moveSpeed)
    {
        this.maxHP = maxHP;
        this.currentHP = currentHP;
        this.moveSpeed = moveSpeed;
    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine(StateName.IDLE, new IdleState());
        stateMachine.AddState(StateName.ROLL, new RollState());
        stateMachine.AddState(StateName.IDLE_TARGET, new TargetState());
        stateMachine.AddState(StateName.TARGETROLL, new TargetRollState());
        stateMachine.AddState(StateName.ATTACK, new AttackState());
    }

    public void PlaySound(string _key)
    {
        SoundManager.Instance.PlayBGM(_key);
    }
}
