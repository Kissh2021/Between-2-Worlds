using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IClimber[] climbers = collision.gameObject.GetComponents<IClimber>();
        foreach(var climber in climbers)
        {
            climber.climb(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IClimber[] climbers = collision.gameObject.GetComponents<IClimber>();
        foreach (var climber in climbers)
        {
            climber.unclimb();
        }
    }
}
