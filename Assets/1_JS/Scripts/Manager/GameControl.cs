using UnityEngine;

public class GameControl
{
    public delegate void OnMoving(Vector3 pDirect);
    public delegate void OnMoveStart();
    public delegate void OnMoveEnd();

    public OnMoving aOnMoving { get; set; }
    public OnMoveStart aOnMoveStart { get; set; }
    public OnMoveEnd aOnMoveEnd { get; set; }

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
    }
    public void Clear()
    {

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
