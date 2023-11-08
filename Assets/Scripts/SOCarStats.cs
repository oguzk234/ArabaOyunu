using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Stats", menuName = "Car Stats")]
public class SOCarStats : ScriptableObject
{
    public float health;
    public float turnSpeed;
    public float acceleration;
}
