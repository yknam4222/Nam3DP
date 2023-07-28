using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitController
{

    public class RollState : BaseState
    {
        public bool isRoll { get; private set; }
        public int Hash_rollTrigger { get; private set; }
        public int Hash_rollisBool { get; private set; }

        public RollState()
        {
            Hash_rollTrigger = Animator.StringToHash("RollTrigger");
            Hash_rollisBool = Animator.StringToHash("isRoll");
        }

        public override void OnEnterState()
        {
            isRoll = true;
        }

        public override void OnUpdateState()
        {
        }

        public override void OnFixedUpdateState()
        {
        }

        public override void OnExitState()
        {
        }
    }
}
