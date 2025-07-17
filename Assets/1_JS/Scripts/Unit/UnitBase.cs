using UnityEngine;

public class UnitData
{
    public int TotalHP = 0;
    public int HP = 0;
    public int Power = 0;
    public int Armor = 0;
}

public class UnitBase : MonoBehaviour
{
    public bool misAlive { private set; get; } // ����ִ��� ����
    public UnitData mUnitData { private set; get; } // ���� ������
    public int mUnitId { private set; get; } // ���� ID

    void Start()
    {
        
    }

    public virtual void InitUnit(int InUnitId, int InHP, int InPower, int InArmor)
    {
        mUnitId = InUnitId; // ���� ID ����
        mUnitData = new UnitData(); // ���� ������ �ʱ�ȭ
        mUnitData.TotalHP = mUnitData.HP = InHP; // �� HP�� ���� HP ����
        mUnitData.Power = InPower; // ���ݷ� ����
        mUnitData.Armor = InArmor; // ���� ����

    }

    public virtual void OnHit(int InDamage)
    {
        if(mUnitData != null)
        {
            return;
        }
    }
    public virtual void OnDie()
    {

    }

}
