using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button repeatButton;

   

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void Awake() {
        menuButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            Loader.Load(Loader.Scene.MainMenuScene);
            SoundManager.Instance.PlaySoundButton();
        });
        repeatButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            Loader.Load(Loader.Scene.GameScene);
            SoundManager.Instance.PlaySoundButton();
        });
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            Show();
            
           // if (PlayFabClientAPI.IsClientLoggedIn()) {
           //     PlayfabManager.Instance.SendLeaderboard(Planet.Score);
           // }
            score.text = Planet.Score.ToString("0") + "m";
        }
        else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }


}