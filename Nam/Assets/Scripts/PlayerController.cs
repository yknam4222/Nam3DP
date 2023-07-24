using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitController;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    public Player player { get; private set; }
    public Vector3 inputDirection { get; private set; }

    Transform groundCheck;
    private int groundLayer;
    public bool isGrounded { get; private set; }

    public Vector2 moveInput { get; private set; }

    public Vector3 moveDir { get; private set; }


    private void Start()
    {
        player = GetComponent<Player>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");

    }

    private void Update()
    {
        SetDir();
        LookAround();
        setSpeed();
    }

    public bool IsGrounded()
    {
        Vector3 boxSize = new Vector3(transform.lossyScale.x, 0.4f, transform.lossyScale.z);
        return Physics.CheckBox(groundCheck.position, boxSize, Quaternion.identity, groundLayer);
    }

    private void setSpeed()
    {
        if (moveInput.x != 0 && moveInput.y == 0)
            player.statusSpeed = -1.5f;
        else if (moveInput.y > 0)
            player.statusSpeed = 0.0f;
        else if (moveInput.y < 0)
            player.statusSpeed = -1.0f;
    }

    private void SetDir()
    {
        if(player.isDied)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 lookForward = new Vector3(player.CameraArm.forward.x, 0f, player.CameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(player.CameraArm.right.x, 0f, player.CameraArm.right.z).normalized;
        moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        player.transform.forward = lookForward;
    }
    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * 2.0f, Input.GetAxis("Mouse Y") * 2.0f);
        Vector3 canAngle = player.CameraArm.rotation.eulerAngles;

        float x = canAngle.x - mouseDelta.y;

        if (x < 180f)
            x = Mathf.Clamp(x, -1f, 70f);
        else
            x = Mathf.Clamp(x, 335f, 361f);

        player.CameraArm.rotation = Quaternion.Euler(x, canAngle.y + mouseDelta.x, canAngle.z);
        player.CameraArm.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.7f, player.transform.position.z);
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