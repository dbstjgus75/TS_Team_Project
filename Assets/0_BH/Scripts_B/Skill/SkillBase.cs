using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public SkillType mSkillType { set; get; } //추후에 스킬타입은 각 스킬데이터에 존재할 예정
    public Vector3 mStartPos { get; private set; }
    public Vector3 mStartDir { get; private set; }
    public ActiveSkillData mActiveSkillData { get; private set; }
    // Update is called once per frame
    public virtual void FireSkill(ActiveSkillData InSkillData, Vector3 InStartPos, Vector3 InStartDir) 
    {
        mActiveSkillData = InSkillData;
        mStartPos = InStartPos;
        mStartDir = InStartDir;

        transform.position = mStartPos;
    }
    public virtual void StopSkill()
    {
        //나 자신 오브젝트의 액티브를 끄는 방법으로
        gameObject.SetActive(false);
        //할역할을 다했기 때문에 스킬풀로 들어감 this:나자신
        GamePoolManager.aInstance.EnqueueSkillPool(this); 
    }
        public virtual void Update()
    {
        
    }
    public virtual void OnDestroy()
    {
    
    }
}
