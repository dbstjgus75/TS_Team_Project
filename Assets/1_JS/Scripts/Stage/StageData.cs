using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
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
    public int Armor;
}

public class StageData // ysh
{
    public int StageId;
    public int MaxSpawnCount;
    public string DropId;
    public List<StageUnitData> Units = new List<StageUnitData>();
}