using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLvl : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject UICanvas;
    public GameObject renderCanvas;
    public GameObject virtualCamera;
    public GameObject renderCamera;
    public GameObject customPass;

    int curIndex;
    private void Start()
    {
        curIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(mainCamera);
            DontDestroyOnLoad(UICanvas);
            DontDestroyOnLoad(renderCamera);
            DontDestroyOnLoad(renderCanvas);
            DontDestroyOnLoad(virtualCamera);
            DontDestroyOnLoad(customPass);
            SceneManager.LoadScene((curIndex + 1), LoadSceneMode.Single);
        }
    }
}
