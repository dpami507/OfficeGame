using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public GameObject soundPrefab;
    public Slider soundSlider;

    public Slider musicSlider;
    public AudioSource musicSource;
    public float musicVolume;

    [SerializeField]
    Sound[] sounds;

    private void Update()
    {
        musicSource.volume = musicSlider.value * musicVolume;
    }

    public void PlaySound(string sound)
    {
        foreach (Sound clip in sounds)
        {
            if (sound == clip.name)
            {
                GameObject soundPrefab_ = Instantiate(soundPrefab, FindFirstObjectByType<PlayerManager>().transform.position, Quaternion.identity);
                AudioSource audioSource = soundPrefab_.GetComponent<AudioSource>();
                audioSource.volume = clip.volume * soundSlider.value;
                audioSource.clip = clip.clip;
                audioSource.Play();
                float length = audioSource.clip.length;
                Destroy(soundPrefab_, length);
            }
        }
    } 
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume;
}
