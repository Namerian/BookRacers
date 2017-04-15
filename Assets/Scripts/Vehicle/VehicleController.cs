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



    [SerializeField]
    private float _acceleration = 5000;

    [SerializeField]
    private float _angularAcceleration = 400;

    //public float _maxVelocity = 50;
    //public float _maxAngularVelocity = 5;

    [SerializeField]
    private float _hoverForce = 500;

    [SerializeField]
    private float _hoverHeight = 2;

    [SerializeField]
    private float _leaningAngle = 25;

    [SerializeField]
    private float _leaningDelay = 0.2f;

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

        if (GameController.Instance != null)
        {
            PlayerData player = GameController.Instance.PlayerData;
            PilotData pilot = GameController.Instance.PilotData[player.CurrentPilotIndex];
            BookData book = GameController.Instance.BookData[player.CurrentBookIndex];

            _acceleration = book.Acceleration * (1 + pilot.Acceleration / 100f) * 100f;
            _angularAcceleration = book.TurnSpeed * (1 + pilot.TurnSpeed / 100f) * 100f;
            _vehicleRigidbody.mass = book.Mass * (1 + pilot.Mass / 100f) * 100f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //****************************************************
        // Hovering

        HandleHovering();

        //****************************************************
        if (!LevelController.Instance.Running)
        return;

        //****************************************************
        // Input

        Vector2 relativeMousePos = HandleInput();
        mouseInput = relativeMousePos;

        //****************************************************
        // movement

        HandleMovement(relativeMousePos);

        //****************************************************
        // rotation

        HandleTurning(relativeMousePos);

        //****************************************************
        // Leaning
        // this comes at the end because it directly changes transform values

        HandleLeaning(relativeMousePos);

        //****************************************************
        // Debugging

        _currentVelocity = _vehicleRigidbody.velocity.magnitude;
        _currentAngularVelocity = _vehicleRigidbody.angularVelocity.magnitude;
    }

    //=====================================================================================================
    //
    //=====================================================================================================

    private void HandleHovering()
    {
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
                downwardForce = downwardForce * Time.deltaTime * _vehicleRigidbody.mass;

                _vehicleRigidbody.AddForceAtPosition(downwardForce, bookCorner.position);
            }
        }
    }

    private void HandleMovement(Vector2 relativeMousePos)
    {
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
            forwardForce = forwardForce * Time.deltaTime * _vehicleRigidbody.mass;

            _vehicleRigidbody.AddForce(forwardForce);
        }
        else
        {
            _vehicleRigidbody.drag = 0;
        }
    }

    private void HandleTurning(Vector2 relativeMousePos)
    {
        //#1
        //float targetAngularVelocity = relativeMousePos.x * _maxAngularVelocity;
        //float angularAcceleration = targetAngularVelocity - _currentAngularVelocity;
        //_vehicleRigidbody.AddRelativeTorque(0, angularAcceleration, 0, ForceMode.Acceleration);
        //_currentAngularVelocity = _vehicleRigidbody.angularVelocity.magnitude;

        //#2
        //_vehicleRigidbody.AddRelativeTorque(0, relativeMousePos.x * _angularAcceleration, 0);

        //#3
        Vector3 turnTorque = Vector3.up * _angularAcceleration * relativeMousePos.x;
        turnTorque = turnTorque * Time.deltaTime * _vehicleRigidbody.mass;
        _vehicleRigidbody.AddTorque(turnTorque);
    }

    private void HandleLeaning(Vector2 relativeMousePos)
    {
        //#1
        //_bookModelTransform.localEulerAngles = new Vector3(0, 0, -relativeMousePos.x * _bookLeaningAngle);

        //#2
        Vector3 newRotation = transform.eulerAngles;
        newRotation.z = Mathf.SmoothDampAngle(newRotation.z, relativeMousePos.x * -_leaningAngle, ref leaningVelocity, _leaningDelay);
        transform.eulerAngles = newRotation;
    }

    private Vector2 HandleInput()
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
            else if (relativeMousePos.y < -_movRectHalfSize.y)
            {
                relativeMousePos.y = -_movRectHalfSize.y;
            }

            //Debug.Log("relative mouse pos: " + relativeMousePos);

            relativeMousePos.x = relativeMousePos.x / _movRectHalfSize.x;
            relativeMousePos.y = relativeMousePos.y / _movRectHalfSize.y;
        }

        return relativeMousePos;
    }
}
