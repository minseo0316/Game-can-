using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    public int baseMaxHealth = 100;
    public int baseMaxMana = 100;
    public int baseMaxGuard = 10;
    public int basePower = 10;
    public int baseCritical = 0;
    public int baseMagic = 0;
    public float baseAttackSpeed = 1.0f;
    public TMP_Text healthText;
    public TMP_Text manaText;
    
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public int maxGuard;
    public int power;
    public int magic;
    public int Critical;
    public float AttackSpeed;
    public Image healthBar;
    public Image manaBar;

    private List<Item> equippedItems = new List<Item>();

    void Start()
    {
        UpdateStats();
        currentHealth = maxHealth;
        currentMana = maxMana;
        
        UpdateHealthBar();
        UpdateManaBar();
    }

    public void EquipItem(Item item)
    {
        if (item.equipmentType != EquipmentType.None)
        {
            equippedItems.Add(item);
            UpdateStats();
        }
    }

    public void UnequipItem(Item item)
    {
        if (equippedItems.Remove(item))
        {
            UpdateStats();
        }
    }

    public void UpdateStats()
    {
        int oldMaxHealth = maxHealth;
        int oldMaxMana = maxMana;

        maxHealth = baseMaxHealth;
        maxMana = baseMaxMana;
        maxGuard = baseMaxGuard;
        power = basePower;
        magic =baseMagic;
        AttackSpeed = baseAttackSpeed;
        Critical = baseCritical;

        foreach (Item item in equippedItems)
        {
            maxHealth +=item.healthBonus;
            power +=item.PowerBonus;
            maxGuard += item.GuardBonus;
            magic += item.MagicBonus;
            AttackSpeed += item.AttackSpeedBonus;
            Critical += item.CrticalBonus;

        }

        // 최대 체력이 증가한 경우, 현재 체력도 비례하여 증가
        if (maxHealth > oldMaxHealth)
        {
            float healthRatio = (float)currentHealth / oldMaxHealth;
            currentHealth = Mathf.RoundToInt(healthRatio * maxHealth);
        }

        // 최대 마나가 증가한 경우, 현재 마나도 비례하여 증가
        if (maxMana > oldMaxMana)
        {
            float manaRatio = (float)currentMana / oldMaxMana;
            currentMana = Mathf.RoundToInt(manaRatio * maxMana);
        }

        UpdateHealthBar();
        UpdateManaBar();
    }

    public void TakeDamage(int damage)
{
    int actualDamage = Mathf.Max(0, damage - maxGuard); // 방어력을 고려한 실제 데미지 계산
    currentHealth -= actualDamage; // 현재 체력 감소

    if (currentHealth < 0) 
        currentHealth = 0; // 체력이 음수가 되지 않도록 제한

    UpdateHealthBar(); // 체력 UI 즉시 업데이트

    Debug.Log($"플레이어가 {actualDamage}의 데미지를 받았습니다. 현재 체력: {currentHealth}/{maxHealth}");

    if (currentHealth <= 0)
    {
        Die(); // 플레이어 사망 처리
    }
}

public void UpdateHealthBar()
{
    if (healthBar != null)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth; // 현재 체력을 비율로 변환하여 슬라이더에 반영
        Debug.Log($"체력바 업데이트됨: {healthBar.fillAmount * 100}%");
    }
    
    if (healthText != null)
    {
        healthText.text = $"{currentHealth}/{maxHealth}"; // 텍스트로도 현재 체력을 표시
    }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthBar();
        Debug.Log($"플레이어 체력 회복: {amount}. 현재 체력: {currentHealth}/{maxHealth}");
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        if (currentMana < 0) currentMana = 0;
        UpdateManaBar();
    }

public void UpdateManaBar()
{
    if (manaBar != null)
    {
        manaBar.fillAmount = (float)currentMana / maxMana;
        manaBar.color = Color.blue;
    }
    if (manaText != null)
    {
        manaText.text = $"{currentMana}/{maxMana}";
    }
}

    private void Die()
    {
        Debug.Log("플레이어 사망!");
        gameObject.SetActive(false);
    }
}