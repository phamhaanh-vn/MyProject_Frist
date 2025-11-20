using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AU;
    public AudioSource MusicSource;
    public AudioSource SFXSource;
    public AudioClip BackGround;
    public AudioClip Death;
    public AudioClip Finish;
    public AudioClip Jump;
    public AudioClip Collection;
    void Awake()
    {
        if (AU == null)
        {
            AU = this;
            DontDestroyOnLoad(gameObject); // Giữ lại khi đổi scene
        }
        else
        {
            Destroy(gameObject); // tránh trùng AudioManager nếu load lại
        }
    }
    void Start()
    {
        MusicSource.clip = BackGround;
        MusicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void StopMusic()
    {
        MusicSource.Stop(); 
    }
    public void ContinueMusic()
    {
        MusicSource.Play();
    }
}
