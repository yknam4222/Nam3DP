using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitController
{

    public class IdleState : BaseState
    {
        
        public override void OnEnterState()
        {
        }

        public override void OnUpdateState()
        {
        }

        public override void OnFixedUpdateState()
        {
            float moveSpeed = Player.Instance.moveSpeed + Player.Instance.statusSpeed;
            Player.Instance.animator.SetFloat("x", Player.Instance.Controller.inputDirection.x * moveSpeed);
            Player.Instance.animator.SetFloat("y", Player.Instance.Controller.inputDirection.z * moveSpeed);
            Player.Instance.transform.position += Player.Instance.Controller.moveDir.normalized * Time.deltaTime  * moveSpeed;
        }

        public override void OnExitState()
        {
        }
    }
}
