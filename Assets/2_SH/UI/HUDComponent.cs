using UnityEngine;
using UnityEngine.UI;
public class HUDComponent : MonoBehaviour
{
    public Text HUDText;
    private void Awake()
    {
        UIManager.aInstance.aOnShowHUDText += HandleShowHUDText; 
    }

    private void OnDestroy()
    {
        UIManager.aInstance.aOnShowHUDText -= HandleShowHUDText;
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

    void Update()
    {
        
    }
}
