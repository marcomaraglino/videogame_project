using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathScreenUI : MonoBehaviour
{
    public TextMeshProUGUI deathText;
    public Button respawnButton;

    void Start()
    {
        // Set up button listener
        respawnButton.onClick.AddListener(OnRespawnClick);
    }

    void OnRespawnClick()
    {
        PlayerState.Instance.Respawn();
    }
} 