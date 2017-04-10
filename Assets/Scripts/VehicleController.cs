using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _movementRectangle;

    [SerializeField]
    private Rigidbody _vehicleRigidbody;


    private Vector2 _movRectHalfSize;

    private float _maxVelocity = 90;
    //private float _maxAcceleration = 4;

    private float _maxAngularVelocity = 5;
    //private float _maxAngularAcceleration = 0.1f;

    [SerializeField]
    private float _currentVelocity;

    [SerializeField]
    private float _currentAngularVelocity;

    // Use this for initialization
    void Start()
    {
        _movRectHalfSize = _movementRectangle.rect.size * 0.5f;

        _vehicleRigidbody.maxAngularVelocity = _maxAngularVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 relativeMousePos = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            relativeMousePos = Input.mousePosition - _movementRectangle.position;

            if (relativeMousePos.x > _movRectHalfSize.x)
            {
                relativeMousePos.x = _movRectHalfSize.x;
            }
            else if (relativeMousePos.x < -_movRectHalfSize.x)
            {
                relativeMousePos.x = -_movRectHalfSize.x;
            }

            if (relativeMousePos.y > _movRectHalfSize.y)
            {
                relativeMousePos.y = _movRectHalfSize.y;
            }
            else if (relativeMousePos.y < 0)
            {
                relativeMousePos.y = 0;
            }

            //Debug.Log("relative mouse pos: " + relativeMousePos);

            relativeMousePos.x = relativeMousePos.x / _movRectHalfSize.x;
            relativeMousePos.y = relativeMousePos.y / _movRectHalfSize.y;
        }

        //movement
        //float targetVelocity = relativeMousePos.y * _maxVelocity;

        //float acceleration = Mathf.Clamp(targetVelocity - _currentVelocity, -_maxAcceleration, _maxAcceleration);
        //_vehicleRigidbody.AddRelativeForce(0, 0, acceleration, ForceMode.Acceleration);

        //_currentVelocity = Mathf.Clamp(_vehicleRigidbody.velocity.magnitude, -_maxVelocity, _maxVelocity);
        //_vehicleRigidbody.velocity = _vehicleRigidbody.velocity.normalized * _currentVelocity;

        //rotation
        //float targetAngularVelocity = relativeMousePos.x * _maxAngularVelocity;

        //float angularAcceleration = targetAngularVelocity - _currentAngularVelocity;
        //_vehicleRigidbody.AddRelativeTorque(0, angularAcceleration, 0, ForceMode.Acceleration);

        //_currentAngularVelocity = _vehicleRigidbody.angularVelocity.magnitude;

        _vehicleRigidbody.AddRelativeForce(0, 0, relativeMousePos.y * _maxVelocity);
        _vehicleRigidbody.AddRelativeTorque(0, relativeMousePos.x * _maxAngularVelocity, 0);
    }
}
