using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptinsUI : MonoBehaviour

{
    public static OptinsUI Instance { get; private set; } 


    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;

    

    private void Awake() {
        Instance = this;

        soundEffectButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();

        });
        musicButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();

        });
        closeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            Hide();

        });
    }

    private void Start() {
        UpdateVisual();

        Hide();
    }


    private void UpdateVisual() {
        soundEffectText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }


    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    } 
}
