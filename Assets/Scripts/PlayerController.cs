using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region "Public Materials"
    public Rigidbody playerRigidBody;
    public Slider healthSlider;

    //Material for player
    public Material playerMat;
    public Material playerGreenMat;
    public Material playerRedMat;

    //Instruction variables
    public bool pauseForMovementInstructions = false;
    #endregion

    #region "Private variables"
    private float speed = 6f;
    private float verticalVelocity = 0;
    private float horizontalVelocity = 0;
    public float cameraAnimationDuration = 2f;
    private float gravity = 9.8f;

    private int laneNum = 2;
    private bool controlLocked = false;

    //Variables for handling player health
    private bool isDead = false;
    private float playerHealth = 0;

    private GameSceneHandler gameSceneHandler;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Get player rigid body reference
        playerRigidBody = GetComponent<Rigidbody>();

        //Get health slider reference
        healthSlider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();

        //Hide healthslider at the start
        healthSlider.gameObject.SetActive(false);

        //Get reference of gameSceneHandler
        gameSceneHandler = FindObjectOfType<GameSceneHandler>();

        //Set starting health for player
        playerHealth = 1f;

        //Get player defualt mat
        playerMat = GetComponentInChildren<SkinnedMeshRenderer>().material;

        //Reduce player health by a small factor every few seconds
        InvokeRepeating("ReducePlayerHealth", 5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseForMovementInstructions)
        {
            playerRigidBody.velocity = new Vector3(0, 0, 0);
            return;
        }

        //Do not run if dead
        if (isDead)
        {
            playerRigidBody.velocity = new Vector3(0, 0, 0);
            GetComponent<Animator>().SetTrigger("dead");

            StartCoroutine(gameSceneHandler.ShowGameOverScreen());
            return;
        }

        //Restrict player interaction during initial camera animation
        if (Time.time < cameraAnimationDuration)
        {
            playerRigidBody.velocity = new Vector3(0, 0, speed);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if ((Input.mousePosition.x <= Screen.width / 2) && laneNum > 1 && !controlLocked)
            {
                //move left
                horizontalVelocity = -2.5f;
                laneNum -= 1;
                controlLocked = true;
                StartCoroutine(StopSlide());
            }
            else if ((Input.mousePosition.x > Screen.width / 2) && laneNum < 3 && !controlLocked)
            {
                //move right
                horizontalVelocity = 2.5f;
                laneNum += 1;
                controlLocked = true;
                StartCoroutine(StopSlide());
            }
        }

        //Make player run
        playerRigidBody.velocity = new Vector3(horizontalVelocity, 0, speed);

        //update health slider
        healthSlider.value = playerHealth <= 0.05f ? 0 : playerHealth;

        //If health gets too low, player is dead
        isDead = playerHealth <= 0.05f ? true : false;
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
        if (other.gameObject.tag == "Collectible")
        {
            //Write logic for collectible interaction
            other.gameObject.SetActive(false);
            playerHealth = playerHealth >= 0.95f ? 1f : playerHealth + 0.1f;
        }

        if (other.gameObject.tag == "Avoidable")
        {
            //Write logic for avoidable interaction
            other.gameObject.SetActive(false);
            playerHealth -= 0.3f;
            StartCoroutine(ChangePlayerMat(false));
        }
    }

    IEnumerator ChangePlayerMat(bool isCollectible)
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = isCollectible ? playerGreenMat : playerRedMat;

        yield return new WaitForSeconds(0.1f);

        GetComponentInChildren<SkinnedMeshRenderer>().material = playerMat;

        yield return new WaitForSeconds(0.1f);

        GetComponentInChildren<SkinnedMeshRenderer>().material = isCollectible ? playerGreenMat : playerRedMat;

        yield return new WaitForSeconds(0.1f);

        GetComponentInChildren<SkinnedMeshRenderer>().material = playerMat;

    }


    //Reduce player health by a small factor
    private void ReducePlayerHealth()
    {
        if (pauseForMovementInstructions)
            return;

        playerHealth -= 0.05f;
    }

}
