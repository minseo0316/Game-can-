using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 5f; // 플레이어를 감지할 범위
    public float attackRange = 2f; // 공격 범위
    public float moveSpeed = 2f; // 이동 속도
    public Transform player; // 플레이어의 위치
    public Animator animator; // 몬스터의 애니메이터
    public int health = 100; // 몬스터의 체력
    public int attackPower = 10; // 몬스터의 공격력
    public float attackCooldown = 1f; // 공격 쿨다운 시간

    [Header("Drop Settings")]
    public int minGoldDrop = 10; // 최소 드랍 골드 양
    public int maxGoldDrop = 50; // 최대 드랍 골드 양
    public GameObject[] dropItems; // 드랍할 아이템 목록 (프리팹 배열)

    private bool isAttacking = false;
    private bool isDead = false;
    private float lastAttackTime = 0f;
    public GameObject goldPrefab; // 골드 프리팹을 참조하는 변수

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
        else if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Idle();
        }
    }

    void MoveTowardsPlayer()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
        
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackPower); // 플레이어에게 데미지 적용
        }
    }

    void Idle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        
        DropLoot(); // 사망 시 아이템 및 골드 드랍
        
        Destroy(gameObject, 2f); // 일정 시간 후 오브젝트 삭제
    }

    void DropLoot()
{
    // 드랍할 골드 양 설정 (범위 내에서 랜덤)
    int goldAmount = Random.Range(minGoldDrop, maxGoldDrop);
    
    GameObject gold = Instantiate(goldPrefab, transform.position, Quaternion.identity);
    
    GoldItem goldItem = gold.GetComponent<GoldItem>();
    if (goldItem != null)
    {
        goldItem.goldAmount = goldAmount; // 드랍된 골드 양 설정
    }
}
}