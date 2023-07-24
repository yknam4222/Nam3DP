using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitController
{

    public class MoveState : BaseState
    {
        public MoveState()
        {

        }

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
            Player.Instance.animator.SetFloat("x", Player.Instance.Controller.moveInput.x * (Player.Instance.MoveSpeed + Player.Instance.statusSpeed)); ;
            Player.Instance.animator.SetFloat("y", Player.Instance.Controller.moveInput.y * (Player.Instance.MoveSpeed + Player.Instance.statusSpeed)); ;
             
            Player.Instance.transform.position += Player.Instance.Controller.moveDir * Time.deltaTime * (Player.Instance.MoveSpeed + Player.Instance.statusSpeed);
        }
    }
}
