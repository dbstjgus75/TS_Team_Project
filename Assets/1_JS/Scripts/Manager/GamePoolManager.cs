using System.Collections.Generic;
using UnityEngine;

public class GamePoolManager
{
    public static GamePoolManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GamePoolManager();
            }
            return sInstance;
        }
    }
    public void Init()
    {
       SkillPool = new Dictionary<SkillType, Queue<SkillBase>>(); //�ʱ�ȭ BH
        NpcPool  = new Dictionary<string, Queue<NpcUnit>>(); // ysh
    }
    public void Clear()
    {
        SkillPool.Clear();
        SkillPool = null;

        NpcPool.Clear(); // ysh
        NpcPool = null; // ysh
    }

    public void EnqueueSkillPool(SkillBase InSkill) //��ų�� ���� �� BH
    {
        if (SkillPool == null)
        {
            return;
        }
        if (SkillPool.ContainsKey(InSkill.mSkillType) == false)
        {
            SkillPool.Add(InSkill.mSkillType, new Queue<SkillBase>());
        }
        SkillPool[InSkill.mSkillType].Enqueue(InSkill);
    }

    public SkillBase DequeueSkillPool(SkillType InSkillType) //Q����ü:���Լ��� BH
    {
        if (SkillPool == null)
        {
            return null;
        }
        if (SkillPool.ContainsKey(InSkillType) == false)
        {
            return null;
        }
        if (SkillPool[InSkillType].Count == 0)
        {
            return null;
        }
        return SkillPool[InSkillType].Dequeue();
    }

    public void EnqueueNpcPool(NpcUnit InNpcUnit) // ysh
    {
        string IUnitId = InNpcUnit.mStageUnitData.UnitId;
        if (NpcPool == null)
        {
            return;
        }
        if (NpcPool.ContainsKey(IUnitId) == false)
        {
            NpcPool.Add(IUnitId, new Queue<NpcUnit>());
        }
        NpcPool[IUnitId].Enqueue(InNpcUnit);
    }

    public NpcUnit DequeueNpcPool(string InUnitId) // ysh
    {
        if (NpcPool == null)
        {
            return null;
        }
        if (NpcPool.ContainsKey(InUnitId) == false)
        {
            return null;
        }
        if (NpcPool[InUnitId].Count == 0)
        {
            return null;
        }
        return NpcPool[InUnitId].Dequeue();
    }


    private static GamePoolManager sInstance = null;

    //�ش� ��ų Ÿ�� ���� ������ ��ų���̽� Q������ ��ųǮ SkillType ���� �� f12����� �ű�� �̵� BH
    private Dictionary<SkillType, Queue<SkillBase>> SkillPool = null; 

    private Dictionary<string, Queue<NpcUnit>> NpcPool = null; // ysh
}
