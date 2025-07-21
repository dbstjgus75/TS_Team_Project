using UnityEngine;

public class NpcUnitMovement : UnitMovementBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mNpcUnit = GetComponent<NpcUnit>(); // JS 8-2
    }

    private void OnDestroy()
    {
        mNpcUnit = null; // JS 8-2
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        MoveToMyPc();
    }

    private void MoveToMyPc()
    {
        if (mNpcUnit.mlsMoveToTarget == false) // JS 8-2
        {
            return; // 이동하지 않도록 설정된 경우
        }
        Vector3 ITargetDirection = GameDataManager.aInstance.GetMyPCObject().transform.position - transform.position;
        Vector3 IDirect =ITargetDirection.normalized;

        transform.position += IDirect * mSpeed * Time.deltaTime;
        if(IDirect != Vector3.zero)
        {
            mRotationTransform.rotation = Quaternion.RotateTowards(mRotationTransform.rotation, 
                                                                    Quaternion.LookRotation(IDirect, Vector3.up),
                                                                    mRotationSpeed * Time.deltaTime);
        }
    }

    private NpcUnit mNpcUnit = null; // JS 8-2
}
