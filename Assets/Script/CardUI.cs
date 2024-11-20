using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CardUI : MonoBehaviour
{
    public TMP_Text cardNameText;
    public TMP_Text cardDescriptionText;
    public Button cardButton;

    private Card card;

    public event Action<Card> OnCardSelected;

    public void SetCard(Card newCard)
    {
        card = newCard;
        UpdateUI();
    }

    void UpdateUI()
    {
        cardNameText.text = card.effect.ToString();
        cardDescriptionText.text = $"+{card.value}";
        cardButton.onClick.AddListener(OnCardClicked);
    }

    void OnCardClicked()
    {
        OnCardSelected?.Invoke(card);
    }
}