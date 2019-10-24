using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    #region "Public variables"
    #endregion

    #region "Private variables"
    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;

    //For starting camera animation
    private float transition = 0;
    private float animationDuration = 2f;
    private Vector3 animationOffset = new Vector3(0, 4, 0);
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = lookAt.position + startOffset;

        //Restrict camera movement on X-axis and Y-axis
        moveVector.x = 0;

        if (transition > 1f)
        {
            transform.position = moveVector;
        }
        else
        {
            //Enable camera animation at the start of the game
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
    }
}
