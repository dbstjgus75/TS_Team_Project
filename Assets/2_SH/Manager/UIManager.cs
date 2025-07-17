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


    private static UIManager sInstance = null; 
}
