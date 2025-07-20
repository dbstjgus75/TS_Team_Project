using System.Collections;
using UnityEngine;

public class NpcUnit : UnitBase
{
    public StageUnitData mStageUnitData { get; set;  }
    public bool mlsMoveToTarget { get; set; } = false; // JS 8-2

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int InUnitId, StageUnitData InStageUnitData)
    {
        InitUnit(InUnitId, InStageUnitData.Hp, InStageUnitData.Power, InStageUnitData.Armor);
        mStageUnitData = InStageUnitData;
        mlsMoveToTarget = true; // JS 8-2
        mlsNoneDamage = false; // JS 8-2
        GameDataManager.aInstance.mLiveNpcUnitCount++;
    }
    private void OnTriggerEnter(Collider other) // JS 8-2
    {
        MyPcUnit IMyPcUnit = other.GetComponent<MyPcUnit>();
        if (IMyPcUnit != null)
        {
            mlsMoveToTarget = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MyPcUnit IMyPCUnit = other.GetComponent<MyPcUnit>();
        if (IMyPCUnit != null)
        {
            mlsMoveToTarget = true; 
        }
    }

    public void SetSpeed(float InSpeed)
    {
        mStageUnitData.UnitSpeed = InSpeed;
        NpcUnitMovement Movement = GetComponent<NpcUnitMovement>();
        if(Movement != null)
        {
            Movement.mSpeed = InSpeed;
        }
    }

    public override void OnHit(int InDamage)
    {
        Debug.Log("OnHit 진입"); // 1. 함수 호출 확인

        if (FSMStageController.aInstance.IsPlayGame() == false)
        {
            Debug.Log("게임이 플레이 상태가 아님");

            return; // 게임이 플레이 중이 아닐 때는 데미지를 받지 않음
        }
        if (mlsNoneDamage == true)
        {
            Debug.Log("mlsNoneDamage == true 상태로 데미지 무효화");

            return; // 데미지를 받지 않도록 설정된 상태
        }
        mlsNoneDamage = true;

        Debug.Log("base.OnHit 진입 전");
        base.OnHit(InDamage);

        Debug.Log(mUnitData != null ? "mUnitData 존재, HP=" + mUnitData.Hp : "mUnitData null");

        Debug.Log("Npc : " + gameObject.name + "Hp : " + mUnitData.Hp);
        if(mlsAlive)
        {
            StartCoroutine(_OnHitting()); // 데미지를 받은 후 일정 시간 동안 다시 데미지를 받지 않도록 설정
        }
    }

    private IEnumerator _OnHitting()
    {
        yield return new WaitForSeconds(1.0f); 
        mlsNoneDamage = false; 
    }

    public override void OnDie() // ysh_7-3
    {
        base.OnDie();
        mlsAlive = false; // NPC가 죽었을 때 생존 상태를 false로 설정
        gameObject.SetActive(false); // 비활성화 처리
        GamePoolManager.aInstance.EnqueueNpcPool(this); // 풀로 반환
        GameDataManager.aInstance.mLiveNpcUnitCount = Mathf.Max(0, --GameDataManager.aInstance.mLiveNpcUnitCount);

        StopAllCoroutines(); // 죽었을 때 모든 코루틴 중지
    }

    private bool mlsNoneDamage = false; 
}
