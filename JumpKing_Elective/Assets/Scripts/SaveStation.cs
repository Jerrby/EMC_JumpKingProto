using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour
{
    public GameObject player;
    public float posX;
    public float posY;

    private const string SAVE_KEY = "PlayerData";
    private void Start()
    {   
        posX = player.transform.position.x;
        posY = player.transform.position.y;
    }
    private PlayerData _localPlayerData = new PlayerData()
    {
        positionX = posX,
        positionY = posY,
    };
    [ContextMenu("Save")]
    public void Save()
    {
        var dataString = JsonUtility.ToJson(_localPlayerData);
        Debug.Log(dataString);
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
            Debug.Log(dataString);
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
