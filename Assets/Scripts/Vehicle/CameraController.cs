using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _distanceBack;

    [SerializeField]
    private float _distanceUp;

    [SerializeField]
    private float _lookAtForward;

    [SerializeField]
    private float _minimumHeight;

    [SerializeField]
    private float _delay;

    private Vector3 positionVelocity;

    void FixedUpdate()
    {
        Vector3 newPosition = _target.position + (_target.forward * _distanceBack);
        newPosition.y = Mathf.Max(newPosition.y + _distanceUp, _minimumHeight);

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref positionVelocity, _delay);

        Vector3 focalPoint = _target.position + (_target.forward * _lookAtForward);
        transform.LookAt(focalPoint);
    }

    private void OnValidate()
    {
        if (_target != null)
            FixedUpdate();
    }
}
