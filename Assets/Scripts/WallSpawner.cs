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
    [SerializeField] private float roadHeight = 15;
    [SerializeField] private float roadWidth = 1000;
    private void Awake()
    {
        lastRoadPosX = firstRoadPosX;
    }

    private void Start()
    {
        //spawnRoad(repeatCount);
    }

    public void spawnRoad(int repeatCount)
    {
        lastRoadPosX += roadWidth;
        Instantiate(RoadPrefab,new Vector3(lastRoadPosX, 0, 0),Quaternion.identity);

        int blocNum = 1;

        for (int i = 0; i < repeatCount; i++)
        {
            Vector3 wallPos = Vector3.zero;

            float wallPosZRandom = Random.Range(-roadHeight, roadHeight);
            //float wallPosXRandom = Random.Range(lastRoadPosX - 500, lastRoadPosX + 500);

            float startPosX = lastRoadPosX - roadWidth/2;
            float repeatX = roadWidth / repeatCount;
            float repeatXupdated = blocNum * repeatX;



            float wallPosXRandom = Random.Range(startPosX + repeatXupdated - repeatXupdated/2,startPosX+repeatXupdated + repeatXupdated/2);

            wallPos = new Vector3(wallPosXRandom, 0, wallPosZRandom);

            blocNum++;

            if(blocNum >= repeatCount)
            {
                blocNum = 0;
            }



            int RandomChoose = Random.Range(0, 3);

            if(RandomChoose == 0 || RandomChoose == 1)
            {
                GameObject block = Instantiate(WallPrefab);
                int randomWallNo = Random.Range(0, wallPropertyList.Count);
                block.GetComponent<WallScript>().WallScr = wallPropertyList[randomWallNo];
                block.transform.position = wallPos;
            }
            else
            {
                GameObject block = Instantiate(DWallPrefab);
                int randomWallNo = Random.Range(0, wallPropertyList.Count);
                block.GetComponent<WallScript>().WallScr = wallPropertyList[randomWallNo];
                block.transform.position = wallPos;
            }
        }
    }
}
