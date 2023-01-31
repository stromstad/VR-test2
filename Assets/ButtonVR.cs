using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    Vector3 initialButtonLocalPosition;
    Vector3 initialColliderPosition;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            initialButtonLocalPosition = button.transform.localPosition;
            initialColliderPosition = other.transform.localPosition;
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(
                initialButtonLocalPosition.x,
                Mathf.Max(initialButtonLocalPosition.y - 0.015f,  initialButtonLocalPosition.y - initialColliderPosition.y - other.transform.localPosition.y),
                initialButtonLocalPosition.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = initialButtonLocalPosition;
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
        sphere.AddComponent<XRSimpleInteractable>();
    }
}
