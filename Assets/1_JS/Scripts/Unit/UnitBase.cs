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
    public bool mlsAlive { set; get; } // 살아있는지 여부
    public UnitData mUnitData { set; get; } // 유닛 데이터
    public int mUnitId { private set; get; } // 유닛 ID

    void Start()
    {
        
    }

    public virtual void InitUnit(int InUnitId, int InHP, int InPower, int InArmor)
    {
        mUnitId = InUnitId; // 유닛 ID 설정
        mUnitData = new UnitData(); // 유닛 데이터 초기화
        mUnitData.TotalHP = mUnitData.HP = InHP; // 총 HP와 현재 HP 설정
        mUnitData.Power = InPower; // 공격력 설정
        mUnitData.Armor = InArmor; // 방어력 설정
        mlsAlive = true; // 살아있음

    }

    public virtual void OnHit(int InDamage)
    {
        if(mUnitData == null)
        {
            return;
        }
        int HitDamage = Mathf.Max(0, InDamage - mUnitData.Armor); // 방어력만큼 감소
        mUnitData.HP -= HitDamage; // HP 감소
        if (mUnitData.HP <= 0)
        {
            OnDie();
        }
    }
    public virtual void OnDie()
    {
        mlsAlive = false; // 살아있지 않음
    }

}
