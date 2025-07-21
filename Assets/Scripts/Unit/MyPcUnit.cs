using UnityEngine;

public class MyPcUnit : UnitBase
{
    public int mMaxExp { get; set; }
    public int mExp { get; set; }
    public int mLevel { get; set; }
    
    void Start()
    {
        
    }

    public override void InitUnit(int InUnitId, int InHP, int InPower, int InArmor)
    {
        base.InitUnit(InUnitId, InHP, InPower, InArmor);
        mExp = 0;
        mMaxExp = 10000;
        mLevel = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {

    }

    public void AddExp(int InAddExp) // ysh
    {
        mExp += InAddExp;
        UIManager.aInstance.SetExp(mExp, mMaxExp); 
    }


    public void SetExp(int InExp) // ysh
    {
        mExp = InExp;
        UIManager.aInstance.SetExp(mExp, mMaxExp); // UIManager를 통해 경험치 슬라이더 업데이트
    }

    public void SetupLevel(int InLevel)
    {
        mLevel = InLevel;
        mMaxExp = MAX_EXP_FROM_LEVEL_VALUE * InLevel; // 레벨에 따라 최대 경험치 설정
        SetExp(0); // 경험치를 0으로 초기화 ysh
    }

    public override void OnHit(int InDamage)
    {
        base.OnHit(InDamage);
    }
    public override void OnDie()
    {
        base.OnDie();
        FSMStageController.aInstance.ChangeState(new FSMStageStateExit()); // 죽었을 때 상태 변경
    }

    void Update()
    {
       
    }

    //public override void OnGetterItem(ItemBase InItemBase) // UI 강의 전 코드 작성 미리함 이 만약 이 함수 강의에서 사용하면 주석 제거하면됨 (주석제거 : 전체드래그  ctrl + /)
    //{
    //    base.OnGettterItem(InItemBase);
    //    if(InItemBase == null)
    //    {
    //        return;
    //    }

    //    switch (InItemBase.mItemData.Type)
    //    {
    //        case EltemType.Exp:
    //            {
    //                AddExp(InItemBase.mItemData.Value) // UI 강의 변경부분 ysh 
    //                if (mExp > mMaxExp)
    //                {
    //                    SetupLevel(mLevel + 1); // 레벨업
    //                    FSMStageController.aInstance.ChangeState(new FSMStageStateLevelup());
    //                }

    //                SetExp(mExp); // UI 강의 코드 변경한 부분 ysh
    //            }
    //            break;
    //    }
    //}

    private const int MAX_EXP_FROM_LEVEL_VALUE = 10000; // 컴파일 단계에서 이 수는 고정됨

}
