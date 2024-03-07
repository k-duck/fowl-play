using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentScript : MonoBehaviour
{
    [SerializeField] private Animator ventAnimator;

    public void OpenVent()
    {
        ventAnimator.SetTrigger("OpenVent");
    }

    public void GooseInVent() 
    {
        ventAnimator.SetBool("InVent", true);
    }

    public void GooseLeaveVent()
    {
        OpenVent();
        ventAnimator.SetBool("InVent", false);
    }
}
