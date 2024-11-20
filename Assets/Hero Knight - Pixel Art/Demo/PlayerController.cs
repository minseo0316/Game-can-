using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private EquipmentUI equipmentUI;
    public float moveSpeed = 5f; // 이동 속도
    private Animator animator; // 애니메이터
    private bool isDead = false; // 죽음 상태
    private float lastAttackTime = 0f; // 마지막 공격 시간
    public float attackCooldown = 1f; // 공격 쿨다운 시간
    public float attackRange = 1f; // 근거리 공격 범위
    public int attackDamage = 10; // 공격 데미지
    private bool isAttacking = false; // 현재 공격 중 여부
    private int level = 1; // 현재 레벨
    private int exp = 0; // 현재 경험치
    public TMP_Text levelText; // 레벨 텍스트 UI

    // 장비로 인한 추가 스텟
    private int equipmentPower = 0;
    private int equipmentGuard = 0;
    private int equipmentMagic = 0;
    private int equipmentHealth = 0;
    private int equipmentCritical = 0;
    private float equipmentAttackSpeed = 0;

    // 스텟 UI 텍스트
    public TMP_Text powerText;
    public TMP_Text healthText;
    public TMP_Text guardText;
    public TMP_Text magicText;
    public TMP_Text attackspeedText;
    public TMP_Text criticalText;

    // 원거리 무기 관련 변수
    public GameObject arrowPrefab; // 화살 프리팹 (활 사용 시)
    public GameObject magicPrefab; // 마법 프리팹 (지팡이 사용 시)
    public Transform firePoint; // 발사 위치
    public Item equippedWeapon; // 현재 장착한 무기 (Item 클래스 사용)

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
        
        UpdateLevelText(); // 초기 레벨 텍스트 업데이트
        UpdateStatsUI();
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
            Attack();
        }
    }

   private void Move()
{
    float moveX = Input.GetAxis("Horizontal");
    float moveY = Input.GetAxis("Vertical");
    Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

    // 애니메이터 파라미터 업데이트
    animator.SetFloat("xdir", moveX);
    animator.SetFloat("ydir", moveY);

    if (moveDirection.magnitude > 0)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 달리기
            transform.Translate(moveDirection * moveSpeed * 1.5f * Time.deltaTime);
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            // 걷기
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
    }
    else
    {
        // 정지 상태일 때 xDir, yDir 초기화
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }
}

   private void Attack()
   {
       if (Input.GetKeyDown(KeyCode.Z) && Time.time >= lastAttackTime + attackCooldown)
       {   
           lastAttackTime = Time.time;

           if (isAttacking) return;

           isAttacking = true;

           if (equippedWeapon != null &&  (equippedWeapon.equipmentType == EquipmentType.Bow || equippedWeapon.equipmentType == EquipmentType.Staff))
            {
                RangedAttack(); // 원거리 공격 실행 (활 또는 지팡이)
            }

           else
           {
               MeleeAttack(); // 근거리 공격 실행 (검 등)
           }

           Invoke("ResetAttack", 0.5f); // 공격 종료 후 리셋 (애니메이션에 따라 시간 조정)
       }
   }

   private void RangedAttack()
   {
       // 근거리 공격 애니메이션 재사용 (별도의 원거리 애니메이션 없음)
       animator.SetTrigger("Attack");

       GameObject projectilePrefab;

       if (equippedWeapon.equipmentType == EquipmentType.Bow)
       {
           projectilePrefab = arrowPrefab; // 활일 경우 화살 발사체 생성
       }
       else if (equippedWeapon.equipmentType == EquipmentType.Staff)
       {
           projectilePrefab = magicPrefab; // 지팡이일 경우 마법 발사체 생성
       }
       else 
       {
           return; // 다른 무기일 경우 아무것도 하지 않음.
       }

       Instantiate(projectilePrefab, firePoint.position, firePoint.rotation); 
       
       Debug.Log(equippedWeapon.itemName + "로 원거리 공격을 수행합니다!");
   }

   private void MeleeAttack()
   {
       // 근접 공격 애니메이션 실행
       animator.SetTrigger("Attack");

       // 플레이어의 현재 위치에서 공격 범위 내의 몬스터 찾기
       Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

       // 총 공격력 계산 (기본 공격력 + 장비로 인한 추가 공격력)
       int totalAttackDamage = playerHealth.power + equipmentPower;

       foreach (Collider2D hit in hits)
       {
           if (hit.CompareTag("Monster"))
           {
               MonsterAI monsterAI = hit.GetComponent<MonsterAI>();
               if (monsterAI != null)
               {
                   monsterAI.TakeDamage(totalAttackDamage); // 총 공격력을 전달하여 몬스터의 체력 감소
                   AddExperience(10); // 몬스터 처치 시 경험치 추가
                   Debug.Log("몬스터에게 " + totalAttackDamage + "의 데미지를 주었습니다!");
               }
           }
       }
   }

   private void ResetAttack()
{
    isAttacking = false;
    animator.ResetTrigger("Attack");
    
    // 공격 종료 후 자동으로 idle 상태로 전환
    animator.SetBool("isAttacking", false);
}

   public void UpdateEquipmentStats(Item item, bool equip)
   {
       int multiplier = equip ? 1 : -1;
       equipmentPower += item.PowerBonus * multiplier;
       equipmentHealth += item.healthBonus * multiplier;
       equipmentGuard += item.GuardBonus * multiplier;
       equipmentMagic += item.MagicBonus * multiplier;
       equipmentAttackSpeed += item.AttackSpeedBonus * multiplier;
       equipmentCritical += item.CrticalBonus * multiplier;

       UpdateStatsUI();
   }

   private void UpdateStatsUI()
   {
       if (powerText != null) powerText.text = $"공격력: {playerHealth.power+equipmentPower}";
       if (guardText != null) guardText.text = $"방어력: {playerHealth.maxGuard}";
       if (healthText != null) healthText.text = $"체력: {playerHealth.maxHealth}";
       if (magicText != null) magicText.text = $"주문력: {playerHealth.magic}";
       if (attackspeedText != null) attackspeedText.text = $"공격속도:{playerHealth.AttackSpeed}";
       if (criticalText != null) criticalText.text = $"치명타확률:{playerHealth.Critical}";
   }

   public void AddExperience(int amount)
   {
       exp += amount;
       CheckLevelUp();
   }

   public void CheckLevelUp()
   {
      if (exp >= 100 * level)
      {
          level++;
          exp = 0;
          Debug.Log("레벨업! 현재 레벨: " + level);
          UpdateLevelText();
      }
   }

   private void UpdateLevelText()
   {
      if (levelText != null)
      {
          levelText.text = "레벨 " + level;
      }
   }
}