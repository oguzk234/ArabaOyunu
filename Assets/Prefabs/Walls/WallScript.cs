using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public WallScriptable WallScr;

    private void Start()
    {
        gameObject.name = WallScr.WallName;
        gameObject.transform.localScale = new Vector3(WallScr.ScaleX, WallScr.ScaleY, WallScr.ScaleZ);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0 + WallScr.ScaleY / 2, gameObject.transform.position.z);
    }
}
