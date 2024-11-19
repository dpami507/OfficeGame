using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public GameObject soundPrefab;
    public Slider soundSlider;

    [SerializeField]
    Sound[] sounds;

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
                Destroy(soundPrefab_, 1f);
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
