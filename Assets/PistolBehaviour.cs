using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PistolBehaviour : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float velocity = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        var grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    void FireBullet(ActivateEventArgs args)
    {
        var spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = velocity * spawnPoint.forward;
        Destroy(spawnedBullet, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
