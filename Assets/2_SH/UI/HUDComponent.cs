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

    void Update()
    {
        
    }
}
