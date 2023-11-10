using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject RoadPrefab;
    [SerializeField] private GameObject WallPrefab;
    [SerializeField] private GameObject DWallPrefab;
    [SerializeField] private List<WallScriptable> wallPropertyList = new List<WallScriptable>();
    [SerializeField] private float lastRoadPosX;
    [SerializeField] private float firstRoadPosX;
    [SerializeField] private float DistanceBetweenWalls;
    public int repeatCount = 10;
    //[SerializeField] private float wallSpawnCd;
    //[SerializeField] private float wallSpawnCdMaxDefault;
    //[SerializeField] private float wallSpawnCdMultiplier;
    private void Awake()
    {
        lastRoadPosX = firstRoadPosX;
    }

    private void Start()
    {
        spawnRoad(repeatCount);
    }

    void Update()
    {


        /*
        wallSpawnCd -= 1 * Time.deltaTime;
        if(wallSpawnCd < 0)
        {
            wallSpawnCd = wallSpawnCdMaxDefault * wallSpawnCdMultiplier;
        }*/
    }

    public void spawnRoad(int repeatCount)
    {
        List<GameObject> blockList = new List<GameObject>();
        lastRoadPosX += 1000;
        Instantiate(RoadPrefab,new Vector3(lastRoadPosX, 0, 0),Quaternion.identity);

        for (int i = 0; i < repeatCount; i++)
        {
            float wallPosZRandom = Random.Range(-13.5f, 13.5f);
            float wallPosXRandom = Random.Range(lastRoadPosX - 500, lastRoadPosX + 500);

            foreach (GameObject bloc in blockList)
            {
                float distance = Vector3.Distance(bloc.transform.position, new Vector3(wallPosXRandom,0,wallPosZRandom));

                wallPosZRandom = Random.Range(-13.5f, 13.5f);
                wallPosXRandom = Random.Range(lastRoadPosX - 500, lastRoadPosX + 500);
                distance = Vector3.Distance(bloc.transform.position, new Vector3(wallPosXRandom, 0, wallPosZRandom));

                /*
                while (Mathf.Abs(distance) < DistanceBetweenWalls)
                {

                }
                */
            }

            int RandomChoose = Random.Range(0, 3);

            if(RandomChoose == 0 || RandomChoose == 1)
            {
                GameObject block = Instantiate(WallPrefab);
                int randomWallNo = Random.Range(0, wallPropertyList.Count);
                block.GetComponent<WallScript>().WallScr = wallPropertyList[randomWallNo];
                block.transform.position = new Vector3(wallPosXRandom, 0, wallPosZRandom);
                blockList.Add(block);
            }
            else
            {
                GameObject block = Instantiate(DWallPrefab);
                block.transform.position = new Vector3(wallPosXRandom, 0, wallPosZRandom);
                blockList.Add(block);
            }

        }
    }
}
