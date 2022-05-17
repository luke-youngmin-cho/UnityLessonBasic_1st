using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� �����͸� ����/�а�/�����ϰ�/����� Ŭ����
/// </summary>
public class PlayerDataManager
{
    #region �̱���
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
    #endregion

    public static string dirPath; // �÷��̾���͸� ������ ���丮 ���, ������ ȣ������ �����ϸ� �ȵ�.

    /// <summary>
    /// ���丮 ��� �ʱ�ȭ
    /// </summary>
    public PlayerDataManager()
    {
        dirPath = $"{Application.persistentDataPath}/PlayerData"; // Application.persistentDataPath �� �ü�� ���� �ٸ�.
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
            SaveData(); // ������ �����Ҷ� ���嵵 ������
        }
    }

    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    /// <param name="nickName"> �÷��̾� �̸� </param>
    public static PlayerData CreateData(string nickName)
    {
        // ���丮 �������� ������ ���� ����
        if (System.IO.Directory.Exists(dirPath) == false)
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }

        data = new PlayerData(nickName); // �ش� �г������� �� �÷��̾� ������ ��ü ����

        string jsonPath = dirPath + $"/Player_{nickName}.json"; // �÷��̾� ������ ��ü�� serialize �ؼ� ������ json ���� �ؽ�Ʈ ���� �̸�
        string jsonData = JsonUtility.ToJson(data); // �÷��̾� �����͸� Serialize ���Ѽ� ���ڿ��� ��ȯ
        System.IO.File.WriteAllText(jsonPath, jsonData); // Serialize �� �÷��̾� �����͸� ���� ��ο��ٰ� ���� ����.

        return data;
    }

    /// <summary>
    /// �÷��̾� �����͸� �о� ��
    /// </summary>
    /// <param name="nickName"> �о�� �������� �÷��̾� �̸� </param>
    /// <exception cref="System.Exception"> �ҷ����� ���� </exception>
    public static PlayerData LoadData(string nickName)
    {
        string jsonPath = dirPath + $"/Player_{nickName}.json"; // �ҷ��� ������ ���

        // �ҷ��� ������ �����ϴ� üũ
        if (System.IO.File.Exists(jsonPath))
        {
            string jsonData = System.IO.File.ReadAllText(jsonPath); // �ش� ���� ��ο��� ������ ����
            data = JsonUtility.FromJson<PlayerData>(jsonData); // ���� �ؽ�Ʈ ������ PlayerData Ÿ������ Deserialize �ؼ� PlayerData �ν��Ͻ�ȭ
            return data;
        }
        else
            throw new System.Exception("�÷��̾� ������ �ҷ����� ����");
    }

    /// <summary>
    /// ��� �÷��̾� �����͸� �ε��ؼ� ��ȯ��
    /// </summary>
    public static PlayerData[] GetAllDatas()
    {
        string[] jsonPaths = System.IO.Directory.GetFiles(dirPath); // �÷��̾� �����Ͱ� ����Ǵ� ��ο��� ��� �÷��̾� ������ ���� ��� �о��.
        PlayerData[] playerDatas = new PlayerData[jsonPaths.Length]; // �÷��̾� ������ ��ü���� ���� �迭

        // �о�� ��� ����� ���ϵ��� �ؽ�Ʈ�� ���� �� PlayerData Ÿ������ Deserialize �ؼ� PlayerData ��ü �ν��Ͻ�ȭ
        for (int i = 0; i < jsonPaths.Length; i++)
        {
            string jsonData = System.IO.File.ReadAllText(jsonPaths[i]);
            playerDatas[i] = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        return playerDatas;
    }


    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    public static void SaveData()
    {
        // ������ ������ ��ȯ
        if (data == null)
            return;

        string jsonPath = dirPath + $"/Player_{_data.nickName}.json"; // ������ ���� ���
        string jsonData = JsonUtility.ToJson(_data); // �÷��̾� ������ Serialize �ؼ� json ������ ���ڿ��� ��ȯ
        System.IO.File.WriteAllText(jsonPath, jsonData); // ��ȯ�� json ������ ���ڿ��� ���Ϸ� ��.
    }

    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    /// <param name="nickName"> ������ �÷��̾� �̸� </param>
    /// <returns> ���� ���� : true , ���� : false </returns>
    public static bool RemoveData(string nickName)
    {
        string jsonPath = dirPath + $"/Player_{nickName}.json"; // ������ ������ ���

        // ������ ������ ����
        if (System.IO.File.Exists(jsonPath))
        {
            System.IO.File.Delete(jsonPath);
            return true;
        }
        return false;
    }
}
