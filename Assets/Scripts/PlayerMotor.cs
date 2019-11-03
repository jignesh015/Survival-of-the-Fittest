using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour
{
    #region "Public variables"
    public CharacterController characterController;
    public Slider healthSlider;
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

    //Variables for handling player health
    private bool isDead = false;
    private float playerHealth = 0;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        healthSlider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        moveVector = Vector3.zero;

        //Set starting health for player
        playerHealth = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        //Restrict player interaction during initial camera animation
        if (Time.time < cameraAnimationDuration)
        {
            characterController.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        ////Set X position
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

        characterController.Move(moveVector);

        healthSlider.value = playerHealth;
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;

        //Reduce player health by a small factor
        //playerHealth -= 0.005f;
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
            playerHealth += 0.5f;
        }

        if (other.gameObject.tag == "Avoidable")
        {
            //Write logic for avoidable interaction
            other.gameObject.SetActive(false);
            playerHealth -= 0.2f;
        }
    }
}
