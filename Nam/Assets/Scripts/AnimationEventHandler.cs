using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitController;

public class AnimationEventHandler : MonoBehaviour
{
    private RollState rollState;
    private TargetRollState targetrollState;
    private AttackState attackState;

    void Start()
    {
        rollState = Player.Instance.stateMachine.GetState(StateName.ROLL) as RollState;
        targetrollState = Player.Instance.stateMachine.GetState(StateName.TARGETROLL) as TargetRollState;
        attackState = Player.Instance.stateMachine.GetState(StateName.ATTACK) as AttackState;
    }

    void Update()
    {
        
    }

    public void OnFinishedDash()
    {
        rollState.isRoll = false;
        Player.Instance.stateMachine.ChangeState(StateName.IDLE);
        rollState.OnExitState();
    }

    public void OnFinishedTargetRoll()
    {
        targetrollState.isTargetRoll = false;
        Player.Instance.stateMachine.ChangeState(StateName.IDLE_TARGET);
        targetrollState.OnExitState();
    }

    public void OnFinishedAttack()
    {
        attackState.isAttack = false;
        if(Player.Instance.stateMachine.PastState is IdleState)
            Player.Instance.stateMachine.ChangeState(StateName.IDLE);
        else if(Player.Instance.stateMachine.PastState is TargetState)
            Player.Instance.stateMachine.ChangeState(StateName.IDLE_TARGET);
        attackState.OnExitState();
    }
}
