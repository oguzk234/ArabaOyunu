using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "New Wall", menuName = "Wall")]
public class WallScriptable : ScriptableObject
{
    public string WallName;
    public float ScaleY;
    public float ScaleX;
    public float ScaleZ;
    public Sprite Textur;
}
