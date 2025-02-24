using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Importa TextMeshPro

public class CargaEscena : MonoBehaviour
{
    public Slider barraDeCarga;
    public TMP_Text textoProgreso; // Cambiado a TMP_Text para TextMeshPro

    void Start()
    {
        StartCoroutine(CargarEscenaAsync("Game")); // Cambia el nombre de la escena destino
    }

    IEnumerator CargarEscenaAsync(string nombreEscena)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);
        operacion.allowSceneActivation = false; // Evita que la escena cambie inmediatamente

        float tiempoCarga = 0f; // 🔹 Se declara antes del while

        while (!operacion.isDone)
        {
            tiempoCarga += Time.deltaTime; // Simula el tiempo de carga
            float progreso = Mathf.Clamp01(tiempoCarga / 3f); // Simula que dura 3 segundos

            barraDeCarga.value = progreso;
            textoProgreso.text = (progreso * 100).ToString("F0") + "%";

            // Asegurar que la barra llegue al 100% antes de cambiar de escena
            if (progreso >= 1f)
            {
                barraDeCarga.value = 1f; // Asegura que la barra se complete
                textoProgreso.text = "100%";
                yield return new WaitForSeconds(0.5f); // Esperar un poco para la animación
                operacion.allowSceneActivation = true; // Activar la escena
            }

            yield return null;
        }
    }

}
