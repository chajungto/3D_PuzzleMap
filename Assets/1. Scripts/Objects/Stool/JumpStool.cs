using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStool : Stool
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.Player.playerController.ForcedJump();
        }
    }

}
