using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WorldManager : MonoBehaviour 
{
    public static WorldManager Instance;
    public List<GameObject> CustomerList = new List<GameObject>();
    public List<GameObject> AllHotelRooms = new List<GameObject>();

    public List<GameObject> OccupiedHotelRooms = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
