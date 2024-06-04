using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static Queue<GameObject> customer;
    private static Queue<GameObject> hotelRooms;

    static GWorld()
    {
        world = new WorldStates();
        customer = new Queue<GameObject>();
        hotelRooms = new Queue<GameObject>();
    }

    private GWorld()
    {
    }

    public void AddCustomer(GameObject go)
    {
        customer.Enqueue(go);
    }

    public GameObject RemoveCustomer()
    {
        if (customer.Count == 0) return null;
        return customer.Dequeue();
    }

    public void AddHotelRoom(GameObject go)
    {
        hotelRooms.Enqueue(go);
    }

    public GameObject RemoveHotelRoom()
    {
        if (hotelRooms.Count == 0) return null;
        return hotelRooms.Dequeue();
    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}
