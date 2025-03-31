using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
//using Newtonsoft.Json;




public class Login : MonoBehaviour
{
    [Header("UI")]
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;

    public void RegisterButton ()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result) 
    {
        messageText.text = "Dang Ki Thanh Cong";
    }
    void OnError(PlayFabError error)
    {
        messageText.text = "Error: " + error.ErrorMessage;
        Debug.LogError(error.GenerateErrorReport());
    }

    public void LoginButton()
    {

    }

    public void ResetPasswordButton()
    {
        
    }


}
