using UnityEngine;
using UnityEngine.SceneManagement;
public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuScreen;
    //[SerializeField] private AudioClip MainMenuSound;

    //private void Awake()
    //{
    //    MainMenuScreen.SetActive(true);
    //}
    public void MainMenu()
    {
        MainMenuScreen.SetActive(true);
        //SoundManager.instance.PlaySound(MainMenuSound);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
