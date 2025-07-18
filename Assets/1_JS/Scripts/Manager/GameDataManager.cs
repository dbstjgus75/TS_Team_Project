using UnityEngine;

public class GameDataManager
{
    public int mStage { get; private set; }
    public static GameDataManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameDataManager();
            }
            return sInstance;
        }
    }

    public GameObject GetMyPCObject()
    {
        return mMyPc;
    }
    public Transform GetSpawnRootTransform()
    {
        return mSpawnRoot;
    }
    public Transform GetSkillRootTransform()
    {
        return mSkillRoot;
    }
    public void Init()
    {
        ClearGameTime(); // ysh
    }
    public void Clear()
    {
        mStage = 0;
        mMyPc = null;
        mSpawnRoot = null;
        mItemRoot = null;
        mSkillRoot = null;

        mCurrentGameTime = 0; // ysh
    }
    public void SetStageData(GameObject InMyPc, Transform InSpawnRoot, Transform InSkillRoot, Transform InItemRoot)
    {
        mMyPc = InMyPc;
        mSpawnRoot = InSpawnRoot;
        mSkillRoot = InSkillRoot;
        mItemRoot = InItemRoot;
    }
    public void SetCurrentStage(int InStage)
    {
        mStage = InStage;
    }

    // 게임 시간과 관련된 구현사항 적용 ysh
    public void UpdateGameTime(float InDeltaTime) // ysh
    {
        mCurrentGameTime += InDeltaTime;
    }
    public float GetGameTime() // ysh
    {
        return mCurrentGameTime;
    }

    public void ClearGameTime() // ysh
    {
        mCurrentGameTime = 0.0f;
    }

    private static GameDataManager sInstance = null;

    private GameObject mMyPc;
    private Transform mSpawnRoot;
    private Transform mSkillRoot;
    private Transform mItemRoot;

    private float mCurrentGameTime = 0.0f;
}
