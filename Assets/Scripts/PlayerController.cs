using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidBody;

    #region "Private variables"
    private float speed = 6f;
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
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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

        playerRigidBody.velocity = new Vector3(horizontalVelocity, 0, speed);
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
        }

        if (other.gameObject.tag == "Avoidable")
        {
            //Write logic for avoidable interaction
            other.gameObject.SetActive(false);
        }
    }
}
