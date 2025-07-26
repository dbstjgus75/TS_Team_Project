using System;
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

        SkillDatas.Clear(); // JS_6-2 
        SkillDatas =null; // JS

        StageDatas.Clear(); // ysh
        StageDatas = null; // ysh
        SkillResources.Clear(); // JS   
        SkillResources = null; // JS


        mLiveNpcUnitCount = 0; // ysh_7-3

        mCurrentGameTime = 0; // ysh
    }

    public void LoadAll() // ysh
    {
        LoadSkillData(); // JS        
        Debug.Log("LoadAll 호출");
        LoadStageData();
        Debug.Log("LoadStageData 호출");
    }

    public void LoadSkillData() // JS
    {
        SkillDatas = new Dictionary<SkillType, SkillData>(); 
        SkillResources = new Dictionary<string, SkillBase>();
        SkillDatas.Clear();
        SkillResources.Clear();

        TextAsset SkillJsonTextAsset = Resources.Load<TextAsset>("Data/SkillDatas");
        string IskillJson = SkillJsonTextAsset.text;
        JObject IDataObject = JObject.Parse(IskillJson);
        JToken IToken = IDataObject["Skills"];
        JArray IArray = IToken.Value<JArray>();

        foreach (JObject EachObject in IArray)
        {
            SkillData NewSkillData = new SkillData();
            NewSkillData.Type = Enum.Parse<SkillType>(EachObject.Value<string>("Type"));
            NewSkillData.ActiveType = Enum.Parse<SkillActiveType>(EachObject.Value<String>("ActiveType"));
            NewSkillData.LevelDatas = new Dictionary<int, SkillLevelData>();
            JArray ILevelArray = EachObject.Value<JArray>("LevelDatas");
            foreach (JObject EachLevel in ILevelArray)
            {
                SkillLevelData NewSkillLevelData = new SkillLevelData();
                NewSkillLevelData.Type = NewSkillData.Type;
                NewSkillLevelData.Level = EachLevel.Value<int>("Level");
                NewSkillLevelData.Path = EachLevel.Value<string>("Path");
                NewSkillLevelData.Power = EachLevel.Value<int>("Power");
                NewSkillLevelData.Size = EachLevel.Value<int>("Size");
                NewSkillLevelData.Speed = EachLevel.Value<float>("Speed");
                NewSkillLevelData.ActiveTime = EachLevel.Value<float>("ActiveTime");
                NewSkillLevelData.CoolTime = EachLevel.Value<float>("CoolTime");

                NewSkillData.LevelDatas.Add(NewSkillLevelData.Level, NewSkillLevelData);

                SkillBase SkillObject = Resources.Load<SkillBase>(NewSkillLevelData.Path);
                string SkillId = GetSkillId(NewSkillLevelData);
                SkillResources.Add(SkillId, SkillObject);
            }
            SkillDatas.Add(NewSkillData.Type, NewSkillData);
        }
    }

    public string GetSkillId(SkillLevelData InSkillLevelData)
    {
        return GetSkillId(InSkillLevelData.Type, InSkillLevelData.Level);
    }

    public string GetSkillId(SkillType InSkillType, int InLevel)
    {
        return string.Format("{0}_{1}", InSkillType.ToString(), InLevel);
    }


    public SkillData FindSkillData(SkillType InSkillType)
    {
        if (SkillDatas.ContainsKey(InSkillType) == false)
        {
            return null;
        }
        return SkillDatas[InSkillType];
    }
    public SkillLevelData FindSkillLevleData(SkillType InSkillType, int InSkillLevel)
    {
        if (SkillDatas.ContainsKey(InSkillType) == false)
        {
            return null;
        }        
        if (SkillDatas[InSkillType].LevelDatas.ContainsKey(InSkillLevel) == false)
        {
            return null;
        }
        return SkillDatas[InSkillType].LevelDatas[InSkillLevel];
    }

    public SkillBase GetSkillObjectPrefab(SkillLevelData InSkillLevelData)
    {
        return GetSkillObjectPrefab(GetSkillId(InSkillLevelData));
    }

    public SkillBase GetSkillObjectPrefab(SkillType InType, int InSkillLevel)
    {
        return GetSkillObjectPrefab(GetSkillId(InType, InSkillLevel));
    }
    public SkillBase GetSkillObjectPrefab(string InSkillId)
    {
        if (SkillResources.ContainsKey(InSkillId) == false)
        {
            return null;
        }
        return SkillResources[InSkillId];
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
    
    private Dictionary<SkillType, SkillData> SkillDatas = null; // JS  
    private Dictionary<string, SkillBase> SkillResources = null; // JS


    private float mCurrentGameTime = 0.0f;
}
