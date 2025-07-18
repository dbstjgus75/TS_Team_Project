using System.Collections;
using UnityEngine;

public class MyPcUnitMovement : UnitMovementBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameControl.aInstance.aOnMoving += HandleMoiving;
        GameControl.aInstance.aOnMoveStart += HandleMoveStart;
        GameControl.aInstance.aOnMoveEnd += HandleMoveEnd;
    }
    
    void OnDestroy()
    {
        GameControl.aInstance.aOnMoving -= HandleMoiving;
        GameControl.aInstance.aOnMoveStart -= HandleMoveStart;
        GameControl.aInstance.aOnMoveEnd -= HandleMoveEnd;

    }

    public void DoManualAttack(SkillType InSkillType, Vector3 InAttackPos)  //BH
    {
        Vector3 IAttackDirect = (InAttackPos - transform.position).normalized; //공격방향
        mRotationTransform.rotation = Quaternion.RotateTowards(mRotationTransform.rotation, Quaternion.LookRotation(IAttackDirect), 360);
    }
    private void HandleMoiving(Vector3 pDirect)
    {
        // 이동
        transform.position += pDirect * mSpeed * Time.deltaTime;
        // 회전
        mRotationTransform.rotation = Quaternion.RotateTowards(
            mRotationTransform.rotation, Quaternion.LookRotation(pDirect), mRotationSpeed * Time.deltaTime);
    }

    private void HandleMoveStart()
    {
        if (mAnimator != null)
        {
            mAnimator.CrossFade("Run", 0.1f);
        }
    }
    private void HandleMoveEnd()
    {
        if(mAnimator != null)
        {
            mAnimator.CrossFade("Idle", 0.1f);
        }
    }
}
