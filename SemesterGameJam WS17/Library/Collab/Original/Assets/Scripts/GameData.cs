﻿using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GameData
{
    #region Instanziierung
    private static GameData instance;

    private GameData()
    {
        if (instance != null)
            return;
        instance = this;
        for (int i = 0; i < 12; i += 1)
            activeLanes.Add(i);
    }

    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameData();
            }

            return instance;
        }
    }
    #endregion

    public float difficulty = 1;

    // bool to display the current status of the game (is paused or not)
    #region Pause
    private bool is_Paused = false;

    public bool Is_Paused
    {

        get
        {
            return is_Paused;
        }

        set
        {
            is_Paused = value;
        }

    }
    #endregion

    #region Score
    private int score = 0;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    #endregion

    public bool Invincible = false;
    public bool IsAlive = true;

    // orientation values between 0 and 3
    #region Orientation
    private int orientation = 0;

    public int Orientation
    {
        get
        {
            return orientation;
        }
        set
        {
            orientation = value;
        }
    }
    #endregion

    #region Settings
    // Volume of SFX and music
    #region Sound
    private float music_Volume = 0;
    public float Music_Volume
    {
        get
        {
            return music_Volume;
        }
        set
        {
            music_Volume = value;
        }
    }

    private float sfx_Volume = 0;
    public float SFX_Volume
    {
        get
        {
            return sfx_Volume;
        }
        set
        {
            sfx_Volume = value;
        }
    }
    #endregion
    #endregion

    public int inputType = 1;

    public List<int> activeLanes = new List<int>();

    public void Save_Settings()
    {
        BinaryFormatter bF = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameData.dat");

        SaveData_Settings data = new SaveData_Settings();
        data.music_volume = Music_Volume;
        data.sfx_volume = SFX_Volume;

        bF.Serialize(file, data);
        file.Close();
    }

    public void Load(String dataToLoad)
    {
        if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinaryFormatter bF = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
            SaveData_Settings data = (SaveData_Settings)bF.Deserialize(file);
            file.Close();

            if (dataToLoad == "settings")
            {
                Music_Volume = data.music_volume;
                SFX_Volume = data.sfx_volume;
            }
        }
    }


    private int laneWidth = 2;

    public Vector2 LaneAttraction(int lane, int orientation)
    {
        Vector2 attractionPoint = Vector2.zero;
        switch (orientation)
        {
            case 0:
                switch (lane)
                {
                    case -1:
                        attractionPoint = new Vector2(-laneWidth, -4);
                        break;

                    case 0:
                        attractionPoint = new Vector2(0, -4);
                        break;

                    case 1:
                        attractionPoint = new Vector2(laneWidth, -4);
                        break;

                    default:
                        break;
                }
                break;

            case 1:
                switch (lane)
                {
                    case -1:
                        attractionPoint = new Vector2(4, -laneWidth);
                        break;

                    case 0:
                        attractionPoint = new Vector2(4, 0);
                        break;

                    case 1:
                        attractionPoint = new Vector2(4, laneWidth);
                        break;

                    default:
                        break;
                }
                break;

            case 2:
                switch (lane)
                {
                    case -1:
                        attractionPoint = new Vector2(-laneWidth, 4);
                        break;

                    case 0:
                        attractionPoint = new Vector2(0, 4);
                        break;

                    case 1:
                        attractionPoint = new Vector2(laneWidth, 4);
                        break;

                    default:
                        break;
                }
                break;

            case 3:
                switch (lane)
                {
                    case -1:
                        attractionPoint = new Vector2(-4, -laneWidth);
                        break;

                    case 0:
                        attractionPoint = new Vector2(-4, 0);
                        break;

                    case 1:
                        attractionPoint = new Vector2(-4, laneWidth);
                        break;

                    default:
                        break;
                }
                break;

            default:
                break;
        }

        return attractionPoint;
    }
}

[Serializable]
class SaveData_Settings
{
    public float music_volume;
    public float sfx_volume;
}