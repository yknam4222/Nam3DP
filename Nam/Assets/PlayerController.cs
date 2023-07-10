using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    private float hAxis;
    private float vAxis;

    private Vector3 moveVec;

    private Animator anim;

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
}
