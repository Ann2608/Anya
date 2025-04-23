using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }      // có thể truy cập từ bất kì đâu
    private AudioSource audioSource;
    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        // phát tiếp âm thanh khi sang scene khác
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip _sound)
    {
        audioSource.PlayOneShot(_sound);
    }
}
