using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;  // El prefab de la nota
    public float spawnInterval = 1f;  // Intervalo entre la generación de notas (ajústalo según el ritmo)
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            // Instancia una nueva nota en la posición de spawn
            Instantiate(notePrefab, transform.position, Quaternion.identity);
            timer = 0f;  // Reinicia el temporizador
        }
    }
}
