using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LootLockerAuthentication : MonoBehaviour
{
    public TMP_InputField logInEmailInputField;
    public TMP_InputField logInPasswordInputField;

    public TMP_InputField signUpEmailInputField;
    public TMP_InputField signUpPasswordInputField;

    public TMP_InputField resetEmailInputField;

    public GameObject Authentication;
    public GameObject LogInPage;
    public GameObject SignUpPage;
    public GameObject ResetPasswordPage;
    public GameObject VerifyPage;
    public GameObject OperationFailed;
    public GameObject LogInButton;
    public GameObject SignUpButton;
    public GameObject Wait;

    public GameObject cameraTransitionObject;

    public GameObject signUpButton;
    public GameObject logInButton;

    public GameObject mainMenu;

    bool rememberMe = true;

    private string logInEmail = "Enter your email...";
    private string logInPassword = "Enter your password...";

    private string signUpEmail = "Enter your email...";
    private string signUpPassword = "Enter your password...";

    private string resetEmail = "Enter your email...";

    private void Start()
    {
        Wait.SetActive(true);
        LogIn();
    }

    private void UpdateEmailFromInputField()
    {
        if (logInEmailInputField != null)
        {
            logInEmail = logInEmailInputField.text;
        }

        if (signUpEmailInputField != null)
        {
            signUpEmail = signUpEmailInputField.text;
        }

        if (resetEmailInputField != null)
        {
            resetEmail = resetEmailInputField.text;
        }
    }

    private void UpdatePasswordFromInputField()
    {
        if (logInPasswordInputField != null)
        {
            logInPassword = logInPasswordInputField.text;
        }

        if (signUpPassword != null)
        {
            signUpPassword = signUpPasswordInputField.text;
        }

        if (signUpPassword.Length < 8)
        {
            if (signUpButton != null)
            {
                signUpButton.SetActive(false);
            }
        }
        else
        {
            if (signUpButton != null)
            {
                signUpButton.SetActive(true);
            }
        }
    }

    public void Update()
    {
        UpdateEmailFromInputField();
        UpdatePasswordFromInputField();

        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved player name: " + response.name);
                Debug.Log(response.name);
            }
            else
            {
                Debug.Log("Error getting player name");
            }
        });
    }

    public void SignUp()
    {
        LootLockerSDKManager.WhiteLabelSignUp(signUpEmail, signUpPassword, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Error while creating user");

                OperationFailed.SetActive(true);

                return;
            }

            Debug.Log("User created successfully");

            VerifyPage.SetActive(true);
            SignUpPage.SetActive(false);

            OperationFailed.SetActive(false);

        });
    }

    public void LogIn()
    {
        LootLockerSDKManager.WhiteLabelLoginAndStartSession(logInEmail, logInPassword, rememberMe, response =>
        {
            if (!response.success)
            {
                if (!response.LoginResponse.success)
                {
                    Debug.Log("error while logging in");

                    OperationFailed.SetActive(true);

                    LogInButton.SetActive(true);
                    SignUpButton.SetActive(true);

                    Wait.SetActive(false);
                }
                else if (!response.SessionResponse.success)
                {
                    Debug.Log("error while starting session");

                    OperationFailed.SetActive(true);

                    LogInButton.SetActive(true);
                    SignUpButton.SetActive(true);

                    Wait.SetActive(false);
                }
                return;

                Authentication.SetActive(false);

                mainMenu.SetActive(true);

                CallMoveCameraToTarget1();
            }

            LootLockerSDKManager.CheckWhiteLabelSession(response =>
            {
                if (response)
                {
                    LootLockerSDKManager.StartWhiteLabelSession((response) =>
                    {
                        if (!response.success)
                        {
                            Debug.Log("error starting LootLocker session");

                            OperationFailed.SetActive(true);

                            return;
                        }

                        Debug.Log("session started successfully");

                        Authentication.SetActive(false);
                        CallMoveCameraToTarget1();
                    });
                }
                else
                {
                    Debug.Log("session is NOT valid, we should show the login form");

                    LogInButton.SetActive(true);
                    SignUpButton.SetActive(true);
                    Wait.SetActive(false);
                }
            });

        }, rememberMe);
    }

    public void ResetPassword()
    {
        LootLockerSDKManager.WhiteLabelRequestPassword(resetEmail, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("error requesting password reset");

                OperationFailed.SetActive(true);

                return;
            }

            Debug.Log("requested password reset successfully");

            LogInButton.SetActive(true);
            SignUpButton.SetActive(true);
            ResetPasswordPage.SetActive(false);

        });
    }

    public void CallMoveCameraToTarget1()
    {
        if (cameraTransitionObject != null)
        {
            CameraTransition cameraTransition = cameraTransitionObject.GetComponent<CameraTransition>();

            if (cameraTransition != null)
            {
                cameraTransition.MoveCameraToTarget1();
            }
            else
            {
                Debug.LogError("CameraTransition component not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("CameraTransition GameObject is not assigned.");
        }
    }
}
