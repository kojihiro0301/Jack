using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionController : MonoBehaviour
{
    [Header("UI 接続")]
    public GameObject optionPanel;

    [Header("テキスト UI")]
    public TextMeshProUGUI helpText;
    private bool isOptionOpen = false;
    void Start()
    {
        optionPanel.SetActive(false);
        
        UpdateUI();
    }

    void Update()
    {
        // Tab 키로 설정 창 켜기/끄기
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOptionOpen) CloseOption();
            else OpenOption();
        }

        // 설정 창이 열려 있을 때만 작동
        if (isOptionOpen)
        {
            // Shift 키로 타이틀 이동
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                if (SceneManager.GetActiveScene().name != "TitleScene")
                {
                    Time.timeScale = 1f; 
                    SceneManager.LoadScene("TitleScene");
                }
            }
        }
    }

    void OpenOption()
    {
        isOptionOpen = true;
        optionPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void CloseOption()
    {
        isOptionOpen = false;
        optionPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    void UpdateUI()
    {
        if(helpText != null)
        {
            helpText.text = "Continue : Tab key\n\n" + 
                            "Back to Start Screen : Shift key\n\n\n";
        }
    }
}