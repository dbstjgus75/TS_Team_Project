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
        mCountDown = 0;
        mDurationTime = 0;

        int ICurrentStageId = GameDataManager.aInstance.mStage; // ysh_7-3 
        StageData ICurrentStageData = GameDataManager.aInstance.FindStageData(ICurrentStageId); // ysh_7-3 
        if (ICurrentStageData != null) // ysh_7-3 
        {
            foreach(StageUnitData EachNpc in ICurrentStageData.Units)
            {
                SpawnManager.aInstance.AddUnitData(EachNpc.UnitId, EachNpc);
            }
            
        }

        mCountDown = 3; // ysh
        mDurationTime = 0; // ysh

        UIManager.aInstance.ShowHUDText("Ready"); // ysh
        UIManager.aInstance.ShowLevelUpStateUI(false); // ysh
    }
    public override void OnExit()
    {
        base.OnExit();
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
                UIManager.aInstance.ShowHUDText(mCountDown.ToString());
                mCountDown--;
                Debug.Log("Count Down - " + mCountDown);
            }
            mDurationTime = 0.0f;
        }
        
    }

    private float mCountDown = 3;
    private float mDurationTime = 0;
}
