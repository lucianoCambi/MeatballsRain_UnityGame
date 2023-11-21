using PlayFab;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button leadboardButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button userButton;
    [SerializeField] private GameObject messageLogin;

   

    private void Awake() {
        
        playButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            Loader.Load(Loader.Scene.GameScene);
        });
        
        leadboardButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            if (PlayFabClientAPI.IsClientLoggedIn()) {
                LeadboardUI.Instance.Show();
            }
            else {
                StartCoroutine(ShowAllertLogin());
            }
        });

        settingsButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            OptinsUI.Instance.Show();

        });

        userButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            UserUI.Instance.Show();
        });

        //quitButton.onClick.AddListener(() => {
        //    Application.Quit();
        //});

        Time.timeScale = 1f;
    }


    private void Start() {
        messageLogin.SetActive(false);
    }


    IEnumerator ShowAllertLogin() {
        messageLogin.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        messageLogin.SetActive(false);
    }
}
