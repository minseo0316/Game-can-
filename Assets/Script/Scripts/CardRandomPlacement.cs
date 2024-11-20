using UnityEngine;

public class CardRandomPlacement : MonoBehaviour
{
    public GameObject cardPrefab; // 카드 프리팹
    public int numberOfCards = 6; // 총 카드 수
    public Vector2 startPosition = new Vector2(-3, 0); // 시작 위치
    public float xSpacing = 2.0f; // x축 간격

    void Start()
    {
        PlaceCardsRandomly();
    }

    void PlaceCardsRandomly()
    {
        // 카드 인덱스를 셔플
        int[] indices = new int[numberOfCards];
        for (int i = 0; i < numberOfCards; i++)
        {
            indices[i] = i;
        }
        ShuffleArray(indices);

        // 셔플된 순서대로 카드를 배치
        for (int i = 0; i < numberOfCards; i++)
        {
            Vector3 position = new Vector3(startPosition.x + indices[i] * xSpacing, startPosition.y, 0);
            Instantiate(cardPrefab, position, Quaternion.identity);
        }
    }

    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}