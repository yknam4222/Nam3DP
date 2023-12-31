using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; } = null;

    public AudioSource audioSource;
    public Dictionary<string, AudioClip> soundDictionary;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
        {
            Destroy(this.gameObject);
        }
        // 초기화
        audioSource = GetComponent<AudioSource>();
        soundDictionary = new Dictionary<string, AudioClip>();
        // 사운드 불러오기
    }

    public void PlaySound(string _key)
    {
        if(!soundDictionary.ContainsKey(_key))
        {
            Debug.Log("없음");
        }

        var clip = soundDictionary[_key];
        audioSource.PlayOneShot(clip);
    }

    public void PlayBGM(string _key)
    {
        if (!soundDictionary.ContainsKey(_key))
        {
            Debug.Log("없음");
        }

        audioSource.clip = soundDictionary[_key];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void AddSoundClip(AudioClip _clip)
    {
        if (soundDictionary.ContainsKey(_clip.name))
        {
            Debug.Log("있음");
        }

        soundDictionary[_clip.name] = _clip;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
