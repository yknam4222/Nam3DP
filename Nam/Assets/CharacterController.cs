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

    Animator animator;

    private void Awake()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    void Start()
    {
        moveSpeed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        setSpeed();
        LookAround();
        Move();
    }

    private void setSpeed()
    {
        if (Input.GetKey(KeyCode.W))
            moveSpeed = 3.0f;
        else
            moveSpeed = 2.0f;
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        animator.SetBool("isRun", isMove);
        animator.SetFloat("x", moveInput.x);
        animator.SetFloat("y", moveInput.y);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            transform.position += moveDir * Time.deltaTime * moveSpeed;
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
    }

}
