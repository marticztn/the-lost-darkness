using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public TextMeshProUGUI batteryIndicator;
    public TextMeshProUGUI moodIndicator;
    public GameObject objectives;
    public GameObject shrines;

    public float battery = 100f;

    public int mood = 25;
    public int totalObjectives = 0;
    public int hazards = 0;
    public int orbs = 0;
    public int tasks = 0;

    public int isFirstRun = 1;

    public void saveGameData()
    {
        PlayerPrefs.SetFloat("playerX", transform.position.x);
        PlayerPrefs.SetFloat("playerY", transform.position.y);
        PlayerPrefs.SetFloat("playerZ", transform.position.z);
        PlayerPrefs.SetFloat("battery", battery);

        PlayerPrefs.SetInt("mood", mood);
        PlayerPrefs.SetInt("totalObjectives", totalObjectives);
        PlayerPrefs.SetInt("hazards", hazards);
        PlayerPrefs.SetInt("orbs", orbs);
        PlayerPrefs.SetInt("tasks", tasks);
        PlayerPrefs.SetInt("isFirstRun", isFirstRun);

        for (int i = 0; i < shrines.transform.childCount; ++i)
        {
            int isActive = shrines.transform.GetChild(i).GetChild(0).gameObject.activeSelf ? 1 : 0;
            PlayerPrefs.SetInt("Shrine " + (i + 1), isActive);
        }
    }

    public void loadGameData()
    {
        battery = (int)(float)getGameData("battery");
        mood = (int)getGameData("mood");
        totalObjectives = (int)getGameData("totalObjectives");
        hazards = (int)getGameData("hazards");
        orbs = (int)getGameData("orbs");
        tasks = (int)getGameData("tasks");
        isFirstRun = (int)getGameData("isFirstRun");

        gameObject.transform.position = new Vector3(
            (float)getGameData("playerX"),
            (float)getGameData("playerY"),
            (float)getGameData("playerZ")
            );

        batteryIndicator.text = "BATTERY: " + battery + "%";
        if (battery == 0)
            batteryIndicator.color = Color.red;

        moodIndicator.text = "MOOD: " + mood + " / 100";
        
        objectives.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "collect energy orbs: " + orbs + "/6";
        if (orbs == 6)
            objectives.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;

        objectives.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "fix electricity: " + tasks + "/1";
        if (tasks == 1)
            objectives.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.green;

        objectives.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "clean hazards: " + hazards + "/3";
        if (hazards == 3)
            objectives.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.green;

        for (int i = 0; i < shrines.transform.childCount; ++i)
        {
            shrines.transform.GetChild(i).GetChild(0).gameObject.SetActive(
                PlayerPrefs.GetInt("Shrine " + (i + 1)) == 1 ? true : false
                );
        }
    }

    public object getGameData(string dataName)
    {
        switch (dataName)
        {
            case "playerX":
                return PlayerPrefs.GetFloat("playerX");

            case "playerY":
                return PlayerPrefs.GetFloat("playerY");

            case "playerZ":
                return PlayerPrefs.GetFloat("playerZ");

            case "battery":
                return PlayerPrefs.GetFloat("battery");

            case "mood":
                return PlayerPrefs.GetInt("mood");

            case "totalObjectives":
                return PlayerPrefs.GetInt("totalObjectives");

            case "hazards":
                return PlayerPrefs.GetInt("hazards");

            case "orbs":
                return PlayerPrefs.GetInt("orbs");

            case "tasks":
                return PlayerPrefs.GetInt("tasks");

            case "isFirstRun":
                return PlayerPrefs.GetInt("isFirstRun");

            default:
                return null;
        }
    }
}
