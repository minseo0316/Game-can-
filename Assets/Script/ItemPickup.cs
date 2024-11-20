using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"충돌 감지: {collision.gameObject.name}");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어 태그 감지");
            Inventory playerInventory = collision.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                Debug.Log("플레이어 인벤토리 컴포넌트 찾음");
                bool itemAdded = playerInventory.AddItem(item);
                if (itemAdded)
                {
                    Debug.Log($"아이템 {item.itemName}이 인벤토리에 추가됨");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log($"아이템 {item.itemName}을 인벤토리에 추가하지 못함");
                }
            }
            else
            {
                Debug.LogError("플레이어에 인벤토리 컴포넌트가 없음");
            }
        }
    }
}