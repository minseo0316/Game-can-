using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public Button equipButton;
    public Button dropButton;
    public GameObject inventoryPanel;
    
    public GameObject equipmentPanel;
    public GameObject equipmentSlotPrefab;
    public Transform equipmentSlotsParent;

    private Inventory inventory;

    private PlayerHealth playerHealth;
    private bool isInventoryOpen = false;
    private bool isEquipmentOpen = false;
    private bool slotsCreated = false;
    private Dictionary<EquipmentType, EquipmentSlot> equipmentSlots = new Dictionary<EquipmentType, EquipmentSlot>();

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory component not found on this GameObject!");
        }
        inventoryPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        equipButton.gameObject.SetActive(false);
        CreateEquipmentSlots();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleEquipment();
        }
    }

    void CreateEquipmentSlots()
{
    foreach (Transform child in equipmentSlotsParent)
    {
        EquipmentSlot slot = child.GetComponent<EquipmentSlot>();
        if (slot != null)
        {
            equipmentSlots.Add(slot.slotType, slot);
        }
    }
}

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        Debug.Log("인벤토리 상태: " + (isInventoryOpen ? "열림" : "닫힘"));
        inventoryPanel.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            if (!slotsCreated)
            {
                CreateSlots();
                slotsCreated = true;
            }
            Debug.Log("UI 업데이트 중");
            UpdateUI();
        }
    }

    void ToggleEquipment()
    {
        isEquipmentOpen = !isEquipmentOpen;
        Debug.Log("장비창 상태: " + (isEquipmentOpen ? "열림" : "닫힘"));
        equipmentPanel.SetActive(isEquipmentOpen);

        if (isEquipmentOpen)
        {
            UpdateEquipmentUI();
        }
    }

    void CreateSlots()
    {
        if (inventory == null || inventory.slots == null)
        {
            Debug.LogError("Inventory or slots is null!");
            return;
        }

        Debug.Log(inventory.slots.Count + "개의 슬롯 생성 중");
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();
            if (slotUI == null)
            {
                Debug.LogError("InventorySlotUI component not found on slotPrefab!");
                continue;
            }
            slotUI.SetSlot(inventory.slots[i], i);
            Button slotButton = newSlot.GetComponent<Button>();
            if (slotButton != null)
            {
                int index = i;
                slotButton.onClick.AddListener(() => SelectSlot(index));
            }
            else
            {
                Debug.LogError("Button component not found on slotPrefab!");
            }
        }
    }

    public void UpdateUI()
    {
        if (!slotsCreated) return;

        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (i < slotParent.childCount)
            {
                InventorySlotUI slotUI = slotParent.GetChild(i).GetComponent<InventorySlotUI>();
                if (slotUI != null)
                {
                    slotUI.SetSlot(inventory.slots[i], i);
                    slotUI.UpdateUI();
                }
            }
            else
            {
                Debug.LogError("슬롯 GameObject가 충분하지 않습니다!");
                break;
            }
        }
    }

    void UpdateEquipmentUI()
    {
        foreach (var slot in equipmentSlots.Values)
        {
            slot.UpdateSlotUI();
        }
    }

    public void SelectSlot(int index)
{
    if (inventory.slots[index].item != null)
    {
        Item selectedItem = inventory.slots[index].item;
        itemNameText.text = selectedItem.itemName;
        itemDescriptionText.text = selectedItem.description;

        // 드롭 버튼 설정
        dropButton.onClick.RemoveAllListeners();
        dropButton.onClick.AddListener(() => DropItem(index));

        // 장착/사용 버튼 설정
        equipButton.gameObject.SetActive(true);
        equipButton.onClick.RemoveAllListeners();

        // 아이템 타입에 따라 버튼 텍스트 설정
        if (selectedItem.equipmentType == EquipmentType.None)
        {
            equipButton.GetComponentInChildren<TMP_Text>().text = "사용";
            equipButton.onClick.AddListener(() => UseItem(selectedItem, index));
        }
        else
        {
            equipButton.GetComponentInChildren<TMP_Text>().text = "장착";
            equipButton.onClick.AddListener(() => EquipItem(index));
        }
    }
    else
    {
        // 슬롯이 비어있으면 버튼 비활성화 및 텍스트 초기화
        itemNameText.text = "";
        itemDescriptionText.text = "";
        equipButton.gameObject.SetActive(false);
    }
}

    void DropItem(int index)
    {
        inventory.RemoveItem(index);
        UpdateUI();
    }
    void UseItem(Item item, int index)
{
    // 예: 포션 사용 시 체력 회복
    if (item.healthBonus > 0)
    {
        playerHealth.Heal(item.healBonus);  // 플레이어 체력 회복
    }

    // 아이템 사용 후 인벤토리에서 제거
    inventory.RemoveItem(index);
    UpdateUI();
}

    void EquipItem(int index)
{
    Item itemToEquip = inventory.slots[index].item;
    if (equipmentSlots.TryGetValue(itemToEquip.equipmentType, out EquipmentSlot targetSlot))
    {
        if (targetSlot.EquipItem(itemToEquip))
        {
            inventory.RemoveItem(index);
            UpdateUI();
            UpdateEquipmentUI();
        }
    }
}
}