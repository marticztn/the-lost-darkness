using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventCheck : MonoBehaviour
{
    public AudioClip electric1;
    public AudioClip electric2;
    public AudioClip electric3;
    public AudioClip electric4;
    public AudioClip taskCompleted;
    public AudioSource audioSource;

    private int count;

    void Start()
    {
        count = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void check()
    {
        int clipChoice = Random.Range(0, 4);
        switch (clipChoice)
        {
            case 0:
                audioSource.PlayOneShot(electric1, 1.0f);
                break;

            case 1:
                audioSource.PlayOneShot(electric2, 1.0f);
                break;

            case 2:
                audioSource.PlayOneShot(electric3, 1.0f);
                break;

            case 3:
                audioSource.PlayOneShot(electric4, 1.0f);
                break;

            default:
                audioSource.PlayOneShot(electric1, 1.0f);
                break;
        }

        ++count;

        Debug.Log("count = " + count);

        if (count == 4)
        {
            StartCoroutine(taskFinished());
        }
    }

    IEnumerator taskFinished()
    {
        audioSource.PlayOneShot(taskCompleted, 0.7f);
        yield return new WaitWhile(() => audioSource.isPlaying);
        SceneManager.LoadScene("Level 1");
    }
}
