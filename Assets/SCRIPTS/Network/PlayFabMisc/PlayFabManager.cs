using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;

public class PlayFabManager : MonoBehaviour
{
        public Text SystemMessage;
        [Header("LoginUI")]
        public GameObject LoginPanel;
        public InputField LoginInput;
        public InputField PasswordInput;
        [Header("RegisterUI")]
        public GameObject RegisterPanel;
        public InputField Register_LoginInput;
        public InputField Register_EmailInput;
        public InputField Register_PasswordInput;
        public InputField Register_RepeatPasswordInput;
        [Header("ResetUI")]
        public GameObject ResetPanel;
        public InputField ResetPassword_EmailInput;

        void Update()
        {
                if(Input.GetKeyDown(KeyCode.Return) && LoginPanel.activeSelf)
                {
                        LoginButton();
                } else if(Input.GetKeyDown(KeyCode.Return) && RegisterPanel.activeSelf)
                {
                        RegisterButton();
                } else if(Input.GetKeyDown(KeyCode.Return) && ResetPanel.activeSelf)
                {
                        ResetPasswordButton();
                }

                if(Input.GetKeyDown(KeyCode.Escape))
                {
                        Application.Quit();
                }
        }

        public void RegisterButton()
        {
                if(Register_PasswordInput.text.Length < 6)
                {
                        SystemMessage.text = "Password too short!";
                        return;
                }

                if(Register_RepeatPasswordInput.text != Register_PasswordInput.text)
                {
                        SystemMessage.text = "Passwords don't match!";
                        return;
                }

                var request = new RegisterPlayFabUserRequest
                {
                        Username = Register_LoginInput.text,
                        Email = Register_EmailInput.text,
                        Password = Register_PasswordInput.text,
                        RequireBothUsernameAndEmail = true

                };
                PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }

        public void LoginButton()
        {
                var request = new LoginWithPlayFabRequest
                {
                        Username = LoginInput.text,
                        Password = PasswordInput.text
                };
                PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
        }

        public void ResetPasswordButton()
        {
                var request = new SendAccountRecoveryEmailRequest
                {
                        Email = ResetPassword_EmailInput.text,
                        TitleId = "D91BA"
                };
                PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
                SystemMessage.text = "Success";
                LoginPanel.SetActive(true);
                RegisterPanel.SetActive(false);
                Debug.Log("Registered");
        }

        private void OnLoginSuccess(LoginResult result)
        {
                SystemMessage.text = "Logged in!";
                Debug.Log("Success");              
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

        private void OnPasswordReset(SendAccountRecoveryEmailResult result)
        {
                SystemMessage.text = "Check your mail";
                LoginPanel.SetActive(true);
                ResetPanel.SetActive(false);
        }

        private void OnError(PlayFabError error)
        {
                SystemMessage.text = "Something went wrong...";
                Debug.Log(error.GenerateErrorReport());
        }
}
