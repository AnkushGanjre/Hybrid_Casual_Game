using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateVirtualCamera : MonoBehaviour
{
    [SerializeField] GameObject cam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cam.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cam.SetActive(false);
        }
    }
}
