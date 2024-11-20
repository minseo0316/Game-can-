using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    public GameObject[] cardPrefabs; // 6개의 카드 프리팹 배열
    public int numberOfCards = 6; // 총 카드 수
    public float xSpacing = 200.0f; // x축 간격
    public Transform cardSelectionPanel; // cardSelectionPanel 참조
    public Button redrawButton; // 다시 뽑기 버튼
    public GameObject redrawLimitPanel; // Redraw Limit Panel
    public Button RLButton; // Redraw Limit Panel 닫기 버튼 (RLButton)

    private GameObject[] currentCards = new GameObject[3]; // 현재 화면에 표시된 카드들
    private bool hasRedrawUsed = false; // 다시 뽑기 버튼 사용했는지 확인하는 변수
    private Button[] cardButtons; // 카드 선택 버튼 배열
    private bool isCardSelected = false;

    void Start()
    {
        // 카드 버튼들을 초기화
        cardButtons = FindObjectsOfType<Button>();

        // 필수 참조가 모두 설정되었는지 확인
        if (cardPrefabs == null || cardPrefabs.Length == 0)
        {
            Debug.LogError("카드 프리팹이 설정되지 않았습니다!");
            return;
        }
        if (cardSelectionPanel == null)
        {
            Debug.LogError("cardSelectionPanel Transform이 설정되지 않았습니다!");
            return;
        }
        if (redrawButton == null)
        {
            Debug.LogError("다시 뽑기 버튼이 설정되지 않았습니다!");
            return;
        }

        // 카드 배치
        PlaceCardsRandomly();

        if (redrawButton != null)
        {
            redrawButton.onClick.AddListener(RedrawCards);
        }

        // 처음에는 RedrawLimitPanel을 비활성화
        if (redrawLimitPanel != null)
        {
            redrawLimitPanel.SetActive(false);
        }

        // RedrawLimitPanel의 닫기 버튼 클릭 시 동작
        if (RLButton != null)
        {
            RLButton.onClick.AddListener(CloseRedrawLimitPanel);
        }
    }

    void PlaceCardsRandomly()
    {
        // 카드 인덱스를 셔플
        int[] indices = new int[numberOfCards];
        for (int i = 0; i < numberOfCards; i++)
        {
            indices[i] = i;
        }
        ShuffleArray(indices);

        // 카드 위치 설정
        float[] xPositions = { -xSpacing, 0, xSpacing };

        // 카드를 셔플된 순서대로 배치
        for (int i = 0; i < 3; i++) // 3개의 카드를 화면에 표시
        {
            GameObject card = Instantiate(cardPrefabs[indices[i]], cardSelectionPanel); // cardSelectionPanel에 카드 배치
            currentCards[i] = card; // 현재 카드를 저장

            // RectTransform 설정
            RectTransform rectTransform = card.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.anchoredPosition = new Vector2(xPositions[i], 0);
            }

            // 버튼 컴포넌트를 가져와 클릭 이벤트를 추가
            Button button = card.GetComponent<Button>();
            if (button != null)
            {
                int cardIndex = indices[i];
                button.onClick.AddListener(() => OnCardSelected(cardIndex));
            }
        }

        // 카드가 배치되면 카드 선택 버튼을 활성화시킴
        EnableCardSelection(true);
    }

    void RedrawCards()
    {
        if (hasRedrawUsed)
        {
            // 이미 사용한 경우에는 Redraw Limit Panel을 표시
            if (redrawLimitPanel != null)
            {
                redrawLimitPanel.SetActive(true);
                // RedrawLimitPanel 활성화 시 카드 선택 비활성화
                EnableCardSelection(false);
            }

            

            return;
        }

        // 기존 카드 삭제
        for (int i = 0; i < currentCards.Length; i++)
        {
            if (currentCards[i] != null)
            {
                Destroy(currentCards[i]);
            }
        }

        // 새로운 카드 배치
        PlaceCardsRandomly();
        // 다시 뽑기 버튼 사용표시
        hasRedrawUsed = true;
    }

    void OnCardSelected(int cardIndex)
    {
        if (isCardSelected) return; 
        Debug.Log("카드 " + cardIndex + " 선택됨");
        //SceneManager.LoadScene(""); // <= scene 이름 넣어야함
    }

    // 카드 선택 버튼 비활성화 / 활성화
    void EnableCardSelection(bool enable)
    {
        // 카드 선택 버튼만 활성화/비활성화
        foreach (Button btn in cardButtons)
        {
            // RLButton은 제외하고 나머지 버튼들만 제어
            if (btn != RLButton)
            {
                btn.interactable = enable;
            }
        }
    }

    // RedrawLimitPanel 닫기
    void CloseRedrawLimitPanel()
    {
        if (redrawLimitPanel != null)
        {
            redrawLimitPanel.SetActive(false);
        }

        // RedrawLimitPanel이 닫히면 카드 선택 활성화
        EnableCardSelection(true);
    }

    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
