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
    [SerializeField] private float roadSize = 1000;
    [SerializeField] private float wallPosHeightDifference;
    public int repeatCount = 10;

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
        List<GameObject> blockList = new List<GameObject>();
        lastRoadPosX += roadSize;
        Instantiate(RoadPrefab,new Vector3(lastRoadPosX, 0, 0),Quaternion.identity);

        int blocNum = 1;

        for (int i = 0; i < repeatCount; i++)
        {
            Vector3 wallPos = Vector3.zero;

            float wallPosZRandom = Random.Range(-wallPosHeightDifference, wallPosHeightDifference);

            float startPosX = lastRoadPosX - roadSize/2;
            float repeatX = roadSize / repeatCount;
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
