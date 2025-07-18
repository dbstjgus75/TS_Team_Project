using UnityEngine;
using UnityEngine.UI;

public class NextStateButton : MonoBehaviour
{
    private void Awake()
    {
        mCurrentButton = GetComponent<Button>();
        if (mCurrentButton == null)
        {
            mCurrentButton.onClick.AddListener(OnNextStateButtonClick);
        }
    }

    private void OnDestroy()
    {
        if (mCurrentButton != null)
        {
            mCurrentButton.onClick.RemoveAllListeners();
        }
    }

    public void OnNextStateButtonClick()
    {
        FSMStageController.aInstance.ChangeState(new FSMStageStateProgress());
        SkillManager MyPcSkillManager = GameDataManager.aInstance.GetMyPCObject().GetComponent<SkillManager>();
        if(MyPcSkillManager != null)
        {
            //MyPcSkillManager.AddSkillData(SkillType.Missile); // *UI ���Ǹ� �� �ڵ� �۵� �ȵ� ���߿� �ּ� ��ü* 
        }
    }


    private Button mCurrentButton;
}
