using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvSceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }
    
    public void EnvironmentClicked(int envIndex)
    {
        Debug.Log("Environment index: " + envIndex);
        GameManager.instance.currentEnvironment = envIndex;

        SceneManager.LoadSceneAsync("GameScene");
    }
}
