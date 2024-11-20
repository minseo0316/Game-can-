using UnityEngine;

[System.Serializable]
public class Card
{
    public enum CardType
    {
        Type1,
        Type2,
        Type3
    }

    public enum CardEffect
    {
        IncreaseMaxHealth,
        RestoreHealth,
        IncreaseMaxMana,
        RestoreMana,
        IncreasePower,
        IncreaseGuard
    }

    public CardType type;
    public CardEffect effect;
    public int value;

    public Card(CardType type, CardEffect effect, int value)
    {
        this.type = type;
        this.effect = effect;
        this.value = value;
    }
    public void ApplyEffect(PlayerHealth playerHealth)
    {
        switch (effect)
        {
            case CardEffect.IncreaseMaxHealth:
                playerHealth.baseMaxHealth += value;
                playerHealth.UpdateStats();
                Debug.Log($"최대 체력이 {value} 증가했습니다.");
                break;
            case CardEffect.RestoreHealth:
                playerHealth.Heal(value);
                break;
            case CardEffect.IncreaseMaxMana:
                playerHealth.baseMaxMana += value;
                playerHealth.UpdateStats();
                Debug.Log($"최대 마나가 {value} 증가했습니다.");
                break;
            case CardEffect.RestoreMana:
                playerHealth.currentMana = Mathf.Min(playerHealth.currentMana + value, playerHealth.maxMana);
                playerHealth.UpdateManaBar();
                Debug.Log($"마나가 {value} 회복되었습니다.");
                break;
            case CardEffect.IncreasePower:
                playerHealth.basePower += value;
                playerHealth.UpdateStats();
                Debug.Log($"기본 공격력이 {value} 증가했습니다.");
                break;
            case CardEffect.IncreaseGuard:
                playerHealth.baseMaxGuard += value;
                playerHealth.UpdateStats();
                Debug.Log($"기본 방어력이 {value} 증가했습니다.");
                break;
        }
    }
}