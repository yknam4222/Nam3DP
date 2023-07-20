using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    public Player player { get; private set; }

    private bool isGround;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        
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