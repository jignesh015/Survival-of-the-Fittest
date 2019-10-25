using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    #region "Public variables"
    public CharacterController characterController;
    #endregion

    #region "Private variables"
    private Vector3 moveVector;
    private float speed = 5f;
    private float verticalVelocity = 0;
    private float gravity = 9.8f;
    private float cameraAnimationDuration = 2f;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveVector = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Restrict player interaction during initial camera animation
        if (Time.time < cameraAnimationDuration)
        {
            characterController.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        //Set X position
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;

        //Set Y position
        verticalVelocity = characterController.isGrounded ? -0.5f : verticalVelocity - (gravity * Time.deltaTime);
        moveVector.y = verticalVelocity;

        //Set Z position
        moveVector.z = speed;

        characterController.Move(moveVector * Time.deltaTime);
    }
}
