using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject Target;
    [SerializeField]
    private Transform mainCamera;

    private Vector3 asd;

    void Start()
    {
        Target = Player.Instance.gameObject;
    }
    private void Update()
    {
        if (!Player.Instance.Controller.isTargetting)
            LookAround();
        else
            TargetLook();
        
    }
    void FixedUpdate()
    {
        SetPos();
        //transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 2.5f, Target.transform.position.z - 4.5f);
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * 2.0f, Input.GetAxis("Mouse Y") * 2.0f);
        Vector3 canAngle = transform.rotation.eulerAngles;

        float x = canAngle.x - mouseDelta.y;

        if (x < 180f)
            x = Mathf.Clamp(x, -1f, 70f);
        else
            x = Mathf.Clamp(x, 335f, 361f);

        transform.rotation = Quaternion.Euler(x, canAngle.y + mouseDelta.x, canAngle.z);
        //transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 1.7f, Target.transform.position.z);
    }

    private void SetPos()
    {
        Vector3 TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y + 1.7f, Target.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 5.0f);
    }

    private void TargetLook()
    {
        Vector3 dir = Player.Instance.Controller.targetEnemy.position - transform.position;
        transform.GetChild(0).rotation= Quaternion.Lerp(transform.GetChild(0).rotation, Quaternion.LookRotation(dir), 0.1f);
        transform.rotation = transform.GetChild(0).rotation;
        //transform.LookAt(Player.Instance.Controller.targetEnemy);
    }

}
