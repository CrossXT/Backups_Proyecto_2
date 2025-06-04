using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SongTimer : MonoBehaviour
{
    public AudioSource musicSource;
    public TextMeshProUGUI timerText; 
    void Update()
    {
        if (musicSource.isPlaying)
        {
            float currentTime = musicSource.time;
            float totalDuration = musicSource.clip.length;
            float remainingTime = totalDuration - currentTime;

            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);

            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
