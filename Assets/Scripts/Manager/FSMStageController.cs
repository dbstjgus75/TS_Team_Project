using System.Linq.Expressions;
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
        mStageFSM = new FSM(new FSMStageStateEnter());
        Debug.Log("EnterStage 실행  mStageFSM : " + mStageFSM);
    }

    public void ChangeState(FSMStateBase InFSMState)
    {
        Debug.Log("ChangeStage 안" + InFSMState + " " + mStageFSM); // mStageFSM -> FSM 임.
        if (mStageFSM != null)
        {
            Debug.Log("mStageFSM != null 들어감");
            mStageFSM.ChangeState(InFSMState); // InFSMState -> new FSMStageStateProress 를 받음
            
        }
    }

    public void OnUpdate(float InDeltaTime)
    {
        if(mStageFSM != null)
        {
            mStageFSM.OnUpdateState(InDeltaTime);
        }
    }
    public bool IsPlayGame() //BH
    {
       
        Debug.Log("IsPlayGame 함수 내부 : " + mStageFSM);
        if (mStageFSM == null)
        {
            return false;
        }
        return mStageFSM.mCurrentState.mCurrentStateType == EFSMStageStateType.StageProgress 
                || mStageFSM.mCurrentState.mCurrentStateType == EFSMStageStateType.StageBoss;

    }
    private FSM mStageFSM = null;
    private static FSMStageController sInstance = null;
}
