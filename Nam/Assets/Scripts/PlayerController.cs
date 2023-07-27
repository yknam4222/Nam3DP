using System.Collections;
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

    public Vector3 inputDirection { get; private set; }
    public Vector3 moveDir { get; private set; }

    IdleState idleState;

    Transform groundCheck;
    private int groundLayer;
    public bool isGrounded { get; private set; }

    bool isSprint;

    private void Start()
    {
        player = GetComponent<Player>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");

        idleState = player.stateMachine.GetState(StateName.MOVE) as IdleState;
    }

    private void Update()
    {
        PlayerRotation();
        SetSprintSpeed();
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

    public void OnMoveAxis()
    {
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprint = true;
        }
        else if (context.canceled)
        {
            isSprint = false;
            StartCoroutine(reduceSpeed());
        }
    }

    void SetSprintSpeed()
    {
        if (isSprint)
        {
            if (player.moveSpeed < 6.0f)
                player.moveSpeed += Time.deltaTime * 3.0f;
        }
    }

    IEnumerator reduceSpeed()
    {
        while(player.moveSpeed > 3.0f)
        {
            player.moveSpeed -= Time.deltaTime * 3.0f;

            yield return null;
        }
    }

    public void PlayerRotation()
    {
        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        moveDir = lookForward * inputDirection.z + lookRight * inputDirection.x;

        transform.forward = lookForward;
    }

    public void LookAt(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            transform.rotation = targetAngle;
        }
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