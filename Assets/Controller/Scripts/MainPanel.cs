using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainPanel : MonoBehaviour
{
   [Header("Options")]
    public Slider volumeFX;
    public Slider volumeMaster;
    public Toggle mute;
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clipSound;
    public float lastVolume;

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject levelSelectPanel;

    private void Awake()
    {
        volumeFX.onValueChanged.AddListener(ChangeVolumeFX);
        volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);
    }

    public void SetMute()
    {
        if (mute.isOn)
        {
            mixer.GetFloat("VolMaster" , out lastVolume);
            mixer.SetFloat("VolMaster" , -80);
        }
        else
            mixer.SetFloat("VolMaster" , lastVolume);
    }

    public void OpenPanel (GameObject panel)
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);

        panel.SetActive(true);
        PlaySoundButton();
    }
    public void ChangeVolumeMaster(float v)
    {
        mixer.SetFloat("VolMaster" , v);
    }
    public void ChangeVolumeFX(float v)
    {
        mixer.SetFloat("VolFx" , v);
    }

    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(clipSound);
    }
}
