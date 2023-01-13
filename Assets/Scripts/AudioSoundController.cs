using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

public class AudioSoundController : MonoBehaviour
{
    #region Variables

    [Separator("Default Settings")] 
    [SerializeField] private MMSoundManager mmSoundManager;
    
    
    [Separator("Track Settings")]
    [SerializeField] private float constMaxVolume = 1f;

    [ConditionalField(nameof(mmSoundManager), false)] [SerializeField]
    private bool master = false;
    [ConditionalField(nameof(master), false, true)] [SerializeField]
    private Slider masterSlider;
    
    [ConditionalField(nameof(mmSoundManager), false)] [SerializeField]
    private bool music = false;
    [ConditionalField(nameof(music), false, true)] [SerializeField]
    private Slider musicSlider;
    
    [ConditionalField(nameof(mmSoundManager), false)] [SerializeField]
    private bool sfx = false;
    [ConditionalField(nameof(sfx), false, true)] [SerializeField]
    private Slider sfxSlider;
    
    [ConditionalField(nameof(mmSoundManager), false)] [SerializeField]
    private bool ui = false;
    [ConditionalField(nameof(ui), false, true)] [SerializeField]
    private Slider uiSlider;
    
    #endregion

    #region GetVolume

    private void GetMaster()
    {
        if (master == false) return;
        if (masterSlider == null) return;
        masterSlider.value = (int)(masterSlider.maxValue / constMaxVolume *
                                   mmSoundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Master, false));
    }
    
    private void GetMusic()
    {
        if (music == false) return;
        if (musicSlider == null) return;
        musicSlider.value = (int)(musicSlider.maxValue / constMaxVolume *
                                  mmSoundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, false));
    }
    
    private void GetSfx()
    {
        if (sfx == false) return;
        if (sfxSlider == null) return;
        sfxSlider.value = (int)(sfxSlider.maxValue / constMaxVolume *
                                mmSoundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, false));
    }
    
    private void GetUi()
    {
        if (ui == false) return;
        if (uiSlider == null) return;
        uiSlider.value = (int)(uiSlider.maxValue / constMaxVolume *
                               mmSoundManager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.UI, false));
    }
    #endregion
    
    #region UpdateVolume

    public void UpdateMaster()
    {
        if (master == false) return;
        if (masterSlider == null) return;
        mmSoundManager.SetVolumeMaster(constMaxVolume / masterSlider.maxValue * masterSlider.value);
    }
    
    public void UpdateMusic()
    {
        if (music == false) return;
        if (musicSlider == null) return;
        mmSoundManager.SetVolumeMusic(constMaxVolume / musicSlider.maxValue * musicSlider.value);
    }
    
    public void UpdateSfx()
    {
        if (sfx == false) return;
        if (sfxSlider == null) return;
        mmSoundManager.SetVolumeSfx(constMaxVolume / sfxSlider.maxValue * sfxSlider.value);
    }
    
    public void UpdateUi()
    {
        if (ui == false) return;
        if (uiSlider == null) return;
        mmSoundManager.SetVolumeUI(constMaxVolume / uiSlider.maxValue * uiSlider.value);
    }

    #endregion

    private void Awake()
    {
        if (mmSoundManager == null) 
            return;
        
        GetMaster();
        GetMusic();
        GetSfx();
        GetUi();
    }
}
