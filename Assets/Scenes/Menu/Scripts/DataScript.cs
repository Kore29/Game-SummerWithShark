using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int night;
    public bool debug;
}

public class DataScript : MonoBehaviour
{
    public static void SavePlayerData(PlayerData data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "player.dat");
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, jsonData);
    }

    public static PlayerData LoadPlayerData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "player.dat");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            // Si el archivo no existe, crea uno nuevo con valores por defecto.
            PlayerData defaultData = new PlayerData
            {
                night = 1,
                debug = false,
            };
            SavePlayerData(defaultData);
            return defaultData;
        }
    }
    public static void DeletePlayerData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "player.dat");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            LoadPlayerData();
        }
    }
}
