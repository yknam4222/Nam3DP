using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitController
{
    public class TargetRollState : BaseState
    {
        public bool isTargetRoll { get; set; }
        public int Hash_targetRollTrigger { get; private set; }
        public int Hash_targetRollBool { get; private set; }

        public Vector3 dashDirection { get; private set; }

        public TargetRollState()
        {
            Hash_targetRollBool = Animator.StringToHash("isTargetRoll");
            Hash_targetRollTrigger = Animator.StringToHash("TargetRollTrigger");
        }

        public override void OnEnterState()
        {
            isTargetRoll = true;
            TargetRoll();
        }

        private void TargetRoll()
        {
            dashDirection = new Vector3(Player.Instance.Controller.inputDirection.x, 0.0f, Player.Instance.Controller.inputDirection.z);
            Player.Instance.transform.forward = dashDirection;
           // Player.Instance.Controller.LookAt(new Vector3(dashDirection.x, 0.0f, dashDirection.z));

            Player.Instance.animator.SetTrigger(Hash_targetRollTrigger);
            Player.Instance.animator.SetBool(Hash_targetRollBool, true);
        }

        public override void OnUpdateState()
        {
        }

        public override void OnFixedUpdateState()
        {
        }

        public override void OnExitState()
        {
            Player.Instance.animator.ResetTrigger(Hash_targetRollTrigger);
            Player.Instance.animator.SetBool(Hash_targetRollBool, false);
        }
    }
}

