using UnityEngine;

public class FSMStageStateProgress : FSMStateBase
{
    public FSMStageStateProgress() 
        : base(EFSMStageStateType.StageProgress) 
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Stage State Progress Enter");

        mNowSpawn = 10; // ysh_7-3
        mDurationTime = 0.0f; // ysh_7-3
        mNextSpawnTime = 0.0f; // ysh_7-3

        mMyPcObj = GameDataManager.aInstance.GetMyPCObject(); // ysh_7-3
        mSpawnRoot = GameDataManager.aInstance.GetSpawnRootTransform(); // ysh_7-3

        int CurrentStage = GameDataManager.aInstance.mStage; // ysh_7-3
        StageData CurrentStageData = GameDataManager.aInstance.FindStageData(CurrentStage); // ysh_7-3
        if (CurrentStageData != null) // ysh_7-3
        {
            mMaxSpawn = CurrentStageData.MaxSpawnCount;
        }

        UIManager.aInstance.HideHUDText(); // ysh
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnProgress(float InDeltaTime)
    {
        base.OnProgress(InDeltaTime);

        mDurationTime += InDeltaTime; // ysh_7-3
        bool bSpawn = false; // ysh_7-3
        if (mDurationTime > mNextSpawnTime) // ysh_7-3
        {
            mNowSpawn = 1;
            mNowSpawn = Mathf.Min(mNowSpawn, mMaxSpawn);
            mNextSpawnTime += 3.0f;
            bSpawn = true;
        }
        
        if (bSpawn) // ysh_7-3
        {
            if(GameDataManager.aInstance.mLiveNpcUnitCount > 100)
            {
                return;
            }
            Vector3 IPivotPos = mMyPcObj.transform.position;

            for(int i=0; i < mNowSpawn; i++)
            {
                NpcUnit NewSpawnUnit = SpawnManager.aInstance.GetRandomUnitData();
                Vector2 IRandomCircle = Random.insideUnitCircle.normalized; // insideUnitCircle.normalized; 플레이어와 거리를 두고 원 형태로 적을 생성하는 함수
                float IRandomFactor = Random.Range(10.0f, 12.0f);
                Vector3 ISpawnPosition = IPivotPos + new Vector3(IRandomCircle.x * IRandomFactor, 5, IRandomCircle.y * IRandomFactor);
                SpawnManager.aInstance.SpawnNpc(NewSpawnUnit.mStageUnitData.UnitId, mSpawnRoot, ISpawnPosition);
            }
        }
        
        if(GameDataManager.aInstance.GetGameTime() > GAME_END_SECONDS) // ysh
        {
            FSMStageController.aInstance.ChangeState(new FSMStageStateExit());
            return;
        }
        GameDataManager.aInstance.UpdateGameTime(InDeltaTime); // ysh
    }

    private GameObject mMyPcObj; // ysh_7-3
    private Transform mSpawnRoot; // ysh_7-3

    private float mDurationTime = 0.0f;
    private int mNowSpawn = 0;
    private int mMaxSpawn = 0;
    private float mNextSpawnTime = 0.0f;

    private const int GAME_END_SECONDS = 100; // ysh
}
