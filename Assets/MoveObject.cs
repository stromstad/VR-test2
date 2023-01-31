using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;

[Serializable]
public class MovableObject
{
    public GameObject Object;
    public Vector3 Speed;
    public float Distance;
    bool isMoving;
    DateTimeOffset started;
    Vector3 initialPosition;

    public void Init()
    {
        isMoving = false;
    }

    public void Move()
    {
        if (!isMoving)
        { 
            isMoving = true;
            initialPosition = Object.transform.localPosition;
            started = DateTimeOffset.UtcNow;
        }
    }

    public void Update()
    {
        if (isMoving)
        {
            var moved = initialPosition - Object.transform.localPosition;

            if (moved.magnitude >= Distance)
            {
                isMoving = false;
                Speed = -1 * Speed;
            }
            else
            {
                Object.transform.localPosition += ((float)(DateTimeOffset.UtcNow - started).Milliseconds) * Speed / 1000.0f;
            }
        }
    }

}

public class MoveObject : MonoBehaviour
{
    public MovableObject[] MovableObjects;

    public void OnStart()
    {
        foreach (var obj in MovableObjects)
            obj.Init();
    }

    public void Move()
    {
        foreach (var obj in MovableObjects)
            obj.Move();
    }

    public void Update()
    {
        foreach (var obj in MovableObjects)
            obj.Update();
    }
}
public enum Direction
{
    xDirectoin, yDirection, zDirection
}