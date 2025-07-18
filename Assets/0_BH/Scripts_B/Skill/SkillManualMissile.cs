using System.Collections;
using UnityEngine;

public class SkillManualMissile : SkillBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void FireSkill(ActiveSkillData InSkillData, Vector3 InStartPos, Vector3 InSkillDir)
    {
        base.FireSkill(InSkillData, InStartPos, InSkillDir);

        mSkillType = SkillType.ManualMissile;

        StartCoroutine(_OnMissileLiftTime());

    }

    public IEnumerator _OnMissileLiftTime()
    {
        float CurrentLifeTime = 0.0f;
        while (true)
        {
            Vector3 AddForceVector = mStartDir * mActiveSkillData.Speed * Time.deltaTime;
            transform.position += new Vector3(AddForceVector.x, 0, AddForceVector.z);
            CurrentLifeTime += Time.deltaTime;
            if(CurrentLifeTime > 2.0f)
            {
                break;
            }
            yield return null;
        }
        StopSkill();
    }
  
}
