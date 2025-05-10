using UnityEngine;
using UnityEngine.UI;

public class UIAudioVisualizer : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject barPrefab;
    public int numberOfBars = 64;
    public float barMaxHeight = 200f;

    private RectTransform[] bars;
    private float[] spectrumData;

    void Start()
    {
        spectrumData = new float[1024];
        bars = new RectTransform[numberOfBars];

        for (int i = 0; i < numberOfBars; i++)
        {
            GameObject bar = Instantiate(barPrefab, transform);
            bars[i] = bar.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        for (int i = 0; i < numberOfBars; i++)
        {
            float intensity = Mathf.Clamp01(spectrumData[i] * 100);
            float height = Mathf.Lerp(bars[i].sizeDelta.y, intensity * barMaxHeight, Time.deltaTime * 30);
            bars[i].sizeDelta = new Vector2(bars[i].sizeDelta.x, height);
        }
    }
}
