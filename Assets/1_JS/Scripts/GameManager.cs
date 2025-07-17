using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int mStaedId = 1;
    public GameObject mMyPc;
    public Transform mNpcSpawnParent;// Parent for NPCs
    public Transform mSkillObjectParant;

    void Start()
    {
        GamePoolManager.aInstance.Init();
        GameControl.aInstance.Init();
        GameDataManager.aInstance.Init();
        FSMStageController.aInstance.Init();
        SpawnManager.aInstance.Init();
    }

    void OnDestroy()
    {
        GamePoolManager.aInstance.Clear();
        GameControl.aInstance.Clear();
        GameDataManager.aInstance.Clear();
        FSMStageController.aInstance.Clear();
        SpawnManager.aInstance.Clear();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {

    }
}
