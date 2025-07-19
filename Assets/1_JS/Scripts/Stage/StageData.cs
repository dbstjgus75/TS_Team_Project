using UnityEngine;

public enum EFSMStageStateType
{
    None,
    StageStart,
    StageProgress,
    StageLevelup,
    StageBoss,
    StageEnd,
}

public class StageUnitData // ysh
{
    public string UnitId;
    public string UnitPath;
    public float UnitSpeed;
    public int Hp;
    public int Power;
    public int Aromor;
}