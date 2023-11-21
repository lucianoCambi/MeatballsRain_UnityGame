using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayfabManager : MonoBehaviour { 

     public static PlayfabManager Instance { get; private set; }

    public GameObject rowPrefab;
    public Transform rowsParent;

    [Header("UI")]

    public TextMeshProUGUI messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;


    public void Awake() {
        Instance = this;
    }

    public void RegisterButton() {
        SoundManager.Instance.PlaySoundButton();
        if (passwordInput.text.Length < 6) {
            messageText.text = "Password too short!";
            return;
        }
        var request = new RegisterPlayFabUserRequest {
            DisplayName = usernameInput.text,
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        messageText.text = "Registered and logged in!";
    }

    public void LoginButton() {
        SoundManager.Instance.PlaySoundButton();
        var request = new LoginWithEmailAddressRequest {
            Email = emailInput.text,
            Password = passwordInput.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result) {
        messageText.text = "Logged in!";
        GetCharacter();
    }

    public void GetCharacter() {
        PlayFabClientAPI.GetUserData( new GetUserDataRequest(), OnCharacterDataRecieved, OnError);

        void OnCharacterDataRecieved(GetUserDataResult result) {
            Debug.Log("Data character recieved.");
        }
    }

    public void ResetPasswordButton() {
        SoundManager.Instance.PlaySoundButton();
        var request = new SendAccountRecoveryEmailRequest {
            Email = emailInput.text,
            TitleId = "78AB3"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult result) {
        messageText.text = "Password reset mail sent!";
    }

    void OnError(PlayFabError error) {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }


    public void SendLeaderboard(int score) {
        
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName="PlatformScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }


    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Successful sent leaderboard");
    }


    public void GetLeaderboard() {
        var request = new GetLeaderboardRequest {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount= 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }


    void OnLeaderboardGet(GetLeaderboardResult result) {

        foreach(Transform item in rowsParent) {
            Destroy(item.gameObject);
        }

        foreach(var item in result.Leaderboard) {

            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString()+"m";


            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue+"m");
        }
    }

}
