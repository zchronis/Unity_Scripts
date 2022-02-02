using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMk1 : MonoBehaviour
{

    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    [SerializeField]
    private float zoomSpeed = 20f;

    [SerializeField]
    private int playerIndex = 0;
    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public float RotationsSpeed = 0.5f;
    private Vector2 inputVector = Vector2.zero;
    private int zoom = 2; //2 is zoomed all the way in, 0 is all the way out

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    public void SetZoomLevel(Vector2 direction)
    {
        zoom += (int)direction.y;
        if(zoom > 2) zoom = 2;
        if(zoom < 0) zoom = 0;
    }

    // Use this for initialization
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {

        if (RotateAroundPlayer)
        {
            Quaternion camTurnAngle =
                Quaternion.AngleAxis(inputVector.x * RotationsSpeed, Vector3.up);

            _cameraOffset = camTurnAngle * _cameraOffset;
        }

        Vector3 newPos = PlayerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
        

        if (zoom == 0)
        {
            //set the up and down distance
            if(_cameraOffset.y < 15f)
                _cameraOffset.y += zoomSpeed * Time.deltaTime;
            if(_cameraOffset.y > 15f)
                _cameraOffset.y = 15f;

            //Debug.Log(_cameraOffset);
        }

        if (zoom == 1)
        {
            //_cameraOffset.y = 10f;
            if(_cameraOffset.y < 10f)
                _cameraOffset.y += zoomSpeed * Time.deltaTime;
            if(_cameraOffset.y > 10f)
                _cameraOffset.y -= zoomSpeed * Time.deltaTime;

            //Debug.Log(_cameraOffset);
        }

        if (zoom == 2)
        {
            //_cameraOffset.y = 5f;
            if(_cameraOffset.y < 5f)
                _cameraOffset.y = 5f;
            if(_cameraOffset.y > 5f)
                _cameraOffset.y -= zoomSpeed * Time.deltaTime;

            //Debug.Log(_cameraOffset);
        }
    }
}