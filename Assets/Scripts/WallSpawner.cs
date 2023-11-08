using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject RoadPrefab;
    [SerializeField] private GameObject WallPrefab;
    [SerializeField] private List<WallScriptable> wallPropertyList = new List<WallScriptable>();
    [SerializeField] private float lastRoadPosX;
    [SerializeField] private float firstRoadPosX;
    //[SerializeField] private float wallSpawnCd;
    //[SerializeField] private float wallSpawnCdMaxDefault;
    //[SerializeField] private float wallSpawnCdMultiplier;
    private void Awake()
    {
        lastRoadPosX = firstRoadPosX;
    }

    private void Start()
    {
        spawnRoad();
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

    public void spawnRoad()
    {
        List<GameObject> blockList = new List<GameObject>();
        lastRoadPosX += 1000;
        Instantiate(RoadPrefab,new Vector3(lastRoadPosX, 0, 0),Quaternion.identity);

        int repeatCount = 10;
        for (int i = 0; i < repeatCount; i++)
        {
            float wallPosZRandom = Random.Range(-12, 12);
            float wallPosXRandom = Random.Range(lastRoadPosX - 500, lastRoadPosX + 500);

            foreach (GameObject bloc in blockList)
            {
                float distance = Vector3.Distance(bloc.transform.position, new Vector3(wallPosXRandom,0,wallPosZRandom));

                while (Mathf.Abs(distance) < 12f)
                {
                    wallPosZRandom = Random.Range(-12, 12);
                    wallPosXRandom = Random.Range(lastRoadPosX - 500, lastRoadPosX + 500);
                    distance = Vector3.Distance(bloc.transform.position, new Vector3(wallPosXRandom, 0, wallPosZRandom));
                }
            }


            GameObject block = Instantiate(WallPrefab);
            int randomWallNo = Random.Range(0, wallPropertyList.Count);
            block.GetComponent<WallScript>().WallScr = wallPropertyList[randomWallNo];
            block.transform.position = new Vector3(wallPosXRandom, 0, wallPosZRandom);
            blockList.Add(block);
        }
    }
}
