using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatients : GAction
{
    GameObject resource;
    public override bool PrePerform()
    {
        target = GWorld.Instance.RemoveCustomer();
        if (target == null ) return false;
        resource = GWorld.Instance.RemoveHotelRoom();
        if (resource != null) 
            inventory.AddItems(resource);
        else
        {
            GWorld.Instance.AddCustomer(target);
            target = null;
            return false;
        }
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
        if (target)
            target.GetComponent<GAgent>().inventory.AddItems(resource);
        return true;
    }
}
