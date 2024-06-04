using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject patientPrefab;
    [SerializeField] int numPatients;

    void Start()
    {
        for (int i = 0; i < numPatients; i++)
        {
            Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
        }

        Invoke(nameof(SpawnPatient), 5);
    }

    void SpawnPatient()
    {
        Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
        Invoke(nameof(SpawnPatient), Random.Range(2, 10));
    }
}
