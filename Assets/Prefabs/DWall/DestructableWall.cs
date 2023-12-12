using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : WallScript
{
    [SerializeField] private List<Rigidbody> RigidbodyList;
    [SerializeField] private float destroyTime;

    public override void Start()
    {
        gameObject.name = WallScr.WallName;
        gameObject.transform.localScale = new Vector3(WallScr.ScaleX*0.4f, WallScr.ScaleY *0.35f, WallScr.ScaleZ * 0.2f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0 + WallScr.ScaleY / 2, gameObject.transform.position.z);
    }

    public void GoDynamic()
    {
        foreach(Rigidbody rg in RigidbodyList)
        {
            rg.isKinematic = false;
        }
        Destroy(gameObject, destroyTime);
    }
}
