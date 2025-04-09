using UnityEngine;

public interface IPlayerSavable
{
    void Save(ref PlayerSaveData data);
    void Load(PlayerSaveData data);
}

[System.Serializable]
public struct PlayerSaveData
{
    public Vector3 Position;
}
