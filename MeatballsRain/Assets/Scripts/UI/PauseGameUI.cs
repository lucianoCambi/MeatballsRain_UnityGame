using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;

    

    private void Start() {

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

   

    private void GameManager_OnGamePaused(object sender, System.EventArgs e) {
      
        Show();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }


    private void Awake() {
        menuButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            Loader.Load(Loader.Scene.MainMenuScene);
            SoundManager.Instance.PlaySoundButton();
        });
        resumeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            GameManager.Instance.TogglePauseGame();
            SoundManager.Instance.PlaySoundButton();
        });
        optionsButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            OptinsUI.Instance.Show();
            SoundManager.Instance.PlaySoundButton();
        });


    }



    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
