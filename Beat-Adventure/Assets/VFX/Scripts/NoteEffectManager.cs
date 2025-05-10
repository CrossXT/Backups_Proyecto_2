using UnityEngine;

public class NoteEffectManager : MonoBehaviour
{
    public static NoteEffectManager Instance;

    public GameObject hitEffectPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnHitEffect(Vector3 position)
    {
        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 1.5f); // Destruye tras 1.5s si no está configurado en el propio prefab
        }
    }
}
