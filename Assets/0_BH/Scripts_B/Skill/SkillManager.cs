using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private void Awake()
    {
        GameControl.aInstance.aOnMouseInput += _OnMouseInput;
    }

    private void OnDestroy()
    {
        GameControl.aInstance.aOnMouseInput -= _OnMouseInput;
    }
    // Update is called once per frame
    void Update()
    {
        CurrentCooltime += Time.deltaTime; //JS수정
    }
    private void _OnMouseInput(int InIndex, Vector3 InMousePos)
    {

        RaycastHit IHit;
        Ray IRay = Camera.main.ScreenPointToRay(InMousePos);
        int layermask = 1 << LayerMask.NameToLayer("Terrain");
        if (Physics.Raycast(IRay, out IHit, 1000, layermask))
        {
            ActiveSkillData NewSkillData= new ActiveSkillData();
            NewSkillData.Type = SkillType.ManualMissile;
            NewSkillData.FirePosition = IHit.point;
            NewSkillData.Cooltime = 0.5f;
            NewSkillData.Speed = 10.0f;
            NewSkillData.ActiveLevel = 2;
            NewSkillData.Power = 100;
            FireSkill(NewSkillData);
        }
    }

    public void FireSkill(ActiveSkillData InSkillData)
    {
        if (CurrentCooltime < InSkillData.Cooltime)
        {
            return;
        }
        MyPcUnitMovement IMovement = GetComponent<MyPcUnitMovement>();
        if (IMovement != null)
        {
            IMovement.DoManualAttack(InSkillData.Type, InSkillData.FirePosition);
        }

        Vector3 ShotDirection = (InSkillData.FirePosition - transform.position).normalized;
        Vector3 StartPosition = new Vector3(transform.position.x, 1, transform.position.z);

        FireSkillObject(InSkillData, StartPosition, ShotDirection);
        CurrentCooltime = 0.0f; // 초기화 왜 안됌?
    }

    public void FireSkillObject(ActiveSkillData InSkillData, Vector3 InStartPos, Vector3 InSkillDir)
    {
        SkillBase SkillObject = GamePoolManager.aInstance.DequeueSkillPool(InSkillData.Type);
        if(SkillObject == null)
        {
            SkillBase NewSkillObjectPrefab = Resources.Load<SkillBase>("Prefabs/Missile");
            SkillObject = GameObject.Instantiate(NewSkillObjectPrefab, GameDataManager.aInstance.GetSkillRootTransform());
            if (SkillObject == null)
            {
                return;
            }
        }
        SkillObject.gameObject.SetActive(true); // 이걸 켜줘야 코르틴이 돌 수 있다
        SkillObject.FireSkill(InSkillData, InStartPos, InSkillDir);
    
    }

    public float CurrentCooltime = 0.0f;
}
