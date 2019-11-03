using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalSceneHandler : MonoBehaviour
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
            SceneManager.LoadScene(0);
        }
    }

    public void AnimalClicked(int animalIndex)
    {
        Debug.Log("Animal index: " + animalIndex);
        GameManager.instance.currentAnimal = animalIndex;

        SceneManager.LoadScene("EnvironmentSelectionScene");

    }
}
