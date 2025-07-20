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
        UIManager.aInstance.SetExp(mExp, mMaxExp); // UIManager�� ���� ����ġ �����̴� ������Ʈ
    }

    public void SetupLevel(int InLevel)
    {
        mLevel = InLevel;
        mMaxExp = MAX_EXP_FROM_LEVEL_VALUE * InLevel; // ������ ���� �ִ� ����ġ ����
        SetExp(0); // ����ġ�� 0���� �ʱ�ȭ ysh
    }

    public override void OnHit(int InDamage)
    {
        base.OnHit(InDamage);
    }
    public override void OnDie()
    {
        base.OnDie();
        FSMStageController.aInstance.ChangeState(new FSMStageStateExit()); // �׾��� �� ���� ����
    }

    void Update()
    {
       
    }

    //public override void OnGetterItem(ItemBase InItemBase) // UI ���� �� �ڵ� �ۼ� �̸��� �� ���� �� �Լ� ���ǿ��� ����ϸ� �ּ� �����ϸ�� (�ּ����� : ��ü�巡��  ctrl + /)
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
    //                AddExp(InItemBase.mItemData.Value) // UI ���� ����κ� ysh 
    //                if (mExp > mMaxExp)
    //                {
    //                    SetupLevel(mLevel + 1); // ������
    //                    FSMStageController.aInstance.ChangeState(new FSMStageStateLevelup());
    //                }

    //                SetExp(mExp); // UI ���� �ڵ� ������ �κ� ysh
    //            }
    //            break;
    //    }
    //}

    private const int MAX_EXP_FROM_LEVEL_VALUE = 10000; // ������ �ܰ迡�� �� ���� ������

}
