using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class GameDataManager
{
    public int mStage { get; private set; }
    public int mLiveNpcUnitCount { get; set; } = 0; // ysh_7-3
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
        ClearGameTime(); // ysh UI
    }
    public void Clear()
    {
        mStage = 0;
        mMyPc = null;
        mSpawnRoot = null;
        mItemRoot = null;
        mSkillRoot = null;

        StageDatas.Clear(); // ysh
        StageDatas = null; // ysh

        mLiveNpcUnitCount = 0; // ysh_7-3

        mCurrentGameTime = 0; // ysh
    }

    public void LoadAll() // ysh
    {
        Debug.Log("LoadAll 호출");
        LoadStageData();
        Debug.Log("LoadStageData 호출");
    }

    protected void LoadStageData() //ysh
    {
        StageDatas = new Dictionary<int, StageData>();
        StageDatas.Clear();

        TextAsset StageJsonTextAsset = Resources.Load<TextAsset>("Data/StageDatas");
        string IStageJson = StageJsonTextAsset.text; 
        JObject IStageDataObject = JObject.Parse(IStageJson);
        JToken IStageToken = IStageDataObject["Stages"];
        JArray IStageArray = IStageToken.Value<JArray>();

        foreach(JObject EachObject in IStageArray)
        {
            StageData NewStageData = new StageData();
            NewStageData.StageId = EachObject.Value<int>("StageId");
            NewStageData.MaxSpawnCount = EachObject.Value<int>("MaxSpawn");
            NewStageData.DropId = EachObject.Value<string>("DropId");
            JArray INpcArray = EachObject.Value<JArray>("UnitPaths");
            foreach(JObject EachNpc in INpcArray)
            {
                StageUnitData UnitData = new StageUnitData();
                UnitData.UnitId = EachNpc.Value<string>("Id");
                UnitData.UnitPath = EachNpc.Value<string>("Path");
                UnitData.UnitSpeed = EachNpc.Value<float>("Speed");
                UnitData.Hp = EachNpc.Value<int>("Hp");
                UnitData.Armor = EachNpc.Value<int>("Armor");
                UnitData.Power = EachNpc.Value<int>("Power");
                NewStageData.Units.Add(UnitData);
            }
            StageDatas.Add(NewStageData.StageId, NewStageData);
        }
    }

    public StageData FindStageData(int InStageId)
    {
        if(StageDatas.ContainsKey(InStageId) == false)
        {
            return null;
        }
        return StageDatas[InStageId];
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

    private Dictionary<int, StageData> StageDatas = null; // ysh

    private float mCurrentGameTime = 0.0f;
}
