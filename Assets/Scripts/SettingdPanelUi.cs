using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SettingdPanelUi : MonoBehaviour
{
   [SerializeField] AudioMixer audioMixer;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void SetMusic(float music)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(music)*20);
    }

    public void SetSound(float sound)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sound)*20);
    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
