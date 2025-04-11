using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    // UI
    public InputField usernameInput;
    public InputField passwordInput;
    public Text errorMessage;
    public Button loginButton;
    public Button registerButton;

    private string filePath = @"E:\3\account.txt"; // Lưu File

    void Start()
    {
        // Nút đăng nhập và đăng kí
        loginButton.onClick.AddListener(OnLoginClick);
        registerButton.onClick.AddListener(OnRegisterClick);
    }

    // Đăng nhập
    void OnLoginClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (CheckLogin(username, password))
        {
            errorMessage.text = "Đăng nhập thành công!";
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            errorMessage.text = "Thông tin đăng nhập không chính xác!";
        }
    }

    // Load màn hình đăng nhập
    void OnRegisterClick()
    {
        SceneManager.LoadScene("Register");
    }

    // Kiểm tra thông tin đăng nhập
    bool CheckLogin(string username, string password)
    {
        if (File.Exists(filePath))
        {
            string[] accounts = File.ReadAllLines(filePath);
            foreach (string account in accounts)        //kiểm tra thông tin tài khoản
            {
                string[] accountDetails = account.Split('\t');
                if (accountDetails[0] == username && accountDetails[1] == password)
                //kiểm tra thông tin tài khoản có đúng hay không
                {
                    return true;
                }
            }
        }
        return false;
    }
}
