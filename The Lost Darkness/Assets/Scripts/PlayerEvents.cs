using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerEvents : MonoBehaviour
{
    public TextMeshProUGUI OrbObjText;
    public TextMeshProUGUI HazardObjText;
    public TextMeshProUGUI MoodIndicator;
    private TextMeshProUGUI batteryText;

    public GameObject flashLightObject;
    public GameObject batteryIndicatorObject;

    private Light flashLight;

    private float batteryDrainSpeed = 2f;
    private float flashLightFadeSpeed = 50f;
    private float flashLightMaxBrightness = 12f;

    private PlayerData playerData;

    private void Start()
    {
        playerData = gameObject.GetComponent<PlayerData>();
        if ((int)playerData.getGameData("isFirstRun") == 0)
        {
            playerData.loadGameData();
        }

        /*------------- Unity -> Edit -> Clear All PlayerPrefs ------------*/
        /*------------------------------ THEN -----------------------------*/
        /*-------------- Uncomment these to reset game state --------------*/
        /*gameObject.transform.position = new Vector3(-18f, 9f, -0.1f);
        playerData.mood = 25;
        playerData.battery = 100f;
        playerData.batteryIndicator.color = Color.white;
        for (int i = 0; i < playerData.shrines.transform.childCount; ++i)
            playerData.shrines.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);*/
        /*-----------------------------------------------------------------*/

        playerData.isFirstRun = 0;
        playerData.saveGameData();

        batteryText = batteryIndicatorObject.GetComponent<TextMeshProUGUI>();
        batteryText.text = "BATTERY: " + (int)playerData.battery + "%";

        flashLight = flashLightObject.GetComponent<Light>();
        flashLight.intensity = 0f;
    }

    private void Update()
    {
        /*--------------- Flash light control ---------------*/
        if (Input.GetKey(KeyCode.Space))
        {
            if ((int)playerData.battery != 0)
            {
                if (flashLight.intensity <= flashLightMaxBrightness)
                    flashLight.intensity += Time.deltaTime * flashLightFadeSpeed;

                StartCoroutine(wait(1f));
                playerData.battery -= Time.deltaTime * batteryDrainSpeed;
                batteryText.text = "BATTERY: " + (int)playerData.battery + "%";
            }

            else
            {
                if (flashLight.intensity >= 0)
                    flashLight.intensity -= Time.deltaTime * flashLightFadeSpeed;

                batteryText.color = Color.red;
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            playerData.saveGameData();
        }

        else
        {
            if (flashLight.intensity >= 0)
                flashLight.intensity -= Time.deltaTime * flashLightFadeSpeed;
        }
        /*---------------------------------------------------*/

        /*---------------------- Objective check ----------------------*/
        if (playerData.totalObjectives == 3)
        {
            // Current level completed

        }
        /*-------------------------------------------------------------*/
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Spike"))
        {
            gameObject.transform.position = new Vector3(-18f, 9f, -0.2f);
            playerData.saveGameData();
            SceneManager.LoadScene("Level 1");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "Orb":
                other.gameObject.SetActive(false);

                playerData.orbs++;
                OrbObjText.text = "collect energy orbs: " + playerData.orbs.ToString() + "/6";

                if (playerData.orbs == 6)
                {
                    playerData.totalObjectives++;
                    playerData.mood += 25;

                    OrbObjText.color = Color.green;
                    MoodIndicator.text = "mood: " + playerData.mood.ToString() + " / 100";
                }

                playerData.saveGameData();
                break;

            case "Wire Task":
                playerData.saveGameData();
                PlayerPrefs.SetFloat("playerX", transform.position.x + 2f);
                SceneManager.LoadScene("Wire Task");
                break;
        }
    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(3f);
    }
}
