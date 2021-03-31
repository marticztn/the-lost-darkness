using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventCheck : MonoBehaviour
{
    private int count;

    void Start()
    {
        count = 0;
    }

    void Update()
    {
        
    }

    public void check()
    {
        ++count;

        Debug.Log("count = " + count);

        if (count == 4)
            SceneManager.LoadScene("Level 1");
    }
}
