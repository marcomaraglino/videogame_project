using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //public float[] playerStats;
    public float[] playerPositionAndRotation;
    //public string[] inventoryContent;

    public PlayerData(float[] _playerPosAndRot) // da includere in futuro anche le stats e l'inventario
    {
        playerPositionAndRotation = _playerPosAndRot;
    }
}
