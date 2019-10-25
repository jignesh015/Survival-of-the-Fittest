using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesController : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;
    public GameObject[] avoidablePrefabs;
    public float interactableSpawnGap = 10f;
    public float collectibleSpawnProbability = 0.5f;

    #region "Private variables"
    //Player reference
    private Transform player;

    //List for storing pooled objects
    private List<GameObject> collectibleGameObjects;
    private List<GameObject> avoidableGameObjects;
    private List<GameObject> spawnedObjects;

    //Set pool objects limit
    private int collectiblePoolLimit = 4;
    private int avoidablePoolLimit = 4;

    //Horizontal positions for spawning interactables
    private float[] horizontalPositions;

    //Variables for spawning interactables
    private float nextCheckForPosZ = 20f;
    private float respawnOffset = 2f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Get player reference
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Set possible horizontal positions
        horizontalPositions = new float[] { -1, 0, 1 };

        //Create list to store pooled interactables
        collectibleGameObjects = new List<GameObject>();
        avoidableGameObjects = new List<GameObject>();
        spawnedObjects = new List<GameObject>();

        //Instantiate all collectibles to use later on
        foreach (GameObject _collectible in collectiblePrefabs)
        {
            for (int i = 0; i  < collectiblePoolLimit; i++)
            {
                GameObject _collectibleObj = Instantiate(_collectible);
                _collectibleObj.transform.parent = transform;
                _collectibleObj.SetActive(false);
                collectibleGameObjects.Add(_collectibleObj);
            }
            
        }

        //Instantiate all avoidables to use later on
        foreach (GameObject _avoidable in avoidablePrefabs)
        {
            for (int i = 0; i < avoidablePoolLimit; i++)
            {
                GameObject _avoidableObj = Instantiate(_avoidable);
                _avoidableObj.transform.parent = transform;
                _avoidableObj.SetActive(false);
                avoidableGameObjects.Add(_avoidableObj);
            }
        }

        //Spawning 4 rows of interactable initially
        for (int i = 1; i <= 4; i++)
        {
            float spawnPositonZ = (i * interactableSpawnGap) + interactableSpawnGap;
            PlaceInteractableRandomly(spawnPositonZ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Place the interactables randomly ahead of player
        float playerPosZ = player.position.z;
        if (playerPosZ >= nextCheckForPosZ + respawnOffset)
        {
            //De-spawn the out of screen interactable
            GameObject _oldSpawnedObj = spawnedObjects[0];
            _oldSpawnedObj.SetActive(false);
            spawnedObjects.RemoveAt(0);

            //Re-spawn at new position
            if (spawnedObjects.FindIndex(s => s.transform.position.z == (nextCheckForPosZ + interactableSpawnGap*4)) == -1)
                PlaceInteractableRandomly(nextCheckForPosZ + interactableSpawnGap*4);

            //Increment nextCheckForPosZ
            nextCheckForPosZ += interactableSpawnGap;
        }
    }

    public void PlaceInteractableRandomly(float spawnPostionZ = 0f)
    {
        //Randomly decide whether to spawn collectible or avoidable
        float collectibleOrAvoidableRandom = Random.value;
        int interactableDecider = collectibleOrAvoidableRandom <= collectibleSpawnProbability ? 0 : 1;

        //Randomly decide the spawnPositionX for spawned object
        float spawnPositionX = horizontalPositions[1];
        float horizontalSpawnRandom = Random.value;
        if (horizontalSpawnRandom <= 0.33f)
        {
            spawnPositionX = horizontalPositions[0];
        }
        else if (horizontalSpawnRandom >= 0.66f)
        {
            spawnPositionX = horizontalPositions[2];
        }

        //Spawn the interactable based on interactableDecider
        List<GameObject> interactablesToSpawn = interactableDecider == 0 ? collectibleGameObjects : avoidableGameObjects;
        GameObject spawnedInteractable = interactablesToSpawn.Find(i => !i.activeSelf);
        spawnedInteractable.SetActive(true);

        //Set spawned object position as per spawnPostionZ and randomly generated spawnPositionX
        spawnedInteractable.transform.position = new Vector3(spawnPositionX, spawnedInteractable.transform.position.y, spawnPostionZ);

        //Add spawned objects to a list for future reference
        spawnedObjects.Add(spawnedInteractable);
    }
}
