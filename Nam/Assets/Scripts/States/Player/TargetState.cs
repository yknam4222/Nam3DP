using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitController
{
    public class TargetState : BaseState
    {
        public bool isTarget { get; set; }
        public int Hash_targetBool { get; private set; }

        public TargetState()
        {
            Hash_targetBool = Animator.StringToHash("isTarget");
        }

        public override void OnEnterState()
        {
            Player.Instance.animator.SetBool(Hash_targetBool, true);
        }

        public override void OnUpdateState()
        {
        }

        public override void OnFixedUpdateState()
        {
            float moveSpeed = Player.Instance.moveSpeed + Player.Instance.statusSpeed;
            Player.Instance.animator.SetFloat("x", Player.Instance.Controller.inputDirection.x * moveSpeed);
            Player.Instance.animator.SetFloat("y", Player.Instance.Controller.inputDirection.z * moveSpeed);
            Player.Instance.transform.position += Player.Instance.Controller.moveDir.normalized * Time.deltaTime * moveSpeed;
        }

        public override void OnExitState()
        {
            if(!Player.Instance.Controller.isTargetting)
            Player.Instance.animator.SetBool(Hash_targetBool, false);
        }
    }
}