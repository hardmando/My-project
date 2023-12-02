using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    int curIndex;
    void Start()
    {
        curIndex = SceneManager.GetActiveScene().buildIndex;
        Invoke("LoadNext", 2f);

    }

    void LoadNext()
    {
        SceneManager.LoadScene((curIndex + 1), LoadSceneMode.Single);
    }
}
