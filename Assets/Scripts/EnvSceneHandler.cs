using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnvironmentClicked(int envIndex)
    {
        GameManager.instance.EnvironmentClicked(envIndex);
    }
}
