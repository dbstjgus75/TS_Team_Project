using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private void Awake()
    {
        LevelOfSkills = new Dictionary<SkillType, int>();
        CurrentActiveSkillDatas = new Dictionary<SkillType, ActiveSkillData>();
        CurrentManualSkillDatas = new List<ActiveSkillData>();

        GameControl.aInstance.aOnMouseInput += _OnMouseInput;        
    }

    private void OnDestroy()
    {
        LevelOfSkills.Clear();
        LevelOfSkills = null;

        CurrentActiveSkillDatas.Clear();
        CurrentActiveSkillDatas = null;

        CurrentManualSkillDatas.Clear();
        CurrentManualSkillDatas = null;

        GameControl.aInstance.aOnMouseInput -= _OnMouseInput;
    }
    // Update is called once per frame
    void Update()
    {
        if(FSMStageController.aInstance.IsPlayGame() == false)
        {
            return;
        }
        foreach(var EachActiveSkll in CurrentActiveSkillDatas)
        {
            EachActiveSkll.Value.CurrentCoolTime += Time.deltaTime;
        }
        CurrentCooltime += Time.deltaTime; //JS수정
    }
    private void _OnMouseInput(int InIndex, Vector3 InMousePos)
    {
        if(FSMStageController.aInstance.IsPlayGame() == false)
        {
            return; 
        }
        if (CurrentManualSkillDatas.Count -1 < InIndex)
        {
            return; 
        }
        if (CurrentManualSkillDatas[InIndex].CurrentCoolTime < CurrentManualSkillDatas[InIndex].ActiveSkillLevelData.CoolTime)
        {
            return;
        }

        RaycastHit IHit;
        Ray IRay = Camera.main.ScreenPointToRay(InMousePos);
        int layermask = 1 << LayerMask.NameToLayer("Terrain");
        if (Physics.Raycast(IRay, out IHit, 1000, layermask))
        {
            CurrentManualSkillDatas[InIndex].FirePosition = IHit.point;
            FireSkill(CurrentManualSkillDatas[InIndex]);

            // SkillData.json 안에 있는 내용들
            //ActiveSkillData NewSkillData= new ActiveSkillData();
            //NewSkillData.Type = SkillType.ManualMissile;
            //NewSkillData.FirePosition = IHit.point;
            //NewSkillData.Cooltime = 0.5f;
            //NewSkillData.Speed = 10.0f;
            //NewSkillData.ActiveLevel = 1;
            //NewSkillData.Power = 100;
            //FireSkill(NewSkillData);
        }
    }

    public void AddSkillData(SkillType InSkillType)
    {
        SkillData ISkillData = GameDataManager.aInstance.FindSkillData(InSkillType);
        if (ISkillData == null)
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
        SkillLevelData ICurrentSkillLevelData = GameDataManager.aInstance.FindSkillLevelData(InSkillType, ICurrentSkillLevel);
        if (ICurrentSkillLevelData == null)
        {
            return;
        }

        if (CurrentActiveSkillDatas.ContainsKey(InSkillType))
        {
            if (CurrentActiveSkillDatas.ContainsKey(InSkillType) == false)
            {
                ActiveSkillData NewSkillData = new ActiveSkillData();
                NewSkillData.Type = InSkillType;
                NewSkillData.ActiveType = ISkillData.ActiveType;
                NewSkillData.CurrentCoolTime = 0.0f;
                NewSkillData.ActiveSkillLevelData = ICurrentSkillLevelData;
            }
            else
            {
                CurrentActiveSkillDatas[InSkillType].CurrentCoolTime = 0.0f;
                CurrentActiveSkillDatas[InSkillType].ActiveSkillLevelData = ICurrentSkillLevelData;
            }
        }
        switch (ISkillData.ActiveType)
        {
            case SkillActiveType.Manual:
                {
                    int IFindIndex = -1;
                    int ICurrentIndex = -0;
                    foreach (var EachSkill in CurrentManualSkillDatas)
                    {
                        if (EachSkill.Type == InSkillType)
                        {
                            IFindIndex = ICurrentIndex;
                        }
                        ICurrentIndex++;
                    }

                    if (IFindIndex >= 0)
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
        if (CurrentActiveSkillDatas.ContainsKey(InSkillType) == false)
        {
            return null;
        }
        return CurrentActiveSkillDatas[InSkillType];
    }
    public void FireSkill(ActiveSkillData InSkillData)
    {
        switch(InSkillData.Type)
        {
            case SkillType.ManualMissile:
                {
                    Vector3 ShotDirection = (InSkillData.FirePosition = transform.position).normalized;
                    Vector3 StartPos = new Vector3(transform.position.x, 1, transform.position.z);
                    FireSkillObject(InSkillData, StartPos, ShotDirection);
                }
                break; 
        }

        InSkillData.CurrentCoolTime = 0;         
    }

    public void FireSkillObject(ActiveSkillData InSkillData, Vector3 InStartPos, Vector3 InSkillDir)
    {
        SkillBase SkillObject = GamePoolManager.aInstance.DequeueSkillPool(InSkillData.Type);
        if(SkillObject == null)
        {
            //SkillBase NewSkillObjectPrefab = GameDataManager.aInstance.GetSkillObjectPrefab(InSkillData.Type, InSkillData.ActiveLevel);
            //SkillObject = GameObject.Instantiate(NewSkillObjectPrefab, GameDataManager.aInstance.GetSkillRootTransform());
            SkillBase NewSkillObjectPrefab = Resources.Load<SkillBase>("Prefabs/Missile");
            SkillObject = GameObject.Instantiate(NewSkillObjectPrefab, GameDataManager.aInstance.GetSkillRootTransform());
            if (SkillObject == null)
            {
                return; // 스킬 오브젝트가 없으면 리턴
            }
        }
        SkillObject.gameObject.SetActive(true); // 이걸 켜줘야 코르틴이 돌 수 있다
        SkillObject.FireSkill(InSkillData, InStartPos, InSkillDir);
    
    }

    public float CurrentCooltime = 0.0f;

    Dictionary<SkillType, int> LevelOfSkills;

    // 현재 사용중인 스킬 정보
    Dictionary<SkillType, ActiveSkillData> CurrentActiveSkillDatas;
    List<ActiveSkillData> CurrentManualSkillDatas;
}
