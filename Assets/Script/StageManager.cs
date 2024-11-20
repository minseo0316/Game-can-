using UnityEngine;

public class StageManager : MonoBehaviour
{
    public CardManager cardManager;
    private int currentStage = 0;

    void Start()
    {
        if (cardManager == null)
        {
            Debug.LogError("CardManager is not assigned in the StageManager!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AdvanceStage();
        }
    }

    void AdvanceStage()
    {
        currentStage++;
        Debug.Log($"Stage advanced to {currentStage}");

        if (currentStage % 5 == 0)
        {
            Debug.Log("Attempting to show card selection");
            if (cardManager != null)
            {
                cardManager.ShowCardSelection();
            }
            else
            {
                Debug.LogError("CardManager is null!");
            }
        }
    }
}