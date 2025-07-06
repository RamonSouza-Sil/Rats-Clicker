using UnityEngine;
using UnityEngine.UI;

public class MusicControll : MonoBehaviour
{
    [Header("Referências")]
    public AudioSource audioSource;
    public Slider volumeSlider;

    void Start()
    {
        
        volumeSlider.value = audioSource.volume;

        
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
