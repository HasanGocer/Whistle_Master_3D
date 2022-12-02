using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //managerde bulunacak

    public int money;
    public int vibration;
    public int sound;
    public int level;

    public void Awake()
    {
        PlayerPrefsPlacement();
    }

    private void PlayerPrefsPlacement()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
        }
        else
        {
            PlayerPrefs.SetInt("money", 100);
        }

        if (PlayerPrefs.HasKey("vibration"))
        {
            vibration = PlayerPrefs.GetInt("vibration");
        }
        else
        {
            PlayerPrefs.SetInt("vibration", 1);
        }

        if (PlayerPrefs.HasKey("sound"))
        {
            sound = PlayerPrefs.GetInt("sound");
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
        }

        if (PlayerPrefs.HasKey("XP"))
        {
            LevelSystem.Instance.XP = PlayerPrefs.GetInt("XP");
        }
        else
        {
            PlayerPrefs.SetInt("XP", LevelSystem.Instance.XP);
        }

        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", level);
        }

        if (!PlayerPrefs.HasKey("first"))
        {
            FactorPlacementWrite(ItemData.Instance.factor);
            PlayerPrefs.SetInt("first", 1);
        }
        ItemData.Instance.factor = FactorPlacementRead();
        ItemData.Instance.IDAwake();
    }

    public void FactorPlacementWrite(ItemData.Field factor)
    {
        string jsonData = JsonUtility.ToJson(factor);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/FactorData.json", jsonData);
    }

    public ItemData.Field FactorPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/FactorData.json");
        ItemData.Field factor = new ItemData.Field();
        factor = JsonUtility.FromJson<ItemData.Field>(jsonRead);
        return factor;
    }

    public void SetMoney()
    {
        PlayerPrefs.SetInt("money", money);
    }

    public void SetLevel()
    {
        PlayerPrefs.SetInt("level", level);
    }

    public void SetXP()
    {
        PlayerPrefs.SetInt("XP", LevelSystem.Instance.XP);
    }

    public void SetSound()
    {
        PlayerPrefs.SetInt("sound", sound);
    }

    public void SetVibration()
    {
        PlayerPrefs.SetInt("vibration", vibration);
    }
}
