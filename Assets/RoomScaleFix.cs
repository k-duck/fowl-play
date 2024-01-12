using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(XROrigin))]

public class RoomScaleFix : MonoBehaviour
{
    private CharacterController _character;
    private XROrigin _XRrig;

    void Start()
    {
        _character = GetComponent<CharacterController>();
        _XRrig = GetComponent<XROrigin>();
    }

    private void FixedUpdate()
    {
        _character.height = _XRrig.CameraInOriginSpaceHeight + 0.15f;

        var centerPoint = transform.InverseTransformPoint(_XRrig.Camera.transform.position);
        _character.center = new Vector3(centerPoint.x, _character.height / 2 + _character.skinWidth, centerPoint.z);


        _character.Move(new Vector3(0.001f, 0.001f, 0.001f));
        _character.Move(new Vector3(-0.001f, -0.001f, -0.001f));
    }

}
