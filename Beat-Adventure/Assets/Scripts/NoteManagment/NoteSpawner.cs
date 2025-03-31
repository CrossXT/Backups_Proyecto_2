using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json; // Asegúrate de tener Newtonsoft.Json en tu proyecto

public class NoteSpawner : MonoBehaviour
{
    public GameObject normalNotePrefab;
    public GameObject holdNotePrefab;
    public Transform[] spawnPositions; // Posiciones donde aparecerán las notas
    public AudioSource musicSource;

    private List<NoteData> noteDataList;
    private float startTime;

    void Start()
    {
        LoadNotesFromJson("Assets/Resources/notes.json"); // Ruta del archivo JSON
        StartCoroutine(SpawnNotes());
    }

    void LoadNotesFromJson(string path)
    {
        string jsonText = File.ReadAllText(path);
        noteDataList = JsonConvert.DeserializeObject<List<NoteData>>(jsonText);
    }

    IEnumerator SpawnNotes()
    {
        startTime = Time.time;
        while (true)
        {
            float elapsedTime = Time.time - startTime;

            for (int i = 0; i < noteDataList.Count; i++)
            {
                NoteData note = noteDataList[i];

                if (note.time <= elapsedTime)
                {
                    SpawnNote(note);
                    noteDataList.RemoveAt(i);
                    i--;
                }
            }
            yield return null;
        }
    }

    void SpawnNote(NoteData note)
    {
        Transform spawnPos = GetSpawnPosition(note.key);
        if (spawnPos == null) return;

        if (note.type == "normal")
        {
            Instantiate(normalNotePrefab, spawnPos.position, Quaternion.identity);
        }
        else if (note.type == "hold")
        {
            GameObject holdNote = Instantiate(holdNotePrefab, spawnPos.position, Quaternion.identity);
            HoldNote holdNoteScript = holdNote.GetComponent<HoldNote>();
            if (holdNoteScript != null)
            {
                holdNoteScript.SetNoteLength(note.duration);
            }
        }
    }

    Transform GetSpawnPosition(string key)
    {
        switch (key)
        {
            case "d": return spawnPositions[0];
            case "f": return spawnPositions[1];
            case "j": return spawnPositions[2];
            case "k": return spawnPositions[3];
            default: return null;
        }
    }
}

[System.Serializable]
public class NoteData
{
    public float time;
    public string key;
    public string type;
    public float duration;
}
