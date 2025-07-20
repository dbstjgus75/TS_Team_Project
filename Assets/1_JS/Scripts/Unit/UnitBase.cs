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
    public bool mlsAlive { set; get; } // ����ִ��� ����
    public UnitData mUnitData { set; get; } // ���� ������
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
        mlsAlive = true; // �������

    }

    public virtual void OnHit(int InDamage)
    {
        if(mUnitData == null)
        {
            return;
        }
        int HitDamage = Mathf.Max(0, InDamage - mUnitData.Armor); // ���¸�ŭ ����
        mUnitData.HP -= HitDamage; // HP ����
        if (mUnitData.HP <= 0)
        {
            OnDie();
        }
    }
    public virtual void OnDie()
    {
        mlsAlive = false; // ������� ����
    }

}
