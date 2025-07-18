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
        TimeSpan INowTimespan = TimeSpan.FromSeconds(IGameTimeTotalSeconds); // 현재 지나오고 있는 시간을 받아옴 ysh
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
            HUDText.gameObject.SetActive(false); // HUDText가 활성화되지 않도록 설정
        }
    }

    // HUD 텍스트 표시 여부와 내용 전달
    public void HandleShowHUDText(bool InisShow, string InText) 
    {
        if(HUDText == null)
        {
            return;
        }

        HUDText.gameObject.SetActive(InisShow); // HUDText가 활성화되면 텍스트를 설정
        HUDText.text = InText; // 텍스트 설정
    }

    // 경험치 슬라이더 설정
    private void HandleSetExp(int InCurrentExp, int InMaxExp) // ysh
    {
        float IExpValue = (float)InCurrentExp / InMaxExp;
        if (MyUnitExpSlider != null)
        {
            MyUnitExpSlider.value = IExpValue; // 슬라이더 값 설정
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
