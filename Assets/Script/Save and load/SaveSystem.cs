using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static SaveData _saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public PlayerSaveData playerData;
    }

    private static string SaveFilePath => Path.Combine(Application.persistentDataPath, "save.save");

    public static void Save()
    {
        if (GameManager.Instance.Player == null)
        {
            Debug.LogWarning("Không tìm thấy Player để lưu.");
            return;
        }

        HandleSaveData();
        string json = JsonUtility.ToJson(_saveData, true);
        File.WriteAllText(SaveFilePath, json);
        Debug.Log("Game đã được lưu tại: " + SaveFilePath);
    }

    public static void Load()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.LogWarning("Save file không tồn tại tại: " + SaveFilePath);
            return;
        }

        string json = File.ReadAllText(SaveFilePath);
        _saveData = JsonUtility.FromJson<SaveData>(json);
        HandleLoadData();
        Debug.Log("Đã load dữ liệu game.");
    }

    private static void HandleSaveData()
    {
        GameManager.Instance.Player.Save(ref _saveData.playerData);
    }

    private static void HandleLoadData()
    {
        GameManager.Instance.Player.Load(_saveData.playerData);
    }
}
