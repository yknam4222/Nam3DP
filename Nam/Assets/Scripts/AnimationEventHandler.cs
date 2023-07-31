using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitController;

public class AnimationEventHandler : MonoBehaviour
{
    private RollState rollState;
    private TargetRollState targetrollState;

    void Start()
    {
        rollState = Player.Instance.stateMachine.GetState(StateName.ROLL) as RollState;
        targetrollState = Player.Instance.stateMachine.GetState(StateName.TARGETROLL) as TargetRollState;
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
}
