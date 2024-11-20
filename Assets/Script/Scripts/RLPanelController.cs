using UnityEngine;
using UnityEngine.UI;

public class RLPanelController : MonoBehaviour
{
    public GameObject redrawLimitPanel; // Redraw Limit Panel
    public Button closeRLButton; // Redraw Limit Panel을 닫는 버튼

    void Start()
    {
        // null 체크: redrawLimitPanel과 closeRLButton이 할당되지 않으면 오류 방지
        if (redrawLimitPanel == null)
        {
            Debug.LogError("Redraw Limit Panel이 할당되지 않았습니다!");
        }
        if (closeRLButton == null)
        {
            Debug.LogError("Redraw Limit Panel 닫기 버튼이 할당되지 않았습니다!");
        }

        // 버튼 클릭 시 Redraw Limit Panel을 닫는 함수 연결
        if (closeRLButton != null)
        {
            closeRLButton.onClick.AddListener(CloseRLPanel);
        }
    }

    // Redraw Limit Panel 닫기
    void CloseRLPanel()
    {
        if (redrawLimitPanel != null)
        {
            // Redraw Limit Panel을 비활성화 (애니메이션을 추가하려면 이 부분을 수정)
            redrawLimitPanel.SetActive(false);
        }
    }
}
