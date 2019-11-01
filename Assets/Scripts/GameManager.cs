using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentAnimal = 0;
    public int currentEnvironment = 0;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimalClicked(int animalIndex)
    {
        Debug.Log("Animal index: " + animalIndex);
        currentAnimal = animalIndex;
        Destroy(SwipeMenu.Menu.instance.gameObject);

        SceneManager.LoadScene("EnvironmentSelectionScene");

    }

    public void EnvironmentClicked(int envIndex)
    {
        Debug.Log("Environment index: " + envIndex);
        currentEnvironment = envIndex;

        SceneManager.LoadSceneAsync("GameScene");
    }
}
