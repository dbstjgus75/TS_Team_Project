using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private void Awake()
    {
        LevelOfSkills = new Dictionary<SkillType, int>(); // ysh 07/23
        CurrentActiveSkillDatas = new Dictionary<SkillType, ActiveSkillData>(); // ysh 07/23
        CurrentManualSkillDatas = new List<ActiveSkillData>(); // ysh 07/23

        GameControl.aInstance.aOnMouseInput += _OnMouseInput;

    }

    private void OnDestroy()
    {
        LevelOfSkills.Clear(); // ysh 07/23
        LevelOfSkills = null; // ysh 07/23

        CurrentActiveSkillDatas.Clear();
        CurrentActiveSkillDatas = null;

        CurrentManualSkillDatas.Clear();
        CurrentManualSkillDatas = null;

        GameControl.aInstance.aOnMouseInput -= _OnMouseInput;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("ISPlayGame : " + FSMStageController.aInstance.IsPlayGame());
        if(FSMStageController.aInstance.IsPlayGame() == false) // ysh 07/23
        {
            Debug.Log("$SkillManager 34 : FSMStageController IsPlayGame() == false 적용");
            return;
        }
        foreach(var EachActiveSkill in CurrentActiveSkillDatas) // ysh 07/23
        {
            EachActiveSkill.Value.CurrentCoolTime += Time.deltaTime;
            if(EachActiveSkill.Value.CurrentCoolTime >= EachActiveSkill.Value.ActiveSkillLevelData.CoolTime)
            {
                if(EachActiveSkill.Value.ActiveType == SkillActiveType.Auto)
                {
                    FireSkill(EachActiveSkill.Value);
                }
            }
        }
        CurrentCooltime += Time.deltaTime; //JS수정
    }
    private void _OnMouseInput(int InIndex, Vector3 InMousePos)
    {
        //플레이 상태가 아닐떄는 인풋이 들어와도 무시
        if (FSMStageController.aInstance.IsPlayGame() == false) //  ysh 07/23
        {
            Debug.Log("$SkillManager 54 : IsPlayGame() = false");
            return;
        }

        // 스킬갯수보다 더 큰값에 인덱스 들어오면 종료
        if (CurrentManualSkillDatas.Count - 1 < InIndex)  // ysh 07/23
        {
            Debug.Log("$SkillManager 61 : CurrentManualSkillDatas.Count - 1 < InIndex");
            return;
        }

        if (CurrentManualSkillDatas [InIndex].CurrentCoolTime < CurrentManualSkillDatas[InIndex].ActiveSkillLevelData.CoolTime)
        {
            Debug.Log("$SkillManager 67 : CurrentManualSkillDatas [InIndex].CurrentCoolTime < CurrentManualSkillDatas[InIndex].ActiveSkillLevel.CoolTime");
            return;
        }

        RaycastHit IHit;
        Ray IRay = Camera.main.ScreenPointToRay(InMousePos);
        int layermask = 1 << LayerMask.NameToLayer("Terrain");
        if (Physics.Raycast(IRay, out IHit, 1000, layermask))
        {
            CurrentManualSkillDatas[InIndex].FirePosition = IHit.point; // ysh 07/23
            FireSkill(CurrentManualSkillDatas[InIndex]); // ysh 07/23
        }
    }

    public void AddSkillData(SkillType InSkillType) // ysh 07/23
    {
        SkillData ISkillData = GameDataManager.aInstance.FindSkillData(InSkillType);
        if(ISkillData == null)
        {
            return;
        }
        if (LevelOfSkills.ContainsKey(InSkillType))
        {
            LevelOfSkills[InSkillType]++;
        }
        else
        {
            LevelOfSkills.Add(InSkillType, 1);
        }

        int ICurrentSkillLevel = LevelOfSkills[InSkillType];
        SkillLevelData ICurrentSkillLevelData = GameDataManager.aInstance.FindSkillLevleData(InSkillType, ICurrentSkillLevel);
        if(ICurrentSkillLevelData == null)
        {
            return;
        }

        if (CurrentActiveSkillDatas.ContainsKey(InSkillType) == false)
        {
            ActiveSkillData NewSkillData = new ActiveSkillData();
            NewSkillData.Type = InSkillType;
            NewSkillData.ActiveType = ISkillData.ActiveType;
            NewSkillData.CurrentCoolTime = 0.0f;
            NewSkillData.ActiveSkillLevelData = ICurrentSkillLevelData;

            CurrentActiveSkillDatas.Add(InSkillType, NewSkillData);
        }
        else
        {
            CurrentActiveSkillDatas[InSkillType].CurrentCoolTime = 0.0f;
            CurrentActiveSkillDatas[InSkillType].ActiveSkillLevelData = ICurrentSkillLevelData; 
        }

        switch (ISkillData.ActiveType)
        {
            case SkillActiveType.Manual:
                {
                    int IFindIndex = -1;
                    int ICurrentIndex = 0;
                    foreach(var EachSkill in CurrentManualSkillDatas)
                    {
                        if(EachSkill.Type == InSkillType)
                        {
                            IFindIndex = ICurrentIndex;
                        }
                        ICurrentIndex++;
                    }

                    if(IFindIndex >= 0)
                    {
                        CurrentManualSkillDatas[IFindIndex] = CurrentActiveSkillDatas[InSkillType];
                    }
                    else
                    {
                        CurrentManualSkillDatas.Add(CurrentActiveSkillDatas[InSkillType]);
                    }

                }
                break;
        }
    }

    public ActiveSkillData GetCurrentSkillData(SkillType InSkillType)
    {
        if(CurrentActiveSkillDatas.ContainsKey(InSkillType) == false)
        {
            return null;
        }
        return CurrentActiveSkillDatas[InSkillType];
    }
    public void FireSkill(ActiveSkillData InSkillData)
    {
        switch (InSkillData.Type)
        {
            case SkillType.Missile: // ysh 07/23
                {
                    for(int fireAngle = 0; fireAngle < 360; fireAngle += 10)
                    {
                        Vector3 ShotDirection = new Vector3(Mathf.Cos(fireAngle * Mathf.Deg2Rad),
                                                            1,
                                                            Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                        Vector3 StartPos = new Vector3(transform.position.x, 1, transform.position.z);

                        FireSkillObject(InSkillData, StartPos, ShotDirection);
                    }
                }
                break;
            case SkillType.ManualMissile:
                {
                    Vector3 ShotDirection = (InSkillData.FirePosition - transform.position).normalized;
                    Vector3 StartPosition = new Vector3(transform.position.x, 1, transform.position.z);
                    FireSkillObject(InSkillData, StartPosition, ShotDirection);
                }
                break;
        }
        InSkillData.CurrentCoolTime = 0;
    }

    public void FireSkillObject(ActiveSkillData InSkillData, Vector3 InStartPos, Vector3 InSkillDir)
    {
        SkillBase SkillObject = GamePoolManager.aInstance.DequeueSkillPool(InSkillData.Type);
        if (SkillObject == null)
        {
            Debug.Log($"[SkillManager] Prefab Load Try: Type={InSkillData.Type}, Level=1");
            SkillBase NewSkillObjectPrefab = GameDataManager.aInstance.GetSkillObjectPrefab(InSkillData.Type, InSkillData.ActiveSkillLevelData.Level);
            if (NewSkillObjectPrefab == null)
            {
                Debug.LogError("[SkillManager] Failed to load Skill Prefab!");
            }
            SkillObject = GameObject.Instantiate(NewSkillObjectPrefab, GameDataManager.aInstance.GetSkillRootTransform());
            //SkillBase NewSkillObjectPrefab = Resources.Load<SkillBase>("Prefabs/Missile");
            //SkillObject = GameObject.Instantiate(NewSkillObjectPrefab, GameDataManager.aInstance.GetSkillRootTransform());
            if (SkillObject == null)
            {
                return; // 스킬 오브젝트가 없으면 리턴
            }
        }
        SkillObject.gameObject.SetActive(true); // 이걸 켜줘야 코르틴이 돌 수 있다
        SkillObject.mSkillType = InSkillData.Type;
        SkillObject.FireSkill(InSkillData, InStartPos, InSkillDir);
    
    }

    public float CurrentCooltime = 0.0f;

    // 스킬 타입별 레벨 정보 ysh 07/23
    Dictionary<SkillType, int> LevelOfSkills; 

    // 현재 사용중인 스킬 정보 ysh 07/23
    Dictionary<SkillType, ActiveSkillData> CurrentActiveSkillDatas;
    List<ActiveSkillData> CurrentManualSkillDatas;
}
