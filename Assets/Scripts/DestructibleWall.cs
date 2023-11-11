using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : WallScript
{
    [SerializeField] private List<Rigidbody> _subWallRigidbodyList;
    [SerializeField] private float destroyTime = 3.5f;

    public override void Start()
    {
        gameObject.name = WallScr.WallName;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * WallScr.ScaleX,gameObject.transform.localScale.y * WallScr.ScaleY,gameObject.transform.localScale.z * WallScr.ScaleZ * 0.5f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0 + WallScr.ScaleY / 2, gameObject.transform.position.z);
    }
    public void goDynamic()
    {
        foreach(Rigidbody rb in _subWallRigidbodyList)
        {
            rb.isKinematic = false;
            Destroy(this.gameObject, destroyTime);
        }
    }
}
