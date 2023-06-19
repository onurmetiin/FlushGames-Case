using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //TODO:PhoneId de al.
    public static DataManager Instance { get; private set; }
    public static PlayerData Data { get; set; }
    public static string savePath;
    private void Awake()
    {

        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log(savePath);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadPlayerData();
    }

    public static void SavePlayerData()
    {
        try
        {
            Data.HashContent = GetHashContent(Data);
            //Write File
            File.WriteAllText(savePath, JsonConvert.SerializeObject(Data, Formatting.Indented));
        }
        catch (Exception ex)
        {
            Debug.LogError("PlayerData dosya yoluna kaydedilemedi." + ex.Message);
        }
    }


    public static void LoadPlayerData()
    {
        string stringJson = "";
        if (File.Exists(savePath))
        {
            stringJson = File.ReadAllText(savePath);
        }
        if (string.IsNullOrEmpty(stringJson))
        {
            Debug.Log("Save bilgisi yok.Default data oluşturuluyor.");
            CreateDefaultPlayerData();
            SavePlayerData();
            return;
        }
        try
        {
            Data = JsonConvert.DeserializeObject<PlayerData>(stringJson);
        }
        catch (Exception ex)
        {
            Debug.LogError("Save dosyası okunurken hata oluştu.Playerdata formatına uygun değil.Error Message: " + ex.Message);
            CreateDefaultPlayerData();
            SavePlayerData();
            return;
        }
        if (Data.HashContent != GetHashContent(Data))
        {
            Debug.LogError("Save dosyası değiştirilmiş kayıtlar sıfırlanıyor.");
            CreateDefaultPlayerData();
            SavePlayerData();
        }

    }

    private static string GetHashContent(PlayerData data)
    {
        //Save content in JSON as string
        string dataJson = JsonConvert.SerializeObject(data, Formatting.Indented);

        PlayerData tempData = JsonConvert.DeserializeObject<PlayerData>(dataJson);
        tempData.HashContent = "";
        dataJson = JsonConvert.SerializeObject(tempData, Formatting.Indented);

        //Setup SHA
        SHA256Managed crypt = new SHA256Managed();

        string hash = String.Empty;
        //Compute Hash
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(dataJson), 0, Encoding.UTF8.GetByteCount(dataJson));

        //Convert to Hex
        foreach (byte bit in crypto)
        {
            hash += bit.ToString("x2");
        }
        return hash;
    }

    private static void CreateDefaultPlayerData()
    {
        Data = new PlayerData
        {
            // BURASI EKLENİLEN VERİLERE DEĞER VERİLECEK YER...
            Money = 0,
            GemStatistics = new Dictionary<string, int>()
        };
    }

    public static void IncreaseCollectedGem(string gemType,int amount=1,bool saveData=true)
    {
        if (!Data.GemStatistics.ContainsKey(gemType))
        {
            Data.GemStatistics.Add(gemType, 0);
        }

        Data.GemStatistics[gemType] += amount;

        if (saveData) SavePlayerData();
    }

    public static void EarningMoney(float amount)
    {
        Data.Money += amount;
        SavePlayerData();
    }
}