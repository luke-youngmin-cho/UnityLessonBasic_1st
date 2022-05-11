using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager
{
    private static PlayerDataManager _instance;
    public static PlayerDataManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerDataManager();
            return _instance;
        }
    }

    public static string dirPath;
    public PlayerDataManager()
    {
        dirPath = $"{Application.persistentDataPath}/PlayerData";
    }


    private static PlayerData _data;
    public static PlayerData data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
            SaveData();
        }
    }

    public static PlayerData CreateData(string nickName)
    {
        if (System.IO.Directory.Exists(dirPath) == false)
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }

        data = new PlayerData(nickName);

        string jsonPath = dirPath + $"/Player_{nickName}.json";
        string jsonData = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(jsonPath, jsonData);

        return data;
    }

    public static PlayerData LoadData(string nickName)
    {
        string jsonPath = dirPath + $"/Player_{nickName}.json";
        if (System.IO.File.Exists(jsonPath))
        {
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            data = JsonUtility.FromJson<PlayerData>(jsonData);
            return data;
        }
        else
            throw new System.Exception("플레이어 데이터 불러오기 실패");
    }

    public static void SaveData()
    {
        if (data == null)
            return;

        string jsonPath = dirPath + $"/Player_{_data.nickName}.json";
        string jsonData = JsonUtility.ToJson(_data);
        System.IO.File.WriteAllText(jsonPath, jsonData);
    }

    public static bool RemoveData(string nickName)
    {
        string jsonPath = dirPath + $"/Player_{nickName}.json";
        if (System.IO.File.Exists(jsonPath))
        {
            System.IO.File.Delete(jsonPath);
            return true;
        }
        return false;
    }
}
