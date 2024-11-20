using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GoldManager : MonoBehaviour
{
    public int currentGold = 0; // 현재 플레이어의 골드
    public TMP_Text goldText; // UI에 표시될 골드 텍스트

    void Start()
    {
        UpdateGoldUI(); // 게임 시작 시 UI 업데이트
    }

    // 골드를 추가하는 함수
    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
    }

    // 골드를 차감하는 함수
    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateGoldUI();
            return true;
        }
        else
        {
            Debug.Log("골드가 부족합니다!");
            return false;
        }
    }

    // 골드를 UI에 업데이트하는 함수
    void UpdateGoldUI()
    {
        goldText.text = currentGold.ToString(); // 골드를 텍스트로 변환하여 표시
    }
}