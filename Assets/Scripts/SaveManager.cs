using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

    }

    public bool isSavingToJson;

    #region || -------- General Section -------- ||

    public void SaveGame()
    {
        AllGameData data = new AllGameData();

        data.playerData = GetPlayerData();

        SaveAllGameData(data);
    }

    private PlayerData GetPlayerData()
    {
        //da fare anche con le stats e l'inventario
        float[] playerPosAndRot = new float[6];
        playerPosAndRot[0] = PlayerState.Instance.playerBody.transform.position.x;
        playerPosAndRot[1] = PlayerState.Instance.playerBody.transform.position.y;
        playerPosAndRot[2] = PlayerState.Instance.playerBody.transform.position.z;

        playerPosAndRot[3] = PlayerState.Instance.playerBody.transform.rotation.x;
        playerPosAndRot[4] = PlayerState.Instance.playerBody.transform.rotation.y;
        playerPosAndRot[5] = PlayerState.Instance.playerBody.transform.rotation.z;

        return new PlayerData(playerPosAndRot);
    }

    public void SaveAllGameData(AllGameData gameData)
    {
        if (isSavingToJson)
        {
            // SaveGameDataToJsonFile(gameData);
        }
        else
        {
            SaveGameDataToBinaryFile(gameData);
        }
    }


    #endregion

    #region || -------- To Binary Section -------- ||

    public void SaveGameDataToBinaryFile(AllGameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/save_game.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();

        print("Data saved to" + Application.persistentDataPath + "/save_game.bin");
    }

    public AllGameData LoadGameDataFromBinaryFile()
    {
        string path = Application.persistentDataPath + "/save_game.bin";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AllGameData data = formatter.Deserialize(stream) as AllGameData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region || -------- Settings Section -------- ||

    #region || -------- Volume Settings -------- ||
    [System.Serializable]
    public class VolumeSettings
    {
        public float music;
        public float effects;
        public float master;
    }

    public void SaveVolumeSettings(float _music, float _effects, float _master)
    {
        VolumeSettings volumeSettings = new VolumeSettings()
        {
            music = _music,
            effects = _effects,
            master = _master,
        };

        PlayerPrefs.SetString("Volume", JsonUtility.ToJson(volumeSettings));
        PlayerPrefs.Save();

        print("Saved to Player Pref");
    }

    public VolumeSettings LoadVolumeSettings()
    {
        return JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("Volume"));
    }

    public float LoadMusicSettings()
    {
        var volumeSettings = JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("Volume"));
        return volumeSettings.music;
    }

    #endregion



    #endregion
}
