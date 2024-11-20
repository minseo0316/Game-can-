using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public EquipmentType slotType;   // 슬롯의 타입 (Weapon, Armor 등)
    public Image slotImage;          // UI 이미지 (아이템 아이콘 표시)
    public PlayerController playerController;  // 플레이어 컨트롤러 참조
    
    [SerializeField] private Item equippedItem;  // 현재 슬롯에 장착된 아이템

    public bool EquipItem(Item item)
    {
        // 무기 슬롯일 경우 여러 무기 타입을 허용 (Weapon, Bow, Staff)
        if (slotType == EquipmentType.weapon)
        {
            if (item.equipmentType == EquipmentType.weapon || 
                item.equipmentType == EquipmentType.Bow || 
                item.equipmentType == EquipmentType.Staff)
            {
                Equip(item);
                return true;
            }
        }
        // 다른 슬롯은 기존 방식 유지 (예: Armor, Boots 등)
        else if (item.equipmentType == this.slotType)
        {
            Equip(item);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 실제로 아이템을 장착하고 스탯 업데이트 및 UI 갱신을 처리하는 함수.
    /// </summary>
    /// <param name="item">장착할 아이템</param>
    private void Equip(Item item)
    {
        // 기존에 장착된 아이템이 있으면 해제 처리
        if (equippedItem != null)
        {
            playerController.UpdateEquipmentStats(equippedItem, false);
        }

        // 새로운 아이템 장착 및 스탯 업데이트
        equippedItem = item;
        playerController.UpdateEquipmentStats(item, true);
        
        UpdateSlotUI();
    }

    /// <summary>
    /// 현재 장착된 아이템의 이름을 반환. 없으면 "없음" 반환.
    /// </summary>
    /// <returns>아이템 이름 또는 "없음"</returns>
    public string GetEquippedItemName()
    {
        return equippedItem != null ? equippedItem.itemName : "없음";
    }

    /// <summary>
    /// 현재 장착된 아이템을 해제하고 반환하는 함수.
    /// </summary>
    /// <returns>해제된 아이템</returns>
    public Item UnequipItem()
    {
        Item item = equippedItem;
        
        if (item != null)
        {
            playerController.UpdateEquipmentStats(item, false);
            equippedItem = null;
            UpdateSlotUI();
        }

        return item;
    }

   /// <summary>
   /// 슬롯 UI를 업데이트하는 함수. 장착된 아이템이 있으면 이미지를 표시.
   /// </summary>
   public void UpdateSlotUI()
   {
       if (slotImage == null)
       {
           Debug.LogError($"슬롯 이미지 컴포넌트가 없습니다. SlotType: {slotType}");
           return;
       }

       Debug.Log($"UpdateSlotUI 호출됨. EquippedItem: {(equippedItem != null ? equippedItem.itemName : "없음")}");
       
       if (equippedItem != null)
       {
           slotImage.sprite = equippedItem.icon;
           slotImage.enabled = true;
           Debug.Log($"아이템 장착됨: {equippedItem.itemName}");
       }
       else
       {
           slotImage.sprite = null;
           slotImage.enabled = false;
           Debug.Log($"슬롯 비어 있음: {slotType}");
       }
   }
}