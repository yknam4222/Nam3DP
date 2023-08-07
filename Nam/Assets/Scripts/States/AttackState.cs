using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitController
{
    public class AttackState : BaseState
    {
        public bool isAttack { get; set; } = false;
        public int Hash_attackisBool { get; private set; }

        public AttackState()
        {
            Hash_attackisBool = Animator.StringToHash("isAttack");
        }

        public override void OnEnterState()
        {
            Player.Instance.animator.SetBool(Hash_attackisBool, true);
            isAttack = true;
        }

        public override void OnUpdateState()
        {
        }

        public override void OnFixedUpdateState()
        {
        }

        public override void OnExitState()
        {
            Player.Instance.animator.SetBool(Hash_attackisBool, false);
        }
    }
}
