using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public Quaternion Rota;
    public GameObject LastSpawnedBuilding;
    public List<GameObject> BuildingList = new List<GameObject>();

    public bool Reversed;
    public void SpawnBuilding(int Count)
    {
        if (!Reversed)
        {
            for (int b = 0; b < Count; b++)
            {
                int i = Random.Range(0, BuildingList.Count);
                GameObject spawned = Instantiate(BuildingList[i]);
                spawned.transform.position = new Vector3(LastSpawnedBuilding.transform.position.x + LastSpawnedBuilding.GetComponent<BuildingStats>().offset.x + spawned.GetComponent<BuildingStats>().offset.x, spawned.GetComponent<BuildingStats>().offset.y, spawned.GetComponent<BuildingStats>().offset.z);
                LastSpawnedBuilding = spawned;
            }
        }
        else if (Reversed)
        {
            for (int b = 0; b < Count; b++)
            {
                int i = Random.Range(0, BuildingList.Count);
                GameObject spawned = Instantiate(BuildingList[i]);
                spawned.transform.position = new Vector3(LastSpawnedBuilding.transform.position.x + LastSpawnedBuilding.GetComponent<BuildingStats>().offset.x + spawned.GetComponent<BuildingStats>().offset.x, spawned.GetComponent<BuildingStats>().offset.y, -spawned.GetComponent<BuildingStats>().offset.z);
                if(spawned.name == "Build3(Clone)" || spawned.name == "Build4(Clone)")
                {
                    spawned.transform.rotation = spawned.transform.rotation * Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    spawned.transform.rotation = spawned.transform.rotation * Quaternion.Euler(0, 0, 180);
                }
                LastSpawnedBuilding = spawned;
            }
        }


        //spawned.transform.position = LastSpawnedBuildingLoc + new Vector3(spawned.GetComponent<BuildingStats>().offset.x, 0, 0);
        //spawned.transform.position = LastSpawnedBuildingLoc + new Vector3(spawned.GetComponent<BuildingStats>().offset.x, 0, 0);
        //spawned.transform.position = LastSpawnedBuildingLoc + spawned.GetComponent<BuildingStats>().offset;
        //LastSpawnedBuildingLoc = spawned.transform.position;
    }

    private void Start()
    {
        SpawnBuilding(50);
    }
}
