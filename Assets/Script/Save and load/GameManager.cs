using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public IPlayerSavable Player { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            IPlayerSavable foundPlayer = FindObjectOfType<AnyaMv>();
            if (foundPlayer != null)
            {
                Player = foundPlayer;
                Debug.Log("Player đã được gán: " + Player);
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Player trong scene.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) // Save
        {
            SaveSystem.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Load
        {
            SaveSystem.Load();
        }
    }
}
