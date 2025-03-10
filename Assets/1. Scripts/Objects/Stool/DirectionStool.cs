using UnityEngine;

public class DirectionStool : Stool
{
    public string direction;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GPSEvent gpsEvent = FindObjectOfType<GPSEvent>();
            if (gpsEvent != null)
            {
                gpsEvent.AddDirection(direction); 
                Debug.Log(direction);
            }
        }
    }
}
