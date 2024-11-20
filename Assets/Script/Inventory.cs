using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    public int inventorySize = 20;
    public PlayerHealth playerHealth; // 플레이어 체력 스크립트 참조

    void Start()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(Item item)
    {
        Debug.Log($"아이템 추가 시도: {item.itemName}");
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item != null && slots[i].item.itemName == item.itemName && slots[i].amount < item.maxStackSize)
            {
                slots[i].amount++;
                return true;
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = item;
                slots[i].amount = 1;
                Debug.Log($"아이템 {item.itemName}을 슬롯 {i}에 추가했습니다.");
                return true;
            }
        }
        Debug.Log("인벤토리가 가득 찼습니다. 아이템을 추가할 수 없습니다.");
        return false;
    }

    // 아이템 사용 메서드
    public void UseItem(int slotIndex)
{
    if (slotIndex >= 0 && slotIndex < slots.Count && slots[slotIndex].item != null)
    {
        Item item = slots[slotIndex].item;

        // 포션일 경우 체력 회복
        if (item.healBonus > 0)  // healBonus가 0보다 큰 경우에만 체력 회복
        {
            playerHealth.Heal(item.healBonus);  // Heal 메서드 호출하여 체력 회복
            slots[slotIndex].amount--;  // 포션 수량 감소

            if (slots[slotIndex].amount <= 0)  // 포션 수량이 0이 되면 슬롯 비우기
            {
                slots[slotIndex].Clear();
            }

            Debug.Log($"{item.itemName} 사용됨. {item.healBonus} 만큼 체력 회복.");
        }
    }
}

    // 아이템 제거 메서드
    public Item RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Count)
        {
            Item removedItem = slots[slotIndex].item;
            slots[slotIndex].Clear();
            return removedItem;
        }
        return null;
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;

    public void Clear()
    {
        item = null;
        amount = 0;
    }
}