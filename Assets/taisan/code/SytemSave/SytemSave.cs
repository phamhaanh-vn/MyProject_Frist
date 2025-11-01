using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SytemSave 
{
    private static SaveData _saveData = new SaveData();
    [System.Serializable]
    public class SaveData
    {
        public PlayerSaveData PlayerData;
        public FruitsSaveData FruitsSave = new FruitsSaveData();
    }
    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }
    public static void Save()
    {
        HandleSaveData();
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }
    public static void HandleSaveData()
    {
        Moveplayer.Mo.Save(ref _saveData.PlayerData);
        foreach (var fruit in GameObject.FindObjectsOfType<SaveFruits>())
        {
            fruit.Save(ref _saveData.FruitsSave);
        }
    }
    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());
        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }
    public static void HandleLoadData()
    {
        Moveplayer.Mo.Load(_saveData.PlayerData);
        foreach (var fruit in GameObject.FindObjectsOfType<SaveFruits>())
        {
            fruit.Load(_saveData.FruitsSave);
        }
    }
    public static void MarkFruitCollected(string fruitId, bool collected)
    {
        // update dictionary
        _saveData.FruitsSave.CollectionFruits[fruitId] = collected;

        // ghi lại file
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

}
