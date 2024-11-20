using System.Collections.Generic;
using UnityEngine;
public class InventoryInput : MonoBehaviour
{
    public Inventory inventory;

    void Update()
    {
        // 숫자키로 슬롯 선택 및 아이템 사용 (예: 1번 슬롯)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.UseItem(0); // 첫 번째 슬롯의 아이템 사용
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.UseItem(1); // 두 번째 슬롯의 아이템 사용
        }

        // 필요에 따라 더 많은 키 설정 가능...
    }
}