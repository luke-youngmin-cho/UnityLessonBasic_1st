using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 데이터를 쓰고/읽고/저장하고/지우는 클래스
/// </summary>
public class PlayerDataManager
{
    #region 싱글톤
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

    public static string dirPath; // 플레이어데이터를 저장할 디렉토리 경로, 생성자 호출전에 참조하면 안됨.

    /// <summary>
    /// 디렉토리 경로 초기화
    /// </summary>
    public PlayerDataManager()
    {
        dirPath = $"{Application.persistentDataPath}/PlayerData"; // Application.persistentDataPath 는 운영체제 마다 다름.
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
            SaveData(); // 데이터 수정할때 저장도 같이함
        }
    }

    /// <summary>
    /// 플레이어 데이터 생성
    /// </summary>
    /// <param name="nickName"> 플레이어 이름 </param>
    public static PlayerData CreateData(string nickName)
    {
        // 디렉토리 존재하지 않으면 새로 생성
        if (System.IO.Directory.Exists(dirPath) == false)
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }

        data = new PlayerData(nickName); // 해당 닉네임으로 새 플레이어 데이터 객체 생성

        string jsonPath = dirPath + $"/Player_{nickName}.json"; // 플레이어 데이터 객체를 serialize 해서 저장할 json 포맷 텍스트 파일 이름
        string jsonData = JsonUtility.ToJson(data); // 플레이어 데이터를 Serialize 시켜서 문자열로 변환
        System.IO.File.WriteAllText(jsonPath, jsonData); // Serialize 된 플레이어 데이터를 파일 경로에다가 파일 쓰기.

        return data;
    }

    /// <summary>
    /// 플레이어 데이터를 읽어 옴
    /// </summary>
    /// <param name="nickName"> 읽어올 데이터의 플레이어 이름 </param>
    /// <exception cref="System.Exception"> 불러오기 실패 </exception>
    public static PlayerData LoadData(string nickName)
    {
        string jsonPath = dirPath + $"/Player_{nickName}.json"; // 불러올 파일의 경로

        // 불러올 파일이 존재하는 체크
        if (System.IO.File.Exists(jsonPath))
        {
            string jsonData = System.IO.File.ReadAllText(jsonPath); // 해당 파일 경로에서 파일을 읽음
            data = JsonUtility.FromJson<PlayerData>(jsonData); // 읽은 텍스트 파일을 PlayerData 타입으로 Deserialize 해서 PlayerData 인스턴스화
            return data;
        }
        else
            throw new System.Exception("플레이어 데이터 불러오기 실패");
    }

    /// <summary>
    /// 모든 플레이어 데이터를 로드해서 반환함
    /// </summary>
    public static PlayerData[] GetAllDatas()
    {
        string[] jsonPaths = System.IO.Directory.GetFiles(dirPath); // 플레이어 데이터가 저장되는 경로에서 모든 플레이어 데이터 파일 경로 읽어옴.
        PlayerData[] playerDatas = new PlayerData[jsonPaths.Length]; // 플레이어 데이터 객체들을 담을 배열

        // 읽어온 모든 경로의 파일들의 텍스트를 읽은 후 PlayerData 타입으로 Deserialize 해서 PlayerData 객체 인스턴스화
        for (int i = 0; i < jsonPaths.Length; i++)
        {
            string jsonData = System.IO.File.ReadAllText(jsonPaths[i]);
            playerDatas[i] = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        return playerDatas;
    }


    /// <summary>
    /// 플레이어 데이터 저장
    /// </summary>
    public static void SaveData()
    {
        // 데이터 없으면 반환
        if (data == null)
            return;

        string jsonPath = dirPath + $"/Player_{_data.nickName}.json"; // 저장할 파일 경로
        string jsonData = JsonUtility.ToJson(_data); // 플레이어 데이터 Serialize 해서 json 포맷의 문자열로 변환
        System.IO.File.WriteAllText(jsonPath, jsonData); // 변환된 json 포맷의 문자열을 파일로 씀.
    }

    /// <summary>
    /// 플레이어 데이터 삭제
    /// </summary>
    /// <param name="nickName"> 삭제할 플레이어 이름 </param>
    /// <returns> 삭제 성공 : true , 실패 : false </returns>
    public static bool RemoveData(string nickName)
    {
        string jsonPath = dirPath + $"/Player_{nickName}.json"; // 삭제할 파일의 경로

        // 파일이 있으면 지움
        if (System.IO.File.Exists(jsonPath))
        {
            System.IO.File.Delete(jsonPath);
            return true;
        }
        return false;
    }
}
