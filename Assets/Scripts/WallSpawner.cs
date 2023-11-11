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
    [SerializeField] private float distanceBetweenWalls;

    [SerializeField] private float wallHeight = 15;
    [SerializeField] private float wallWidth = 1000;

    public int repeatCount = 10;


    private void Awake()
    {
        lastRoadPosX = firstRoadPosX;
    }

    private void Start()
    {
        spawnRoad(repeatCount);
<<<<<<< Updated upstream
    }

    void Update()
    {


        /*
        wallSpawnCd -= 1 * Time.deltaTime;
        if(wallSpawnCd < 0)
        {
            wallSpawnCd = wallSpawnCdMaxDefault * wallSpawnCdMultiplier;
        }*/
=======
>>>>>>> Stashed changes
    }

    public void spawnRoad(int repeatCount)
    {
        List<GameObject> blockList = new List<GameObject>();
        lastRoadPosX += wallWidth;
        Instantiate(RoadPrefab,new Vector3(lastRoadPosX, 0, 0),Quaternion.identity);

        for (int i = 0; i < repeatCount; i++)
        {
            float wallPosZRandom = Random.Range(-17, 17);
            float wallPosXRandom = Random.Range(lastRoadPosX - 500, lastRoadPosX + 500);

<<<<<<< Updated upstream
            foreach (GameObject bloc in blockList)
=======
            float wallPosZRandom = Random.Range(-wallHeight, wallHeight);

            float startPosX = lastRoadPosX - wallWidth/2;
            float repeatX = wallWidth / repeatCount;
            float repeatXupdated = blocNum * repeatX;



            float wallPosXRandom = Random.Range(startPosX + repeatXupdated - repeatXupdated/2,startPosX+repeatXupdated + repeatXupdated/2);

            wallPos = new Vector3(wallPosXRandom, 0, wallPosZRandom);

            blocNum++;

            if(blocNum >= repeatCount)
>>>>>>> Stashed changes
            {
                float distance = Vector3.Distance(bloc.transform.position, new Vector3(wallPosXRandom,0,wallPosZRandom));

                wallPosZRandom = Random.Range(-17, 17);
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
<<<<<<< Updated upstream
                block.transform.position = new Vector3(wallPosXRandom, 0, wallPosZRandom);
=======
                int randomWallNo = Random.Range(0, wallPropertyList.Count); //
                block.GetComponent<WallScript>().WallScr = wallPropertyList[randomWallNo]; //
                block.transform.position = wallPos;
>>>>>>> Stashed changes
                blockList.Add(block);
            }

        }
    }
}
