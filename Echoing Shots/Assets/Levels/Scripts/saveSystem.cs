using System;
using UnityEngine;

[System.Serializable]//as i was researching how to save found this and this allows for jsonUtility.toJson to save data //easyer and faster this way to
public class saveSystem
{
    //player data
    //position
    public Vector3 playerPosition;//for x,y,z
    //health
    public int playerHealth;
    //coins
    public int playerCoins;

    //game state data

    //game item count
    public int gameItemCount;
    //wave count
    public int waveCount;
    //enemy data
    public string SceneName;

    //Constructor to initialize arrays
    public saveSystem(Vector2 pos, int hp, int coinCount, int ItemCount, int wave, string scene)
    {
        //initialize array
        playerPosition = pos;
        playerHealth = hp;
        playerCoins = coinCount;
        gameItemCount = ItemCount;
        waveCount = wave;
        SceneName = scene;
    }

}
