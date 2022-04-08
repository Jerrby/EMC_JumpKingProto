using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour
{
    private const string SAVE_KEY = "PlayerData";
    private PlayerData _localPlayerData = new PlayerData() 
    {

    };
    [ContextMenu("Save")]
    public void Save()
    {
        var dataString = JsonUtility.ToJson(_localPlayerData);
        PlayerPrefs.SetString(SAVE_KEY, dataString);
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            //Load data
            var dataString = PlayerPrefs.GetString(SAVE_KEY);
            var playerData = JsonUtility.FromJson<PlayerData>(dataString);
            _localPlayerData = playerData;
        }
        else
        {
            //Create new data & save
            _localPlayerData = new PlayerData();
            Save();
        }

    }
}
