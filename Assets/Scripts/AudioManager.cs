using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]AudioSource musicSource, sfxSource;
    [SerializeField] AudioClip[] musicList, soundList;
    private int musicIndex = 0;
    [SerializeField]Dictionary<string, AudioClip> sfxDictionary;
    // Start is called before the first frame update
    void Start()
    {
        InitializeSFXDictionary();
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicSource.isPlaying)
        {
            playNextSongs();
        }
    }
    public void playNextSongs()
    {
        musicIndex += 1 % musicList.Length;
        musicSource.clip = musicList[musicIndex];
        musicSource.Play();
    }
    void InitializeSFXDictionary()
    {
        
        sfxDictionary = new Dictionary<string, AudioClip>();
        for (int i = 0; i < soundList.Length; i++)
        {
            sfxDictionary.Add(soundList[i].name, soundList[i]);
        }
    }
    public void PlaySFX(string sfxName)
    {
        if (sfxDictionary.ContainsKey(sfxName))
        {
            sfxSource.clip = sfxDictionary[sfxName];
            sfxSource.Play();
        }
        else
        {
            Debug.LogError("SFX not found: " + sfxName);
        }
    }
}
