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
    public Vector3 inputDirection { get; private set; }

    IdleState idleState;

    Transform groundCheck;
    private int groundLayer;
    public bool isGrounded { get; private set; }

    private void Start()
    {
        player = GetComponent<Player>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");

        idleState = player.stateMachine.GetState(StateName.MOVE) as IdleState;
    }

    private void Update()
    {
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

        transform.rotation = Quaternion.LookRotation(inputDirection);
        transform.Translate(Vector3.forward * Time.deltaTime * 3.0f);
    }

    public void LookAt(Vector3 direction)
    {
        if(direction != Vector3.zero)
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