using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeadboardUI : MonoBehaviour {



    [SerializeField] private Button closeButton;

   
    public static LeadboardUI Instance { get; private set; }

    private void Awake() {
        Instance = this;

        closeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            Hide();

        });
    }
    // Start is called before the first frame update
    private void Start() {


        Hide();
    }

    public void Show() {
        PlayfabManager.Instance.GetLeaderboard();
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}