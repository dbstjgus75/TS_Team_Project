using UnityEngine;

public class GameControl
{
    public delegate void OnMoving(Vector3 pDirect);
    public delegate void OnMoveStart();
    public delegate void OnMoveEnd();
    public delegate void OnMouseInput(int InIndex, Vector3 InMousePos); //BH
    public OnMoving aOnMoving { get; set; }
    public OnMoveStart aOnMoveStart { get; set; }
    public OnMoveEnd aOnMoveEnd { get; set; }

    public OnMouseInput aOnMouseInput { get; set; } // BH

    public static GameControl aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameControl();
            }
            return sInstance;
        }
    }
    public void Init()
    {

    }
    public void SetControlObject(GameObject InGameObject)
    {
        mControlObject = InGameObject;
        mMovementBase = InGameObject.GetComponent<UnitMovementBase>();
    }
    public GameObject GetCotrolObject()
    {
        return mControlObject;
    }
    public void OnUpdate()
    {
        _UpdateKeyboard();
        _UpdateMouseInput(); //BH
    }
    public void Clear()
    {

    }
    private void _UpdateMouseInput() //BH
    {
        if (Input.GetMouseButton(0))  //��ư�ٿ����� �ٲٸ� 1Ŭ���� 1�̻��Ϲ߻�
        {
            if (aOnMouseInput != null)
            {
                aOnMouseInput(0, Input.mousePosition);
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (aOnMouseInput != null)
            {
                aOnMouseInput(1, Input.mousePosition);
            }
        }
    }
    private void _UpdateKeyboard()
    {
        Vector3 MoveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            MoveVector += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveVector += new Vector3(-1, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveVector += new Vector3(0, 0, -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveVector += new Vector3(1, 0, 0);
        }

        Vector3 MoveVectorNormal = MoveVector.normalized;
        if (MoveVectorNormal != Vector3.zero)
        {
            if (aOnMoving != null)
            {
                if (aOnMoving != null)
                {
                    aOnMoving(MoveVectorNormal);
                }

                if (misMoving == false)
                {
                    if (aOnMoveStart != null)
                    {
                        aOnMoveStart();
                    }
                    misMoving = true;

                }
            }
            aOnMoving(MoveVectorNormal);
        }
    }

    private static GameControl sInstance = null;

    private GameObject mControlObject = null;
    private UnitMovementBase mMovementBase = null;
    private bool misMoving = false;
}
