using UnityEngine;

public enum EquipmentType
{
    None,   // 장착할 수 없는 일반 아이템
    weapon,
    armor,
    boots,
    glove,
    ring,
    belt,
    necklace,
    earring,
    Bow,
    Staff
}




[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;          // 아이템 이름
    public string description;       // 아이템 설명
    public Sprite icon;              // 아이템 아이콘
    public int maxStackSize = 99;    // 최대 스택 크기
    public EquipmentType equipmentType = EquipmentType.None;  // 장비 종류

    public int healBonus;            // 포션의 체력 회복량 (포션일 경우)
    public int price;                // 아이템 가격

    // 장비 보너스 스탯
    public int PowerBonus;           // 공격력 보너스
    public int GuardBonus;           // 방어력 보너스
    public int healthBonus;          // 체력 보너스
    public int MagicBonus;           // 마법 보너스
    public float AttackSpeedBonus;   // 공격 속도 보너스
    public int CrticalBonus;        // 치명타 보너스
}