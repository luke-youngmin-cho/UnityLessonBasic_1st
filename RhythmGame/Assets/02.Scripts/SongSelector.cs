using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class SongSelector : MonoBehaviour
{
    public static SongSelector instance;
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
        DontDestroyOnLoad(instance);
    }
    public bool isPlayable { get { return songData != null && clip != null; } }
    [HideInInspector] public VideoClip clip;
    [HideInInspector] public SongData songData;
    public void LoadSong(string videoName)
    {
        clip = Resources.Load<VideoClip>($"VideoClips/{videoName}");
        TextAsset songDataText = Resources.Load<TextAsset>($"SongDatas/{videoName}");
        songData = JsonUtility.FromJson<SongData>(songDataText.ToString());
    }
}
