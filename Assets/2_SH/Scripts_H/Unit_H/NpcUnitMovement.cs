using UnityEngine;

public class NpcUnitMovement : UnitMovementBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        MoveToMyPc();
    }

    private void MoveToMyPc()
    {
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
}
