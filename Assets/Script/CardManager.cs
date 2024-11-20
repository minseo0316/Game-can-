using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    public GameObject cardSelectionCanvas;
    public GameObject[] cardPrefabs;  // Inspector에서 여러 카드 프리팹을 할당할 수 있는 배열
    public Transform cardContainer;
    public int cardsToChoose = 3;

    private List<Card> allCards = new List<Card>();

    void Start()
    {
        InitializeCards();
        cardSelectionCanvas.SetActive(false);
    }

    void InitializeCards()
    {
        allCards.Clear();
        allCards.Add(new Card(Card.CardType.Type1, Card.CardEffect.IncreaseMaxHealth, 10));
        allCards.Add(new Card(Card.CardType.Type2, Card.CardEffect.RestoreHealth, 20));
        allCards.Add(new Card(Card.CardType.Type3, Card.CardEffect.IncreaseMaxMana, 10));
        allCards.Add(new Card(Card.CardType.Type1, Card.CardEffect.RestoreMana, 20));
        allCards.Add(new Card(Card.CardType.Type2, Card.CardEffect.IncreasePower, 5));
        allCards.Add(new Card(Card.CardType.Type3, Card.CardEffect.IncreaseGuard, 5));
    }

    public void ShowCardSelection()
    {
        Debug.Log("ShowCardSelection called in CardManager");
        cardSelectionCanvas.SetActive(true);
        List<Card> selectedCards = GetRandomCards(cardsToChoose);

        // 기존 카드 제거
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Card card in selectedCards)
        {
            GameObject cardPrefab = cardPrefabs[(int)card.type];  // 카드 타입에 따라 프리팹 선택
            GameObject cardObject = Instantiate(cardPrefab, cardContainer);
            CardUI cardUI = cardObject.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardUI.SetCard(card);
                cardUI.OnCardSelected += OnCardChosen;
            }
            else
            {
                Debug.LogError("CardUI component not found on card prefab");
            }
        }
    }

    List<Card> GetRandomCards(int count)
    {
        List<Card> randomCards = new List<Card>();
        List<Card> tempCards = new List<Card>(allCards);

        for (int i = 0; i < count; i++)
        {
            if (tempCards.Count > 0)
            {
                int randomIndex = Random.Range(0, tempCards.Count);
                randomCards.Add(tempCards[randomIndex]);
                tempCards.RemoveAt(randomIndex);
            }
        }

        return randomCards;
    }

    void OnCardChosen(Card chosenCard)
    {
        // 선택된 카드의 효과를 적용합니다
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        chosenCard.ApplyEffect(playerHealth);

        // 카드 선택 캔버스를 비활성화하고 카드 오브젝트들을 제거합니다
        cardSelectionCanvas.SetActive(false);
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }
    }
}