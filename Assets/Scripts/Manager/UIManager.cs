using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UIManager
{
    public static UIManager aInstance 
    {
        get {
            if (sInstance == null) 
            {
                sInstance = new UIManager(); 
            }
            return sInstance;
        }
    }

    // 텍스트 표시 여부와 텍스트 내용 전달
    public delegate void OnShowHUDText(bool InIsShow, string Text);
    
    // HUD 텍스트 내용 변경 및 지정
    public OnShowHUDText aOnShowHUDText { get; set; }

    // 경험치 슬라이더 설정
    public delegate void OnSetExp(int InExp, int InMaxExp); // ysh
    public OnSetExp aOnSetExp { get; set; } // ysh

    public delegate void OnShowLevelUpStateUI(bool InIsShow); // 레벨업 UI 표시 여부 ysh
    public OnShowLevelUpStateUI aOnShowLevelUpStateUI { get; set; } // ysh

    // HUD 텍스트 표시
    public void ShowHUDText(string Intext)
    {
        if (aOnShowHUDText != null)
        {
            aOnShowHUDText(true, Intext);
        }
    }

    // HUD 텍스트 숨김
    public void HideHUDText()
    {
        if (aOnShowHUDText != null)
        {
            aOnShowHUDText(false, string.Empty);
        }
    }

    public void ShowLevelUpStateUI(bool InIsShow) // ysh
    {
        if (aOnShowLevelUpStateUI != null)
        {
            aOnShowLevelUpStateUI(InIsShow);
        }
    }

    // 경험치 슬라이더 설정
    public void SetExp(int InCurrentExp, int InMaxExp) // ysh
    {
        if (aOnSetExp != null)
        {
            aOnSetExp(InCurrentExp, InMaxExp);
        }
    }


    private static UIManager sInstance = null; 
}
