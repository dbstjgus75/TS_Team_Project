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

    // �ؽ�Ʈ ǥ�� ���ο� �ؽ�Ʈ ���� ����
    public delegate void OnShowHUDText(bool InIsShow, string Text);
    
    // HUD �ؽ�Ʈ ���� ���� �� ����
    public OnShowHUDText aOnShowHUDText { get; set; }

    // ����ġ �����̴� ����
    public delegate void OnSetExp(int InExp, int InMaxExp); // ysh
    public OnSetExp aOnSetExp { get; set; } // ysh

    public delegate void OnShowLevelUpStateUI(bool InIsShow); // ������ UI ǥ�� ���� ysh
    public OnShowLevelUpStateUI aOnShowLevelUpStateUI { get; set; } // ysh

    // HUD �ؽ�Ʈ ǥ��
    public void ShowHUDText(string Intext)
    {
        if (aOnShowHUDText != null)
        {
            aOnShowHUDText(true, Intext);
        }
    }

    // HUD �ؽ�Ʈ ����
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

    // ����ġ �����̴� ����
    public void SetExp(int InCurrentExp, int InMaxExp) // ysh
    {
        if (aOnSetExp != null)
        {
            aOnSetExp(InCurrentExp, InMaxExp);
        }
    }


    private static UIManager sInstance = null; 
}
