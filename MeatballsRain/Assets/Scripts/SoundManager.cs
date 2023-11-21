using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClip audioButton;
    [SerializeField] private Camera camera;

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    private float volume = 1f;
    private float buttonVolume = 10f;

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }
    
    public void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplayer * volume);
    }


    public void PlaySoundButton() {
        AudioSource.PlayClipAtPoint(audioButton, camera.transform.position,volume *buttonVolume);
    }

    public void ChangeVolume() {
        volume += .1f;
        if(volume > 1f) {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }

    
}
