using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject Target;
    [SerializeField]
    private Transform mainCamera;

    void Start()
    {
        Target = Player.Instance.gameObject;
    }

    void FixedUpdate()
    {
        if (!Player.Instance.Controller.isTargetting)
            LookAround();
        else
            TargetLook();
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

        Vector3 TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y + 1.7f, Target.transform.position.z);
        transform.rotation = Quaternion.Euler(x, canAngle.y + mouseDelta.x, canAngle.z);
        //transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 5.0f);
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 1.7f, Target.transform.position.z);
    }

    private void TargetLook()
    {
        Vector3 TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y + 1.7f, Target.transform.position.z);
        transform.rotation = Quaternion.Euler(Player.Instance.Controller.inputDirection.x, 0.0f, Player.Instance.Controller.inputDirection.z);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 5.0f);
    }

}
