using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _movementRectangle;

    [SerializeField]
    private Rigidbody _vehicleRigidbody;

    [SerializeField]
    private Transform[] _bookCorners;




    public float _acceleration = 200;
    public float _angularAcceleration = 10;

    //public float _maxVelocity = 50;
    //public float _maxAngularVelocity = 5;

    public float _hoverForce = 500;
    public float _hoverHeight = 2;

    public float _leaningAngle = 6;
    public float _leaningDelay = 0.2f;

    [Header("Debugging")]

    [SerializeField]
    private float _currentVelocity;

    [SerializeField]
    private float _currentAngularVelocity;

    [SerializeField]
    private Vector2 mouseInput;

    //=====================================================================================================
    //
    //=====================================================================================================

    private Vector2 _movRectHalfSize;
    private float leaningVelocity;

    //=====================================================================================================
    //
    //=====================================================================================================

    // Use this for initialization
    void Start()
    {
        _movRectHalfSize = _movementRectangle.rect.size * 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Input
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
            else if (relativeMousePos.y < -_movRectHalfSize.y)
            {
                relativeMousePos.y = -_movRectHalfSize.y;
            }

            //Debug.Log("relative mouse pos: " + relativeMousePos);

            relativeMousePos.x = relativeMousePos.x / _movRectHalfSize.x;
            relativeMousePos.y = relativeMousePos.y / _movRectHalfSize.y;
        }

        mouseInput = relativeMousePos;

        //***********************************************************************************************
        // Hovering

        //#1
        //Ray ray = new Ray(transform.position, -transform.up);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, _hoverHeight))
        //{
        //    float proportionalHeight = (_hoverHeight - hit.distance) / _hoverHeight;
        //    Vector3 appliedHoverForce = Vector3.up * proportionalHeight * _hoverForce;
        //    _vehicleRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        //}

        //#2
        RaycastHit hoverHit;

        foreach (Transform bookCorner in _bookCorners)
        {
            Vector3 downwardForce;
            float distancePercentage;

            if (Physics.Raycast(bookCorner.position, bookCorner.up * -1, out hoverHit, _hoverHeight))
            {
                distancePercentage = 1 - (hoverHit.distance / _hoverHeight);

                downwardForce = transform.up * _hoverForce * distancePercentage;
                downwardForce = downwardForce * Time.fixedDeltaTime * _vehicleRigidbody.mass;

                _vehicleRigidbody.AddForceAtPosition(downwardForce, bookCorner.position);
            }
        }

        //***********************************************************************************************
        // movement

        //#1
        //float targetVelocity = relativeMousePos.y * _maxVelocity;
        //float acceleration = Mathf.Clamp(targetVelocity - _currentVelocity, -_maxAcceleration, _maxAcceleration);
        //_vehicleRigidbody.AddRelativeForce(0, 0, acceleration, ForceMode.Acceleration);
        //_currentVelocity = Mathf.Clamp(_vehicleRigidbody.velocity.magnitude, -_maxVelocity, _maxVelocity);
        //_vehicleRigidbody.velocity = _vehicleRigidbody.velocity.normalized * _currentVelocity;

        //#2
        //_vehicleRigidbody.AddRelativeForce(0, 0, relativeMousePos.y * _acceleration);

        //#3
        if (Physics.Raycast(transform.position, -transform.up, _hoverHeight + 0.5f))
        {
            _vehicleRigidbody.drag = 1;

            Vector3 forwardForce = transform.forward * _acceleration * relativeMousePos.y;
            forwardForce = forwardForce * Time.fixedDeltaTime * _vehicleRigidbody.mass;

            _vehicleRigidbody.AddForce(forwardForce);
        }
        else
        {
            _vehicleRigidbody.drag = 0;
        }

        //***********************************************************************************************
        // rotation

        //#1
        //float targetAngularVelocity = relativeMousePos.x * _maxAngularVelocity;
        //float angularAcceleration = targetAngularVelocity - _currentAngularVelocity;
        //_vehicleRigidbody.AddRelativeTorque(0, angularAcceleration, 0, ForceMode.Acceleration);
        //_currentAngularVelocity = _vehicleRigidbody.angularVelocity.magnitude;

        //#2
        //_vehicleRigidbody.AddRelativeTorque(0, relativeMousePos.x * _angularAcceleration, 0);

        //#3
        Vector3 turnTorque = Vector3.up * _angularAcceleration * relativeMousePos.x;
        turnTorque = turnTorque * Time.fixedDeltaTime * _vehicleRigidbody.mass;
        _vehicleRigidbody.AddTorque(turnTorque);

        //***********************************************************************************************
        // Leaning
        // this comes at the end because it directly changes transform values

        //#1
        //_bookModelTransform.localEulerAngles = new Vector3(0, 0, -relativeMousePos.x * _bookLeaningAngle);

        //#2
        Vector3 newRotation = transform.eulerAngles;
        newRotation.z = Mathf.SmoothDampAngle(newRotation.z, relativeMousePos.x * -_leaningAngle, ref leaningVelocity, _leaningDelay);
        transform.eulerAngles = newRotation;

        //***********************************************************************************************
        // Debugging

        _currentVelocity = _vehicleRigidbody.velocity.magnitude;
        _currentAngularVelocity = _vehicleRigidbody.angularVelocity.magnitude;
    }
}
