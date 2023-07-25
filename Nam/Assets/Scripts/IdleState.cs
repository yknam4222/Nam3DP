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
            Player.Instance.Controller.LookAt(Player.Instance.Controller.inputDirection);
            Player.Instance.rigidBody.velocity = Player.Instance.Controller.inputDirection * 3.0f;
        }

        public override void OnExitState()
        {
        }
    }
}
