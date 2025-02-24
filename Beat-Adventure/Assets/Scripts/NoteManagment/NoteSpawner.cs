using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject[] notePrefabs; // Prefabs de notas (una por cada l�nea)
    public Transform[] spawnPositions; // Posiciones donde aparecer�n las notas
    public float[] noteSpeeds; // Velocidades de cada l�nea
    public float spawnRate = 1f; // Tiempo entre cada aparici�n de nota

    void Start()
    {
        StartCoroutine(SpawnNotes());
    }

    IEnumerator SpawnNotes()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            for (int i = 0; i < notePrefabs.Length; i++) // Genera una nota en cada l�nea
            {
                GameObject newNote = Instantiate(notePrefabs[i], spawnPositions[i].position, Quaternion.identity);

                // Ajusta la velocidad de la nota con el script NoteMovement
                NoteMovement noteScript = newNote.GetComponent<NoteMovement>();
                if (noteScript != null)
                {
                    noteScript.speed = noteSpeeds[i]; // Asigna la velocidad de la l�nea
                }
            }
        }
    }
}
