using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public float skillCooldown = 10f;
    public float moveSpeed = 5f;
    public float attackSpeed = 1f;

    private void Start()
    {
        // 현재 체력을 최대 체력으로 설정
        currentHP = maxHP;
    }

    // 최대 체력을 증가시키는 메서드
    public void IncreaseMaxHP(int amount)
    {
        maxHP += amount;
        Debug.Log($"Max HP가 {amount}만큼 증가하여 {maxHP}가 되었습니다.");
    }

    // 스킬 쿨타임을 감소시키는 메서드
    public void ReduceSkillCooldown(float amount)
    {
        skillCooldown = Mathf.Max(0, skillCooldown - amount);
        Debug.Log($"스킬 쿨타임이 {amount}초 감소하여 {skillCooldown}초가 되었습니다.");
    }

    // 이동 속도를 증가시키는 메서드
    public void IncreaseMoveSpeed(float multiplier)
    {
        moveSpeed *= multiplier;
        Debug.Log($"이동 속도가 {multiplier}배 증가하여 {moveSpeed}가 되었습니다.");
    }

    // 공격 속도를 증가시키는 메서드
    public void IncreaseAttackSpeed(float multiplier)
    {
        attackSpeed *= multiplier;
        Debug.Log($"공격 속도가 {multiplier}배 증가하여 {attackSpeed}가 되었습니다.");
    }

    // 체력을 최대치로 회복하는 메서드
    public void RestoreFullHealth()
    {
        currentHP = maxHP;
        Debug.Log("체력이 최대치로 회복되었습니다.");
    }
}
