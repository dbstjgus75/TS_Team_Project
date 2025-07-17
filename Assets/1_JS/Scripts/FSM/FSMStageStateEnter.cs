using UnityEngine;

public class FSMStageStateEnter : FSMStateBase
{
    public FSMStageStateEnter() 
        : base(EFSMStageStateType.StageStart) 
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Stage State Progress Exit");
        mDurationTime = 0;
    }

    public override void OnProgress(float InDeltaTime)
    {
        base.OnProgress(InDeltaTime);
        mDurationTime += InDeltaTime;
        if(mDurationTime > 1.0f)
        {
            if (mCountDown <= 0)
            {
                FSMStageController.aInstance.ChangeState(new FSMStageStateProgress());
            }
            else
            {
                mCountDown--;
                Debug.Log("Count Down - " + mCountDown);
            }
            mDurationTime = 0.0f;
        }
        
    }

    private float mCountDown = 3;
    private float mDurationTime = 0;
}
