using UnityEngine;

public class FSMStateBase
{
    public EFSMStageStateType mCurrentStateType { get; protected set; }
    public FSMStateBase(EFSMStageStateType InType)
    {
        mCurrentStateType = InType;
    }

    public virtual void OnEnter()
    {
        // Override this method in derived classes to handle state entry logic
    }
    public virtual void OnExit()
    {
        // Override this method in derived classes to handle state exit logic
    }
    public virtual void OnProgress(float InDeltaTime)
    {
        // Override this method in derived classes to handle state progress logic
    }
}
