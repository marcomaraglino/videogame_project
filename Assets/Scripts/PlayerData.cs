using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
<<<<<<< Updated upstream
    //public float[] playerStats;
    public float[] playerPositionAndRotation;
    //public string[] inventoryContent;

    public PlayerData(float[] _playerPosAndRot) // da includere in futuro anche le stats e l'inventario
    {
        playerPositionAndRotation = _playerPosAndRot;
=======
    public float[] playerStats;
    public float[] playerPositionAndRotation;
    public string[] inventoryContent;
    public string[] quickSlotsContent;

    public PlayerData(float[] _playerStats, float[] _playerPosAndRot, string[] _inventoryContent, string[] _quickSlotsContent)
    {
        playerStats = _playerStats;
        playerPositionAndRotation = _playerPosAndRot;
        inventoryContent = _inventoryContent;
        quickSlotsContent = _quickSlotsContent;
>>>>>>> Stashed changes
    }
}
