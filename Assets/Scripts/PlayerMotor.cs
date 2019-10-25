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
    private float horizontalVelocity = 0;
    private float cameraAnimationDuration = 2f;
    private float gravity = 9.8f;

    private int laneNum = 2;
    private bool controlLocked = false;
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
        //moveVector = new Vector3(moveVector.x, 0, 0);

        ////Set X position
        ////moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetMouseButton(0))
        {
            if ((Input.mousePosition.x <= Screen.width / 2) && laneNum > 1 && !controlLocked)
            {
                //move left
                horizontalVelocity = -2;
                laneNum -= 1;
                controlLocked = true;

                characterController.SimpleMove(new Vector3(-2, 0, speed));

                StartCoroutine(StopSlide());
            }
            else if((Input.mousePosition.x > Screen.width/2) && laneNum < 3 && !controlLocked)
            {
                //move right
                horizontalVelocity = 2;
                laneNum += 1;
                controlLocked = true;
                characterController.SimpleMove(new Vector3(2, 0, speed));
                StartCoroutine(StopSlide());
            }
        }

        //Set Y position
        verticalVelocity = characterController.isGrounded ? -0.5f : verticalVelocity - (gravity * Time.deltaTime);


        moveVector.x = 0;
        moveVector.y = verticalVelocity;

        //Set Z position
        moveVector.z = speed * Time.deltaTime;

        characterController.Move(moveVector );

    }

    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(0.5f);
        horizontalVelocity = 0;
        controlLocked = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Collectible" )
        {
            //Write logic for collectible interaction
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Avoidable")
        {
            //Write logic for avoidable interaction
            other.gameObject.SetActive(false);
        }
    }
}
