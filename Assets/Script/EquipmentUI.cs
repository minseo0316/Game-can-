using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentUI : MonoBehaviour
{
    public PlayerController playerController;
    public TMP_Text totalStatsText;
    public PlayerHealth playerHealth;

    private int totalPower = 0;
    private int totalHealth = 0;
    private int totalInt = 0;
    private float totalAttackSpeed=0f;
    private int totalCritical=0;
    private int totalGuard = 0;

    public void UpdateTotalStats()
    {
        totalPower += playerHealth.power;
        totalHealth += playerHealth.maxHealth;
        totalInt += playerHealth.magic;
        totalAttackSpeed += playerHealth.AttackSpeed;
        totalCritical += playerHealth.Critical;
        totalGuard += playerHealth.maxGuard;

        totalStatsText.text = $"총 스텟\n힘: {totalPower}\n체력: {totalHealth}\n지능: {totalInt}\n방어력: {totalGuard}\n공격속도: {totalAttackSpeed} \n치명타확률: {totalCritical}";
    }

    // 아이템 장착/해제 시 이 메서드를 호출하여 UI 업데이트
    public void OnEquipmentChanged()
    {
        UpdateTotalStats();
    }
}
