using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public GameObject introPanel; // 설명 창 패널
    public GameObject cardSelectionPanel; // 카드 선택 창 패널
    public Button startButton; // 시작 버튼

    void Start()
    {
        // 패널이 null이 아닌지 체크
        if (introPanel == null || cardSelectionPanel == null)
        {
            Debug.LogError("Intro Panel 또는 Card Selection Panel이 할당되지 않았습니다!");
            return;
        }

        // 설명 창은 기본적으로 활성화하고, 카드 선택 창은 비활성화
        introPanel.SetActive(true);
        cardSelectionPanel.SetActive(false);

        // 시작 버튼이 null이 아니면 버튼 클릭 이벤트 연결
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonPressed);
        }
        else
        {
            Debug.LogWarning("Start Button이 할당되지 않았습니다.");
        }
    }

    public void OnStartButtonPressed()
    {
        // 설명 창을 닫고 카드 선택 창을 열기
        introPanel.SetActive(false);
        cardSelectionPanel.SetActive(true);
    }
}
