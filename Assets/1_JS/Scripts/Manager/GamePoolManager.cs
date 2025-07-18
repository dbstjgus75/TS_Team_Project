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
    }
    public void Clear()
    {
        SkillPool.Clear();
        SkillPool = null;
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

    private static GamePoolManager sInstance = null;

    //�ش� ��ų Ÿ�� ���� ������ ��ų���̽� Q������ ��ųǮ SkillType ���� �� f12����� �ű�� �̵� BH
    private Dictionary<SkillType, Queue<SkillBase>> SkillPool = null; 
}
