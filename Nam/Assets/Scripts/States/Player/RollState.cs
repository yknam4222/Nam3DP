using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitController
{
    public class RollState : BaseState
    {
        public bool isRoll { get;  set; }
        public int Hash_rollTrigger { get; private set; }
        public int Hash_rollisBool { get; private set; }

        public RollState()
        {
            Hash_rollisBool = Animator.StringToHash("isRoll");
            Hash_rollTrigger = Animator.StringToHash("RollTrigger");
        }

        public override void OnEnterState()
        {
            isRoll = true;
            Roll();
        }

        private void Roll()
        {
            Player.Instance.animator.SetTrigger(Hash_rollTrigger);
            Player.Instance.animator.SetBool(Hash_rollisBool, true);
        }
        public override void OnUpdateState()
        {
        }

        public override void OnFixedUpdateState()
        {
        }

        public override void OnExitState()
        {
            Player.Instance.animator.ResetTrigger(Hash_rollTrigger);
            Player.Instance.animator.SetBool(Hash_rollisBool, false);
        }
    }
}
