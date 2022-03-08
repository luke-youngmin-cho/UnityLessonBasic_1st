using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
public class NotesMaker : MonoBehaviour
{
    SongData songData;
    KeyCode[] keyCodes = { KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L };

    public VideoPlayer vp;
    public bool onRecord
    {
        set
        {
            if (value)
                StartRecording();
            else
                StopRecording();
        }
        get { return vp.isPlaying; }
    }
    private void Update()
    {
        if (onRecord)
            Recording();
    }
    private void StartRecording()
    {
        songData = new SongData();
        songData.videoName = vp.clip.name;
        vp.Play();
    }
    private void Recording()
    {
        foreach (KeyCode keyCode in keyCodes)
        {
            if(Input.GetKeyDown(keyCode))
                CreateNoteData(keyCode);
        }
        if (Input.GetKeyDown(KeyCode.Insert))
            SaveSongData();
    }
    private void StopRecording()
    {
        songData = null;
        vp.Stop();
    }
    private void CreateNoteData(KeyCode keyCode)
    {
        NoteData noteData = new NoteData();

        float tmpTime = (float)vp.time * 1000;
        int tmpTimeInt = (int)tmpTime;
        Debug.Log(tmpTimeInt);
        if (tmpTime % 10 < 5)
        {
            tmpTimeInt /= 10;
        }
        else
        {
            tmpTimeInt /= 10;
            tmpTimeInt++;
        }
        Debug.Log(tmpTimeInt);
        float roundedTime = (float)tmpTimeInt / 100;
        Debug.Log(roundedTime);
        noteData.time = roundedTime;
        noteData.keyCode = keyCode;
        songData.notes.Add(noteData);
        Debug.Log($"Created note {keyCode}");
    }
    private void SaveSongData()
    {
        Debug.Log("Save SongData ");
        // panel 만 띄우고 선택시 디렉토리 문자열 반환 (저장하지 않음)
        string dir = EditorUtility.SaveFilePanel("저장할 곳을 지정하세요", "", $"{songData.videoName}", "json");
        // 실제 song data 를 json 포멧으로 저장
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(songData));
    }
}