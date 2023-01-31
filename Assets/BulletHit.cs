using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public GameObject particleEffect;
    public GameObject bullet;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Collided with {other.gameObject.name} tagged {other.gameObject.tag}");
        if (other.gameObject.tag == "zombie")
        {
            particleEffect.SetActive(true);
            Destroy(bullet);
        }
    }

}
