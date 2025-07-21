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
        Debug.Log("OnHit ����"); // 1. �Լ� ȣ�� Ȯ��

        if (FSMStageController.aInstance.IsPlayGame() == false)
        {
            Debug.Log("������ �÷��� ���°� �ƴ�");

            return; // ������ �÷��� ���� �ƴ� ���� �������� ���� ����
        }
        if (mlsNoneDamage == true)
        {
            Debug.Log("mlsNoneDamage == true ���·� ������ ��ȿȭ");

            return; // �������� ���� �ʵ��� ������ ����
        }
        mlsNoneDamage = true;

        Debug.Log("base.OnHit ���� ��");
        base.OnHit(InDamage);

        Debug.Log(mUnitData != null ? "mUnitData ����, HP=" + mUnitData.Hp : "mUnitData null");

        Debug.Log("Npc : " + gameObject.name + "Hp : " + mUnitData.Hp);
        if(mlsAlive)
        {
            StartCoroutine(_OnHitting()); // �������� ���� �� ���� �ð� ���� �ٽ� �������� ���� �ʵ��� ����
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
        mlsAlive = false; // NPC�� �׾��� �� ���� ���¸� false�� ����
        gameObject.SetActive(false); // ��Ȱ��ȭ ó��
        GamePoolManager.aInstance.EnqueueNpcPool(this); // Ǯ�� ��ȯ
        GameDataManager.aInstance.mLiveNpcUnitCount = Mathf.Max(0, --GameDataManager.aInstance.mLiveNpcUnitCount);

        StopAllCoroutines(); // �׾��� �� ��� �ڷ�ƾ ����
    }

    private bool mlsNoneDamage = false; 
}
