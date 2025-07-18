using System;
using UnityEngine;
using UnityEngine.UI;
public class HUDComponent : MonoBehaviour
{
    public Text HUDText;
    public Slider MyUnitExpSlider; // ysh
    public Text GameTimeText; // ysh
    public GameObject RootLevelUpUI; // ysh
    private void Awake()
    {
        UIManager.aInstance.aOnShowHUDText += HandleShowHUDText;
        UIManager.aInstance.aOnSetExp += HandleSetExp; // ysh
        UIManager.aInstance.aOnShowLevelUpStateUI += HandleLevelUpStateUI; // ysh
        InitHUD(); // ysh
    }

    private void OnDestroy()
    {
        UIManager.aInstance.aOnShowHUDText -= HandleShowHUDText;
        UIManager.aInstance.aOnSetExp -= HandleSetExp; // ysh
        UIManager.aInstance.aOnShowLevelUpStateUI -= HandleLevelUpStateUI; // ysh
    }

    void Update()
    {
        float IGameTimeTotalSeconds = GameDataManager.aInstance.GetGameTime(); // ysh
        TimeSpan INowTimespan = TimeSpan.FromSeconds(IGameTimeTotalSeconds); // ���� �������� �ִ� �ð��� �޾ƿ� ysh
        if (GameTimeText)
        {
            GameTimeText.text = string.Format("{0:D2}:{1:D2}", INowTimespan.Minutes, INowTimespan.Seconds);
        }
    }

    protected void InitHUD() // ysh
    {
        if(HUDText != null)
        {
            HUDText.text = string.Empty;
            HUDText.gameObject.SetActive(false); // HUDText�� Ȱ��ȭ���� �ʵ��� ����
        }
    }

    // HUD �ؽ�Ʈ ǥ�� ���ο� ���� ����
    public void HandleShowHUDText(bool InisShow, string InText) 
    {
        if(HUDText == null)
        {
            return;
        }

        HUDText.gameObject.SetActive(InisShow); // HUDText�� Ȱ��ȭ�Ǹ� �ؽ�Ʈ�� ����
        HUDText.text = InText; // �ؽ�Ʈ ����
    }

    // ����ġ �����̴� ����
    private void HandleSetExp(int InCurrentExp, int InMaxExp) // ysh
    {
        float IExpValue = (float)InCurrentExp / InMaxExp;
        if (MyUnitExpSlider != null)
        {
            MyUnitExpSlider.value = IExpValue; // �����̴� �� ����
        }
    }

    private void HandleLevelUpStateUI(bool InIsShow) // ysh
    {
        if(RootLevelUpUI != null) // ysh
        {
            RootLevelUpUI.SetActive(InIsShow); 
        }
    }


}
