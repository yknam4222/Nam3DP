using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitController;

public class Player : MonoBehaviour
{
    public bool isDied { get; private set; } = false;
    public static Player Instance {get { return instance; } }
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

    #region 카메라
    public Transform CameraArm { get { return cameraArm; } }

    [SerializeField]
    private Transform cameraArm;
    #endregion

    #region 캐릭터 스텟
    public float MaxHP { get { return maxHP; } }
    public float CurrentHP { get { return maxHP; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public float statusSpeed { get;  set; } = 0.0f;

    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float moveSpeed;
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
        stateMachine = new StateMachine(StateName.MOVE, new MoveState());
    }
}
