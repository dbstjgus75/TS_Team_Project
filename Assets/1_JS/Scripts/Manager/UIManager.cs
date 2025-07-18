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


    private static UIManager sInstance = null; 
}
