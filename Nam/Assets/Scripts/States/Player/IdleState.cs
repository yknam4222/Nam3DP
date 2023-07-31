using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitController
{

    public class IdleState : BaseState
    {
        private int Hash_moveAnim;
        public IdleState()
        {
            Hash_moveAnim = Animator.StringToHash("moveSpeed");
        }

        public override void OnEnterState()
        {
        }

        public override void OnUpdateState()
        {
        }

        public override void OnFixedUpdateState()
        {
            float moveSpeed = Player.Instance.moveSpeed + Player.Instance.statusSpeed;
            Player.Instance.animator.SetFloat(Hash_moveAnim, Player.Instance.Controller.inputDirection.magnitude * moveSpeed);
            Player.Instance.transform.position += Player.Instance.Controller.moveDir.normalized * Time.deltaTime  * moveSpeed;
        }

        public override void OnExitState()
        {
        }
    }
}
