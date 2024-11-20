using UnityEngine;

public class GoldItem : MonoBehaviour
{
    public int goldAmount = 10; // 이 아이템이 줄 골드 양

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player 태그와 충돌했을 때만 실행
        {
            GoldManager goldManager = other.GetComponent<GoldManager>();
            if (goldManager != null)
            {
                goldManager.AddGold(goldAmount); // 플레이어의 골드에 추가
            }

            Destroy(gameObject); // 골드 아이템 파괴 (획득 후 사라짐)
        }
    }
}