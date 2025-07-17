using UnityEditor;
using UnityEngine;

public class FSMStageController
{
    public static FSMStageController aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new FSMStageController();
            }
            return sInstance;
        }
    }
    public void Init()
    {

    }
    public void Clear()
    {

    }
    public void EnterStage()
    {
        mStageFSM = new FSM(new FSMStateBase(EFSMStageStateType.StageStart));
    }

    public void OnUpdate(float InDeltaTime)
    {

    }
    private FSM mStageFSM = null;
    private static FSMStageController sInstance = null;
}
