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

    public void SetupLevel(int InLevel)
    {
        mLevel = InLevel;
        mExp = 0;
        mMaxExp = MAX_EXP_FROM_LEVEL_VALUE * InLevel; // ������ ���� �ִ� ����ġ ����
    }

    public override void OnHit(int InDamage)
    {
        base.OnHit(InDamage);
    }
    public override void OnDie()
    {
        base.OnDie();
    }

    void Update()
    {
       
    }

    private const int MAX_EXP_FROM_LEVEL_VALUE = 10000; // ������ �ܰ迡�� �� ���� ������
}
