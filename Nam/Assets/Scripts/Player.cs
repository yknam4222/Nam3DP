using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitController;

public class Player : MonoBehaviour
{
    public static Player Instance {get { return instance; } }
    public Rigidbody rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public CapsuleCollider capsuleCollider { get; private set; }
    public StateMachine stateMachine { get; private set; }

    [SerializeField]
    private Transform weaponLeft;
    [SerializeField]
    private Transform weaponRight;

    private static Player instance;

    #region Ä³¸¯ÅÍ ½ºÅÝ
    public float MaxHP { get { return maxHP; } }
    public float CurrentHP { get { return maxHP; } }
    public float MoveSpeed { get { return moveSpeed; } }

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

            DontDestroyOnLoad(gameObject);
            return;
        }
        else
            DestroyImmediate(gameObject);
    }

    public void OnUpdateStats(float maxHP, float currentHP, float moveSpeed)
    {
        this.maxHP = maxHP;
        this.currentHP = currentHP;
        this.moveSpeed = moveSpeed;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
