using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int mStageId = 1;
    public GameObject mMyPc;
    public Transform mNpcSpawnParent;// Parent for NPCs
    public Transform mSkillObjectParant;
    public Transform mItemObjectParent;

    void Start()
    {
        GameDataManager.aInstance.Init();
        GameDataManager.aInstance.SetStageData(mMyPc, mNpcSpawnParent, mSkillObjectParant, mItemObjectParent);
        GameDataManager.aInstance.SetCurrentStage(mStageId);
        GameDataManager.aInstance.LoadAll(); // ysh update 2:01

        GamePoolManager.aInstance.Init();

        GameControl.aInstance.Init();
        GameControl.aInstance.SetControlObject(mMyPc);

        SpawnManager.aInstance.Init();
        SpawnManager.aInstance.SetupSpawnPoint(mMyPc);

        FSMStageController.aInstance.Init();

        FSMStageController.aInstance.EnterStage();

    }

    void OnDestroy()
    {
        GameDataManager.aInstance.Clear();        
        GamePoolManager.aInstance.Clear();
        GameControl.aInstance.Clear();
        SpawnManager.aInstance.Clear();
        FSMStageController.aInstance.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        FSMStageController.aInstance.OnUpdate(Time.deltaTime);
        GameControl.aInstance.OnUpdate();
    }
    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {

    }
}
