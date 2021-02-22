using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public string[] sceneArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneArray[sceneNumber]);
    }
    public void ToExitGame()
    {
        Application.Quit();
    }
}
