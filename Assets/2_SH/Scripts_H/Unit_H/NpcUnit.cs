using UnityEngine;

public class NpcUnit : UnitBase
{
    public StageUnitData mStageUnitData { get; set;  }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int InUnitId, StageUnitData InStageUnitData)
    {
        InitUnit(InUnitId, InStageUnitData.Hp, InStageUnitData.Power, InStageUnitData.Armor);
        mStageUnitData = InStageUnitData;

        GameDataManager.aInstance.mLiveNpcUnitCount++;
    }

    public void SetSpeed(float InSpeed)
    {
        mStageUnitData.UnitSpeed = InSpeed;
        NpcUnitMovement Movement = GetComponent<NpcUnitMovement>();
        if(Movement != null)
        {
            Movement.mSpeed = InSpeed;
        }
    }

    public override void OnDie() // ysh_7-3
    {
        base.OnDie();
        GameDataManager.aInstance.mLiveNpcUnitCount = Mathf.Max(0, --GameDataManager.aInstance.mLiveNpcUnitCount);
    }
}
