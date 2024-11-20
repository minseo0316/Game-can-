using UnityEngine;
using UnityEngine.UI;

public class CardEffectManager : MonoBehaviour
{
    // Player 참조
    public Player player;

    // 각 카드에 해당하는 버튼
    public Button card0Button;
    public Button card1Button;
    public Button card2Button;
    public Button card3Button;
    public Button card4Button;
    public Button card5Button;

    private void Start()
    {
        // 버튼 클릭 이벤트 등록
        card0Button.onClick.AddListener(ApplyCard0Effect);
        card1Button.onClick.AddListener(ApplyCard1Effect);
        card2Button.onClick.AddListener(ApplyCard2Effect);
        card3Button.onClick.AddListener(ApplyCard3Effect);
        card4Button.onClick.AddListener(ApplyCard4Effect);
        card5Button.onClick.AddListener(ApplyCard5Effect);
    }

    // 카드 0: Max HP 증가
    void ApplyCard0Effect()
    {
        player.IncreaseMaxHP(20); // 예시: Max HP 20 증가 / 수치 변동
        Debug.Log("카드 0: Max HP가 20 증가하였습니다.");
    }

    // 카드 1: 스킬 쿨타임 감소
    void ApplyCard1Effect()
    {
        player.ReduceSkillCooldown(2); // 예시: 쿨타임 2초 감소
        Debug.Log("카드 1 선택됨: 스킬 쿨타임이 2초 감소하였습니다.");
    }

    // 카드 2: 이동 속도 증가
    void ApplyCard2Effect()
    {
        player.IncreaseMoveSpeed(1.5f); // 예시: 이동 속도 1.5배 증가
        Debug.Log("카드 2 선택됨: 이동 속도가 1.5배 증가하였습니다.");
    }

    // 카드 3: 공격 속도 증가
    void ApplyCard3Effect()
    {
        player.IncreaseAttackSpeed(1.2f); // 예시: 공격 속도 1.2배 증가
        Debug.Log("카드 3 선택됨: 공격 속도가 1.2배 증가하였습니다.");
    }

    // 카드 4: 이동 속도 추가 증가
    void ApplyCard4Effect()
    {
        player.RestoreFullHealth(); // 체력 완전 회복
        Debug.Log("카드 4 선택됨: 체력이 최대치로 회복되었습니다.");
        
    }

    // 카드 5: 체력 최대 회복
    void ApplyCard5Effect()
    {
        player.IncreaseMoveSpeed(1.3f); // 예시: 이동 속도 추가로 1.3배 증가
        Debug.Log("카드 5 선택됨: 이동 속도가 추가로 1.3배 증가하였습니다.");
    }
}