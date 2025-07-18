using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public SkillType mSkillType { set; get; } //���Ŀ� ��ųŸ���� �� ��ų�����Ϳ� ������ ����
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
        //�� �ڽ� ������Ʈ�� ��Ƽ�긦 ���� �������
        gameObject.SetActive(false);
        //�ҿ����� ���߱� ������ ��ųǮ�� �� this:���ڽ�
        GamePoolManager.aInstance.EnqueueSkillPool(this); 
    }
        public virtual void Update()
    {
        
    }
    public virtual void OnDestroy()
    {
    
    }
}
