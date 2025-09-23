using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void UpdateUI()
    {
        RoamUI.Instance.UpdateInventoryUI();
        switch(GameManager.Instance.gameState)
        {
            case GameState.Roam:
                RoamUI.Instance.roamUICanvas.SetActive(true);
                break;

            case GameState.Slice:
                RoamUI.Instance.roamUICanvas.SetActive(false);
                break;

            case GameState.Fry:
                // Future implementation
                break;

            case GameState.Shake:
                // Future implementation
                break;

            default:
                break;
        }

    }

}
