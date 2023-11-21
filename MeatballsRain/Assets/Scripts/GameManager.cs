using PlayFab;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }



    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;


    private bool scoreSent = false;

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }


    private State state;
    private float countdownToStartTimer = 3f;
    private float waitingToStartTimer = 1f;
    private bool isGamePaused = false;


   


    private void Awake() {
        Instance = this;

        state = State.WaitingToStart;
    }
    private void Start() {
        PauseButtonUI.Instance.OnPauseAction += PauseButtonUI_OnPauseAction;
    }


    

    private void PauseButtonUI_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:

                scoreSent = false;

                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
            case State.GameOver:
                if (!scoreSent) { 
                    SendScore();
                    scoreSent = true;
                }
                
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
        }
        
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;
            

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else {
            Time.timeScale = 1f;

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    internal void GameOver() {
        state = State.GameOver;
    }

    internal bool IsPaused() {
        return isGamePaused;
    }

    private void SendScore() {
        if (PlayFabClientAPI.IsClientLoggedIn()) {
            PlayfabManager.Instance.SendLeaderboard(Planet.Score);
        }
    }

}
