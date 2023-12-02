using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoreBunkerSceneController : MonoBehaviour
{
    int curIndex;
    public GameObject seagullAudio;
    public GameObject blackScreen;
    public GameObject titleText;
    // Start is called before the first frame update
    void Start()
    {
        curIndex = SceneManager.GetActiveScene().buildIndex;
        Invoke("EnableSeagullAudio", 3);
        Invoke("EnableBlackScreen", 7.5f);
        Invoke("LoadNext", 14);
    }

    void LoadNext()
    {
        SceneManager.LoadScene((curIndex + 1), LoadSceneMode.Single);
    }

    void EnableSeagullAudio ()
    {
        seagullAudio.SetActive(true);
    }
    void EnableBlackScreen()
    {
        blackScreen.SetActive(true);
        titleText.SetActive(true);
    }
}
