using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
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
    string jsonPathProject;

    string jsonPathPersistant;

    string binaryPath;

    public bool isSavingToJson;

    private void Start()
    {
        jsonPathProject = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveGame.json";
        jsonPathPersistant = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveGame.json";
        binaryPath = Application.persistentDataPath + "/save_game.bin";
    }

    #region || -------- General Section -------- ||

    #region || -------- Saving -------- ||

    public void SaveGame()
    {
        AllGameData data = new AllGameData();

        data.playerData = GetPlayerData();

        SelectSavingType(data);
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


    public void SelectSavingType(AllGameData gameData)
    {
        if (isSavingToJson)
        {
            SaveGameDataToJsonFile(gameData);

        }
        else
        {
            SaveGameDataToBinaryFile(gameData);
        }
    }
    #endregion

    #region || -------- Loading -------- ||

    public AllGameData SelectLoadingType()
    {
        if (isSavingToJson)
        {
            AllGameData gameData = LoadGameDataFromJsonFile(); 
            return gameData;
        }
        else
        {
            AllGameData gameData = LoadGameDataFromBinaryFile();
            return gameData;
        }
    }

    public void LoadGame()
    {
        // Player Data
        SetPlayerData(SelectLoadingType().playerData);

        // EnvironmentData
        // setEnvironmentData
    }

    private void SetPlayerData(PlayerData playerData)
    {
        // In futuro si aggiungeranno anche le stats e l'inventario
        Vector3 loadedPosition;
        loadedPosition.x = playerData.playerPositionAndRotation[0];
        loadedPosition.y = playerData.playerPositionAndRotation[1];
        loadedPosition.z = playerData.playerPositionAndRotation[2];

        PlayerState.Instance.playerBody.transform.position = loadedPosition;

        Vector3 loadedRotation;
        loadedRotation.x = playerData.playerPositionAndRotation[3];
        loadedRotation.y = playerData.playerPositionAndRotation[4];
        loadedRotation.z = playerData.playerPositionAndRotation[5];

        PlayerState.Instance.playerBody.transform.rotation = Quaternion.Euler(loadedRotation);
    }

    public void StartLoadedGame()
    {
        SceneManager.LoadScene("Scene Canvas - Pierfabio");

        StartCoroutine(DelayLoading());
    }

    private IEnumerator DelayLoading()
    {
        yield return new WaitForSeconds(1f);

        LoadGame();
    }


    #endregion

    #endregion

    #region || -------- To Binary Section -------- ||

    public void SaveGameDataToBinaryFile(AllGameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();


        FileStream stream = new FileStream(binaryPath, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();

        print("Data saved to" + binaryPath);
    }

    public AllGameData LoadGameDataFromBinaryFile()
    {
        

        if (File.Exists(binaryPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(binaryPath, FileMode.Open);

            AllGameData data = formatter.Deserialize(stream) as AllGameData;
            stream.Close();

            print("Data loaded from" + binaryPath);

            return data;

            Time.timeScale = 1f;
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region || -------- To Json Section -------- ||

    public void SaveGameDataToJsonFile(AllGameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);

        string encrypted = EncryptionDecryption(json);

        using (StreamWriter writer = new StreamWriter(jsonPathProject))
        {
            writer.Write(encrypted);
            print("Saved Game to Json file at :" + jsonPathProject);
        };
    }

    public AllGameData LoadGameDataFromJsonFile()
    {
       using (StreamReader reader = new StreamReader(jsonPathProject))
        {
            string json = reader.ReadToEnd();

            string decrypted = EncryptionDecryption(json);

            AllGameData data = JsonUtility.FromJson<AllGameData>(decrypted);
            return data;

            Time.timeScale = 1f;
        };
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

    #region || -------- Encryption -------- ||

    public string EncryptionDecryption(string jsonString)
    {
        string keyword = "1234567";

        string result = "";

        for (int i = 0; i < jsonString.Length; i++) {
            result += (char)(jsonString[i] ^ keyword[i % keyword.Length]);
        }

        return result;
    }


    #endregion

}
