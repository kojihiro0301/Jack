using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionController : MonoBehaviour
{
    [Header("UI 연결")]
    public GameObject optionPanel;    // 설정 창 패널
    public Slider volumeSlider;       // 음량 조절 슬라이더
    
    [Header("텍스트 UI")]
    public TextMeshProUGUI helpText;   // '조작법' 표시용
    private bool isOptionOpen = false; // 설정 창 열림 여부

    void Start()
    {
        // 1. 시작할 때 패널은 끄고, 슬라이더 값 동기화
        optionPanel.SetActive(false);
        volumeSlider.value = AudioListener.volume; 
        
        // 2. 슬라이더 이벤트 연결
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 3. 텍스트 설정
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
            // Delete 키로 닫기 (게임 재개)
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                CloseOption();
            }
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
        Time.timeScale = 0f; // 일시 정지
    }

    void CloseOption()
    {
        isOptionOpen = false;
        optionPanel.SetActive(false);
        Time.timeScale = 1f; // 재개
    }
    // 화면의 텍스트들을 갱신하는 함수
    void UpdateUI()
    {
        // 도움말 텍스트 갱신
        if(helpText != null)
        {
            helpText.text = "Continue : Delete key\n\n\n" + 
                            "Back to Start Screen : Shift key\n\n\n\n" +
                            "Sound Control";
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}