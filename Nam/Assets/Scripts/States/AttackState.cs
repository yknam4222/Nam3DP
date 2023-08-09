using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitController
{
    public class AttackState : BaseState
    {
        public bool isAttack { get; set; } = false;
        public int Hash_attackisBool { get; private set; }
        public int Hash_attackNum { get; private set; }

        public AttackState()
        {
            Hash_attackisBool = Animator.StringToHash("isAttack");
            Hash_attackNum = Animator.StringToHash("AttackNum");
        }

        public override void OnEnterState()
        {
            int attacknum = Random.Range(1, 3);
            Player.Instance.animator.SetInteger(Hash_attackNum, attacknum);
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
