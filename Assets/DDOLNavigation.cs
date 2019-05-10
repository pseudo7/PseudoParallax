using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOLNavigation : MonoBehaviour
{
    public static DDOLNavigation Instance;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (SceneManager.GetActiveScene().buildIndex == 0)
                Application.Quit();
            else SceneManager.LoadScene(0);
    }

    public void Anaglyph()
    {
        SceneManager.LoadScene(1);
    }

    public void MonoScopic3D()
    {
        SceneManager.LoadScene(2);
    }

    public void Trombone()
    {
        SceneManager.LoadScene(3);
    }
}
