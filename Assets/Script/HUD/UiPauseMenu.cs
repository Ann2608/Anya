using UnityEngine;
using UnityEngine.SceneManagement;
public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    //[SerializeField] private AudioClip GameOverSound;

    private void Awake()
    {
        GameOverScreen.SetActive(false);
    }
    public void GameOVer()
    {
        GameOverScreen.SetActive(true);
        //SoundManager.instance.PlaySound(GameOverSound);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //Load lại scene hiện tại
    }
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
