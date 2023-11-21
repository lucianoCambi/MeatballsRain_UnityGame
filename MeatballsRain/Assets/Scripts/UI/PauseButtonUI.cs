using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonUI : MonoBehaviour {

    public static PauseButtonUI Instance { get; private set; }

    [SerializeField] private Button pauseButton;

    public event EventHandler OnPauseAction;

   
    private void Awake() {
        Instance = this;

        pauseButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySoundButton();
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        });
    }
}
