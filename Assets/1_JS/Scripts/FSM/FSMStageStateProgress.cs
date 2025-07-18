using UnityEngine;

public class FSMStageStateProgress : FSMStateBase
{
    public FSMStageStateProgress() 
        : base(EFSMStageStateType.StageStart) 
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Stage State Progress Enter");
        UIManager.aInstance.HideHUDText(); // ysh
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnProgress(float InDeltaTime)
    {
        base.OnProgress(InDeltaTime);
        
        if(GameDataManager.aInstance.GetGameTime() > GAME_END_SECONDS) // ysh
        {
            FSMStageController.aInstance.ChangeState(new FSMStageStateExit());
            return;
        }
        GameDataManager.aInstance.UpdateGameTime(InDeltaTime); // ysh
    }

    private const int GAME_END_SECONDS = 100; // ysh
}
