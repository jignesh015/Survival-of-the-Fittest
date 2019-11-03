using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    //Variables for storing all environment prefabs
    [Header("Forest Prefabs")]
    public GameObject forestStartingPlanePrefab;
    public GameObject[] forestGroundPrefabs;

    [Header("Savannah Prefabs")]
    public GameObject savannahStartingPlanePrefab;
    public GameObject[] savannahGroundPrefabs;

    [Header("Polar Prefabs")]
    public GameObject polarStartingPlanePrefab;
    public GameObject[] polarGroundPrefabs;

    private GameObject startingPlanePrefab;
    private GameObject[] groundPrefabs;
    public float groundLength = 100f;

    private List<GameObject> groundGameObjects;
    private List<GameObject> spawnedGroundObjects;

    private int groundPoolLimit = 2;
    private float nextCheckForPosZ = 100f;
    private float respawnOffset = 20f;
    private Transform player;

    private void Awake()
    {
        switch (GameManager.instance.currentEnvironment)
        {
            case 0:
                startingPlanePrefab = forestStartingPlanePrefab;
                groundPrefabs = forestGroundPrefabs;
                break;
            case 1:
                startingPlanePrefab = savannahStartingPlanePrefab;
                groundPrefabs = savannahGroundPrefabs;
                break;
            case 2:
                startingPlanePrefab = polarStartingPlanePrefab;
                groundPrefabs = polarGroundPrefabs;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Reference player transform
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Initialize list for storing pooled objects
        groundGameObjects = new List<GameObject>();
        spawnedGroundObjects = new List<GameObject>();

        //Instantiate starting plane
        GameObject startingPlane = Instantiate(startingPlanePrefab);
        startingPlane.transform.parent = transform;

        //Instantiate ground prefabs at the beginning
        foreach (GameObject _ground in groundPrefabs)
        {
            for (int i = 0; i < groundPoolLimit; i++)
            {
                GameObject _groundObj = Instantiate(_ground);
                _groundObj.SetActive(false);
                _groundObj.transform.parent = transform;
                groundGameObjects.Add(_groundObj);
            }
        }

        //Spawn ground initially
        for (int i = 0; i < groundPoolLimit; i++)
        {
            SpawnGround(i * groundLength);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float playerPosZ = player.position.z;
        if (playerPosZ >= nextCheckForPosZ + respawnOffset)
        {
            //De-spawn out of screen ground
            GameObject _oldSpawnedGround = spawnedGroundObjects[0];
            _oldSpawnedGround.SetActive(false);
            spawnedGroundObjects.RemoveAt(0);

            //Re-spawn at new position
            SpawnGround(nextCheckForPosZ + groundLength);

            //Increment nextCheckForPosZ
            nextCheckForPosZ += groundLength;
        }
    }

    public void SpawnGround(float spawnPosZ = -1f)
    {
        GameObject _groundObj = groundGameObjects.Find(g => !g.activeSelf);
        _groundObj.SetActive(true);
        Vector3 _groundObjPos = _groundObj.transform.position;
        _groundObj.transform.position = new Vector3(_groundObjPos.x, _groundObjPos.y, spawnPosZ);
        spawnedGroundObjects.Add(_groundObj);
    }
}
