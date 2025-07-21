using UnityEngine;

public class UnitMovementBase : MonoBehaviour
{
    public float mSpeed = 5.0f; // �̵� �ӵ�
    public Transform mRotationTransform; // ���� ȸ����ų ������Ʈ
    public float mRotationSpeed = 400.0f; // ȸ�� �ӵ�
    public Animator mAnimator; 
    void Start()
    {
        
    }

    protected virtual void Update() // ysh_7-3
    {
        Vector3 INowPosition = transform.position + new Vector3(0, 100, 0);
        Vector3 IDirection = new Vector3(0, -1, 0);
        RaycastHit IHit;
        int layermask = 1 << LayerMask.NameToLayer("Terrain");
        if(Physics.Raycast(INowPosition, IDirection, out IHit, 200, layermask))
        {
            float IHeight = IHit.point.y;
            Vector3 INewPos = transform.position;
            INewPos.y = IHeight;
            transform.position = INewPos;
        }
    }
}
