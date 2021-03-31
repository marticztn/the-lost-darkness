using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int battery;
    public int mood;
    public float[] position;

    public PlayerData(PlayerMovement playerMovement)
    {
        position = new float[3];
        position[0] = playerMovement.transform.position.x;
        position[1] = playerMovement.transform.position.y;
        position[2] = playerMovement.transform.position.z;
    }
}
