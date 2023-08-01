﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnitController;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    public Player player { get; private set; }

    [SerializeField]
    private Transform cameraArm;

    public Transform targetEnemy { get; private set; }
    private Transform target;

    public Vector3 inputDirection { get; private set; }
    public Vector3 moveDir { get; private set; }

    public Vector3 lookForward { get; private set; }

    IdleState idleState;
    RollState rollState;
    TargetRollState targetrollState;
    TargetState targetState;

    Transform groundCheck;
    private int groundLayer;
    public bool isGrounded { get; private set; }

    public bool isTargetting { get; private set; } = false;

    private void Start()
    {
        player = GetComponent<Player>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");

        idleState = player.stateMachine.GetState(StateName.IDLE) as IdleState;
        rollState = player.stateMachine.GetState(StateName.ROLL) as RollState;
        targetState = player.stateMachine.GetState(StateName.IDLE_TARGET) as TargetState;
        targetrollState = player.stateMachine.GetState(StateName.TARGETROLL) as TargetRollState;
    }

    private void Update()
    {
        OnSprintInput();
        if (!isTargetting)
            FindTarget();
    }

    private void FixedUpdate()
    {
        PlayerRotation();
        
    }
    public bool IsGrounded()
    {
        Vector3 boxSize = new Vector3(transform.lossyScale.x, 0.4f, transform.lossyScale.z);
        return Physics.CheckBox(groundCheck.position, boxSize, Quaternion.identity, groundLayer);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (player.isDied)
        {
            inputDirection = Vector3.zero;
            return;
        }

        Vector2 input = context.ReadValue<Vector2>();
        inputDirection = new Vector3(input.x, 0f, input.y);

        //transform.rotation = Quaternion.LookRotation(inputDirection);
        //transform.Translate(Vector3.forward * Time.deltaTime * 3.0f);
    }

    public void OnRollInput(InputAction.CallbackContext context)
    {
        if (player.isDied)
        {
            return;
        }
        if (context.performed)
        {
            if (player.stateMachine.CurrentState is IdleState)
                player.stateMachine.ChangeState(StateName.ROLL);
            else if (player.stateMachine.CurrentState is TargetState)
                player.stateMachine.ChangeState(StateName.TARGETROLL);
        }
    }

    public void OnSprintInput()
    {
        if (player.isDied)
            return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (player.moveSpeed < 6.0f)
                player.moveSpeed += Time.deltaTime * 5.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            StartCoroutine(setspeed());
    }

    public void OnTargetEnemy(InputAction.CallbackContext context)
    {
        if (context.performed && !isTargetting && target != null)
        {
            isTargetting = true;
            targetEnemy = target; //
            Debug.Log("now targe ==" + targetEnemy.name);
            player.stateMachine.ChangeState(StateName.IDLE_TARGET);
        }
        else if (context.performed && isTargetting)
        {
            isTargetting = false;
            player.stateMachine.ChangeState(StateName.IDLE);
        }
    }

    IEnumerator setspeed()
    {
        while (player.moveSpeed > 3.0f)
        {
            player.moveSpeed -= Time.deltaTime * 5.0f;

            yield return null;
        }
    }

    public void PlayerRotation()
    {

        lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        //if (!isTargetting)
            moveDir = lookForward * inputDirection.z + lookRight * inputDirection.x;

        bool isMove = inputDirection.magnitude != 0;
        if (isMove && !(player.stateMachine.CurrentState is RollState))
        {
            if (player.stateMachine.CurrentState is IdleState)
                transform.forward = moveDir;
            else if (player.stateMachine.CurrentState is TargetState)
                transform.forward = lookForward;
        }

    }

    public void LookAt(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            transform.rotation = targetAngle;
        }
    }

    public void FindTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 10f, 1 << 6);
        Transform closestTraget = null;
        float maxDistnace = Mathf.Infinity;

        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Enemy")
                {
                    float targetDistance = Vector3.Distance(transform.position, cols[i].transform.position);

                    Vector3 targetDirection = (cols[i].transform.position - transform.position).normalized;
                    float targetAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (targetDistance < maxDistnace && targetAngle < 90)
                    {
                        closestTraget = cols[i].transform;
                        maxDistnace = targetDistance;
                    }
                    Debug.Log("Physics Enemy : Target found");
                }
            }
            if(closestTraget)
            {
            target = closestTraget;
            Debug.Log(target.name);
            }
        }
        else
        {
            Debug.Log("Physics Enemy : Target lost");
            target = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}

/*
[SerializeField]
private float walkSpeed;//걷기 속도

[SerializeField]
private float runSpeed;//달리기 속도

private float hAxis; //좌우 이동
private float vAxis; //앞뒤 이동

private Vector3 moveVec;

private Animator anim;//애니메이션


private void Awake()
{
    anim = GetComponent<Animator>();
}

private void FixedUpdate()
{
    hAxis = Input.GetAxis("Horizontal");
    vAxis = Input.GetAxis("Vertical");

    moveVec = new Vector3(hAxis, 0, vAxis).normalized;

    transform.position += moveVec * walkSpeed * Time.deltaTime;
}

private void Update()
{
    if (hAxis == 0 && vAxis == 0)
        anim.SetBool("isRun", false);
    else
        anim.SetBool("isRun", true);

    anim.SetFloat("x", hAxis);
    anim.SetFloat("y", vAxis);

}
 */