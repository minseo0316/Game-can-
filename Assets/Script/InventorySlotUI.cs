using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text amountText;
    
    private InventorySlot slot;
    private int slotIndex;

    public void SetSlot(InventorySlot newSlot, int index)
    {
        slot = newSlot;
        slotIndex = index;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (slot.item != null)
        {
            iconImage.sprite = slot.item.icon;
            iconImage.enabled = true;
            amountText.text = slot.amount.ToString();
        }
        else
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
            amountText.text = "";
        }
    }

    public void OnSlotClicked()
    {
        InventoryUI inventoryUI = GetComponentInParent<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.SelectSlot(slotIndex);
        }
    }
}