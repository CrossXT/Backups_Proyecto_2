using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CargaEscena : MonoBehaviour
{
    public Slider barraDeCarga;
    public TMP_Text textoProgreso;

    void Start()
    {
        StartCoroutine(CargarEscenaAsync(DestinyScene.nombreEscena)); // Usa la escena definida externamente
    }

    IEnumerator CargarEscenaAsync(string nombreEscena)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);
        operacion.allowSceneActivation = false;

        float tiempoCarga = 0f;

        while (!operacion.isDone)
        {
            tiempoCarga += Time.deltaTime;
            float progreso = Mathf.Clamp01(tiempoCarga / 3f); // Simula carga de 3 segundos

            barraDeCarga.value = progreso;
            textoProgreso.text = (progreso * 100).ToString("F0") + "%";

            if (progreso >= 1f)
            {
                barraDeCarga.value = 1f;
                textoProgreso.text = "100%";
                yield return new WaitForSeconds(0.5f);
                operacion.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
