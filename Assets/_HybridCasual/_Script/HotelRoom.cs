using UnityEngine;

public class HotelRoom : MonoBehaviour
{
    public bool IsActive = false;
    public Transform TargetPosition;
    public bool IsOccupied = false;
    public Vector3 BedPosition;

    private void Start()
    {
        BedPosition = TargetPosition.position;
        if (IsActive)
        {
            WorldManager.Instance.AllHotelRooms.Add(this.gameObject);
        }
    }

    public void SetRoomActive()
    {
        if (!IsActive)
        {
            IsActive = true;
            WorldManager.Instance.AllHotelRooms.Add(this.gameObject);
        }
    }
}
