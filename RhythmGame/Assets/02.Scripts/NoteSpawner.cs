using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public KeyCode keyCode;
    public GameObject notePrefab;

    public void SpawnNote()
    {
        GameObject note = Instantiate(notePrefab, transform.position, Quaternion.identity);
        note.transform.localScale = transform.lossyScale;
    }
}
