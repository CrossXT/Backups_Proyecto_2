using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject[] notePrefabs; // Prefabs de notas (una por cada línea)
    public Transform[] spawnPositions; // Posiciones donde aparecerán las notas
    public float[] noteSpeeds; // Velocidades de cada línea
    public float spawnRate = 1f; // Tiempo entre cada aparición de nota

    void Start()
    {
        StartCoroutine(SpawnNotes());
    }

    IEnumerator SpawnNotes()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            for (int i = 0; i < notePrefabs.Length; i++) // Genera una nota en cada línea
            {
                GameObject newNote = Instantiate(notePrefabs[i], spawnPositions[i].position, Quaternion.identity);

                // Ajusta la velocidad de la nota con el script NoteMovement
                NoteMovement noteScript = newNote.GetComponent<NoteMovement>();
                if (noteScript != null)
                {
                    noteScript.speed = noteSpeeds[i]; // Asigna la velocidad de la línea
                }
            }
        }
    }
}
