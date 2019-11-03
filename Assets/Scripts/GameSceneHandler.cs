using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneHandler : MonoBehaviour
{
    public GameObject bear;
    public GameObject lion;
    public GameObject polarbear;

    public GameObject movementInstruction;
    public GameObject interactableInstruction;


    private GameObject player;

    private void Awake()
    {
        movementInstruction.SetActive(false);
        interactableInstruction.SetActive(false);

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
        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("ShowMovementInstruction", 2.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowMovementInstruction()
    {
        player.GetComponent<PlayerController>().pauseForMovementInstructions = true;
        movementInstruction.SetActive(true);
    }

    public void HideMovementInstruction()
    {
        movementInstruction.SetActive(false);
        interactableInstruction.SetActive(true);
    }

    public void HideInteractableInstruction()
    {
        player.GetComponent<PlayerController>().pauseForMovementInstructions = false;
        //movementInstruction.SetActive(false);
        //interactableInstruction.SetActive(true);
    }


}
