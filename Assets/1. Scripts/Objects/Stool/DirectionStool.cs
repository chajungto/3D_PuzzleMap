using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionStool : Stool
{
    public string direction;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StageEvent stageEvent = FindObjectOfType<StageEvent>();
            if (stageEvent != null)
            {
                stageEvent.AddDirection(direction); 
                Debug.Log(direction);
            }
        }

    }

    protected override void OnTriggerExit(Collider other)
    {

    }
}
