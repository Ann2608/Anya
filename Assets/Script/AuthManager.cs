using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Để chuyển scene

[Serializable]
public class UserData
{
    public string email;
    public string passwordHash;
    public string salt; // Thêm trường salt
}

public class AuthManager : MonoBehaviour
{
    public InputField emailInput;
    public InputField passwordInput;
    public Text messageText;
    public string gameSceneName = "Game"; // Tên scene game sau khi đăng nhập thành công

    private string dataPath;
    private string userFile = "user_data.json";

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, userFile);
        Debug.Log("User data file path: " + dataPath);
    }

    public void RegisterUser()
    {
        StartCoroutine(RegisterCoroutine(emailInput.text, passwordInput.text));
    }

    private IEnumerator RegisterCoroutine(string email, string password)
    {
        yield return null; // Chạy ngay lập tức

        if (!IsValidEmail(email))
        {
            messageText.text = "Email không hợp lệ!";
            yield break;
        }

        if (!IsValidPassword(password))
        {
            messageText.text = "Mật khẩu phải có ít nhất 8 ký tự, chứa chữ hoa, chữ thường, số và ký tự đặc biệt!";
            yield break;
        }

        // Kiểm tra xem email đã tồn tại chưa
        if (File.Exists(dataPath))
        {
            string existingJson = File.ReadAllText(dataPath);
            if (!string.IsNullOrEmpty(existingJson))
            {
                try
                {
                    UserData existingUser = JsonUtility.FromJson<UserData>(existingJson);
                    if (existingUser != null && existingUser.email == email)
                    {
                        messageText.text = "Email này đã được đăng ký!";
                        yield break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("Lỗi đọc file JSON để kiểm tra email: " + ex.Message);
                }
            }
        }

        // Tạo salt ngẫu nhiên
        string salt = GenerateSalt();
        string passwordHash = HashPassword(password, salt);
        UserData newUser = new UserData { email = email, passwordHash = passwordHash, salt = salt };

        try
        {
            string json = JsonUtility.ToJson(newUser);
            File.WriteAllText(dataPath, json);
            messageText.text = "Đăng ký thành công!";
            Debug.Log("Đăng ký thành công!");
        }
        catch (Exception ex)
        {
            messageText.text = "Lỗi ghi file đăng ký!";
            Debug.LogWarning("Lỗi ghi file JSON: " + ex.Message);
        }
    }

    public void LoginUser()
    {
        StartCoroutine(LoginCoroutine(emailInput.text, passwordInput.text));
    }

    private IEnumerator LoginCoroutine(string email, string password)
    {
        yield return null; // Chạy ngay lập tức

        try
        {
            if (!File.Exists(dataPath))
            {
                messageText.text = "Tài khoản không tồn tại!";
                Debug.LogWarning("Không tìm thấy dữ liệu người dùng!");
                yield break;
            }

            string json = File.ReadAllText(dataPath);
            if (string.IsNullOrEmpty(json))
            {
                messageText.text = "Lỗi dữ liệu người dùng!";
                Debug.LogWarning("File JSON trống!");
                yield break;
            }

            UserData savedUser = JsonUtility.FromJson<UserData>(json);
            if (savedUser == null)
            {
                messageText.text = "Lỗi dữ liệu người dùng!";
                Debug.LogWarning("Dữ liệu JSON không hợp lệ!");
                yield break;
            }

            string hashedPasswordToCheck = HashPassword(password, savedUser.salt);
            if (savedUser.email == email && savedUser.passwordHash == hashedPasswordToCheck)
            {
                messageText.text = "Đăng nhập thành công!";
                Debug.Log("Đăng nhập thành công!");
                // Lưu trạng thái đăng nhập (ví dụ: sử dụng PlayerPrefs)
                PlayerPrefs.SetString("LoggedInUserEmail", email);
                PlayerPrefs.Save();
                // Chuyển đến scene game
                SceneManager.LoadScene(gameSceneName);
            }
            else
            {
                messageText.text = "Sai email hoặc mật khẩu!";
                Debug.LogWarning("Sai email hoặc mật khẩu!");
            }
        }
        catch (Exception ex)
        {
            messageText.text = "Lỗi khi đọc dữ liệu đăng nhập!";
            Debug.LogWarning("Lỗi khi đọc dữ liệu người dùng: " + ex.Message);
        }
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) return false;
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return false;
        return password.Length >= 8 &&
               Regex.IsMatch(password, ".*[A-Z].*") &&
               Regex.IsMatch(password, ".*[a-z].*") &&
               Regex.IsMatch(password, ".*[0-9].*") &&
               Regex.IsMatch(password, ".*[!@#$%^&*(),.?\"{}|<>].*");
    }

    // Hàm băm mật khẩu có sử dụng salt
    private string HashPassword(string password, string salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Kết hợp mật khẩu và salt
            string saltedPassword = password + salt;
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    // Hàm tạo salt ngẫu nhiên
    private string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    // Hàm kiểm tra trạng thái đăng nhập (có thể gọi ở các scene khác)
    public static bool IsUserLoggedIn()
    {
        return PlayerPrefs.HasKey("LoggedInUserEmail");
    }

    // Hàm lấy email của người dùng đã đăng nhập (có thể gọi ở các scene khác)
    public static string GetLoggedInUserEmail()
    {
        return PlayerPrefs.GetString("LoggedInUserEmail", "");
    }

    // Hàm đăng xuất
    public static void LogoutUser(string loginSceneName = "Login")
    {
        PlayerPrefs.DeleteKey("LoggedInUserEmail");
        PlayerPrefs.Save();
        SceneManager.LoadScene(loginSceneName);
    }
}