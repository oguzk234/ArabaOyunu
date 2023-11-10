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
        //spawnRoad(repeatCount);
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

        int blocNum = 1;

        for (int i = 0; i < repeatCount; i++)
        {
            Vector3 wallPos = Vector3.zero;

            float wallPosZRandom = Random.Range(-15f, 15f);
            //float wallPosXRandom = Random.Range(lastRoadPosX - 500, lastRoadPosX + 500);

            //TODO Magic number
            float startPosX = lastRoadPosX - 500;
            float repeatX = 1000 / repeatCount;
            float repeatXupdated = blocNum * repeatX;



            float wallPosXRandom = Random.Range(startPosX + repeatXupdated - repeatXupdated/2,startPosX+repeatXupdated + repeatXupdated/2);

            wallPos = new Vector3(wallPosXRandom, 0, wallPosZRandom);

            blocNum++;

            if(blocNum >= repeatCount)
            {
                blocNum = 0;
            }



            int RandomChoose = Random.Range(0, 3);
            
            //TODO Bu objeleri bir listeye koyabilirsin
            //TODO RandomChoice'a göre listeen o indexi seçersin
            //TODO listenin countundan büyük olduğu durumda da hiçbir şey yapmazsın

            if(RandomChoose == 0 || RandomChoose == 1)
            {
                GameObject block = Instantiate(WallPrefab);
                int randomWallNo = Random.Range(0, wallPropertyList.Count);
                block.GetComponent<WallScript>().WallScr = wallPropertyList[randomWallNo];
                block.transform.position = wallPos;
                blockList.Add(block);
            }
            else
            {
                GameObject block = Instantiate(DWallPrefab);
                block.transform.position = wallPos;
                blockList.Add(block);
            }
        }
    }
}
