using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;

    [SerializeField]
    private Transform cameraArm;

    private float moveSpeed;
    private float statusSpeed;

    Animator animator;
    Rigidbody rigid;

    bool isMove;
    bool isGround;
    bool isJump;

    private void Awake()
    {
        animator = characterBody.GetComponent<Animator>();
        rigid = characterBody.GetComponent<Rigidbody>();
    }

    void Start()
    {
        moveSpeed = 3.0f;
        statusSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        setSpeed();
        setAnim();
        LookAround();
        Move();
        Jump();

        changeIdle();
    }

    private void setSpeed()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveInput.x != 0 && moveInput.y == 0)
            statusSpeed = -1.5f;
        else if (moveInput.y > 0)
            statusSpeed = 0.0f;
        else if (moveInput.y < 0)
            statusSpeed = -1.0f;
    }

    private void setAnim()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (moveSpeed < 6.0f)
                moveSpeed += Time.deltaTime * 3.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            StartCoroutine(setspeed());
    }

    private void changeIdle()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger("EquipBow");
            animator.SetBool("isBow", true);
        }
    }

    IEnumerator setspeed()
    {
        while (moveSpeed > 3.0f)
        {
            moveSpeed -= Time.deltaTime * 3.0f;

            yield return null;
        }
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isMove = moveInput.magnitude != 0;
        animator.SetFloat("x", moveInput.x * (moveSpeed + statusSpeed));
        animator.SetFloat("y", moveInput.y * (moveSpeed + statusSpeed));
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            transform.position += moveDir * Time.deltaTime * (moveSpeed + statusSpeed);
        }
    }

    private void Jump()
    {
        isGround = Physics.Raycast(characterBody.position, Vector3.down, 0.1f, LayerMask.GetMask("Ground"));

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);

            if ((moveSpeed + statusSpeed) > 5.0f)
                animator.SetTrigger("isRunJump");
            else
                animator.SetTrigger("isJump");
        }

    }
    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * 2.0f, Input.GetAxis("Mouse Y") * 2.0f);
        Vector3 canAngle = cameraArm.rotation.eulerAngles;

        float x = canAngle.x - mouseDelta.y;

        if (x < 180f)
            x = Mathf.Clamp(x, -1f, 70f);
        else
            x = Mathf.Clamp(x, 335f, 361f);

        cameraArm.rotation = Quaternion.Euler(x, canAngle.y + mouseDelta.x, canAngle.z);
        cameraArm.position = new Vector3(characterBody.position.x, characterBody.position.y + 1.7f, characterBody.position.z);
    }

}
