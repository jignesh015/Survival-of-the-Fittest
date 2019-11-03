using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneHandler : MonoBehaviour
{
    public GameObject bear;
    public GameObject lion;
    public GameObject polarbear;

    public GameObject movementInstruction;
    public GameObject interactableInstruction;
    public GameObject gameOverScreen;


    private PlayerController playerController;

    private void Awake()
    {
        movementInstruction.SetActive(false);
        interactableInstruction.SetActive(false);
        gameOverScreen.SetActive(false);

        Debug.Log("Current animal: " + GameManager.instance.currentAnimal);

        switch (GameManager.instance.currentAnimal)
        {
            case 0:
                bear.SetActive(true);
                bear.GetComponent<Animator>().SetTrigger("run");
                break;
            case 1:
                lion.SetActive(true);
                lion.GetComponent<Animator>().SetTrigger("run");
                break;
            case 2:
                polarbear.SetActive(true);
                polarbear.GetComponent<Animator>().SetTrigger("run");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        Invoke("ShowMovementInstruction", playerController.cameraAnimationDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }
    }

    private void ShowMovementInstruction()
    {
        playerController.pauseForMovementInstructions = true;
        movementInstruction.SetActive(true);
    }

    public void HideInteractableInstruction()
    {
        playerController.pauseForMovementInstructions = false;
        playerController.healthSlider.gameObject.SetActive(true);

    }

    public IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(2.5f);

        playerController.healthSlider.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void ContinueAfterGameOver()
    {
        SceneManager.LoadScene(0);
    }

}
