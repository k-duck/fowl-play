using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoors : MonoBehaviour
{
    public Animator DoorR;
    public Animator DoorL;
    // Start is called before the first frame update
   public void TriggerDoors()
    {
        DoorL.SetTrigger("EffectDoor");
        DoorR.SetTrigger("EffectDoor");
    }
}
