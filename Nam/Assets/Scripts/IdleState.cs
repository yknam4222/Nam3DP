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
            Move();
        }

        public override void OnExitState()
        {
        }

        private void Move()
        {
            Player.Instance.animator.SetFloat("x", Player.Instance.Controller.moveInput.x * (Player.Instance.moveSpeed + Player.Instance.statusSpeed)); ;
            Player.Instance.animator.SetFloat("y", Player.Instance.Controller.moveInput.y * (Player.Instance.moveSpeed + Player.Instance.statusSpeed)); ;
             
            Player.Instance.transform.position += Player.Instance.Controller.moveDir * Time.deltaTime * (Player.Instance.moveSpeed + Player.Instance.statusSpeed);
        }
    }
}
