using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneHandler : MonoBehaviour
{
    public GameObject bear;
    public GameObject lion;
    public GameObject polarbear;

    private void Awake()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
