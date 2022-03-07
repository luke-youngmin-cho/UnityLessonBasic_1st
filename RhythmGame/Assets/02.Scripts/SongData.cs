using System.Collections.Generic;
using UnityEngine;
public class SongData
{
    public string videoName;
    public List<NoteData> notes;

    public SongData()
    {
        notes = new List<NoteData>();
    }
}