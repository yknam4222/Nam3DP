using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitController;

public class AnimationEventHandler : MonoBehaviour
{
    private RollState rollState;

    void Start()
    {
        rollState = Player.Instance.stateMachine.GetState(StateName.ROLL) as RollState;
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
}
