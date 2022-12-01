using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //managerde bulunacak

    public bool inTransfer;
    public bool dropTransfer;

    public int money;
    public int researchPoint;
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
    }

    public void SetResearchPoint()
    {
        PlayerPrefs.SetInt("researchPoint", researchPoint);
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
