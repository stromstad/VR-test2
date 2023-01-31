using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetModel : MonoBehaviour
{
    public GameObject GameObject;
    public Vector3 Offset;

    // Update is called once per frame
    void Update()
    {
        GameObject.transform.position += Offset;
    }
}
