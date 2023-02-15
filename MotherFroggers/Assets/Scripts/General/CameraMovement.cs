using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _max = new Vector2(19, 22);
    [SerializeField] private Vector2 _min = new Vector2(2, 7);
    [SerializeField] private Vector2 _zoomBounds = new Vector2(-5, 10);

    private float _zoom = 0.0f;

    [SerializeField] private Vector2 _epsilon = new Vector2(250, 150);

    [SerializeField] private float _moveSpeed = 8.0f;
    [SerializeField] private float _zoomSpeed = 1.0f;
    [SerializeField] private float _dragSpeed = 20.0f;

    private Vector3 _dragOrigin;
    private Vector3 _resetCamera;

    private bool _isDragging;

    private void Start()
    {
        _resetCamera = Camera.main.transform.position;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        
        Vector3 velocity = new Vector3();

        //if (mousePos.x < _epsilon.x) velocity.x = _moveSpeed;
        //else if (mousePos.x > Screen.width - _epsilon.x) velocity.x = -_moveSpeed;
        //
        //if (mousePos.y < _epsilon.y) velocity.z = _moveSpeed;
        //else if(mousePos.y > Screen.height - _epsilon.y) velocity.z = -_moveSpeed;
        //
        //transform.position += velocity * Time.deltaTime;
        //
        //Vector3 clampedPos = transform.position;
        //if(clampedPos.x < _min.x)
        //{
        //    clampedPos.x = _min.x;
        //}
        //else if(clampedPos.x > _max.x)
        //{
        //    clampedPos.x = _max.x;
        //}
        //if (clampedPos.z < _min.y)
        //{
        //    clampedPos.z = _min.y;
        //}
        //else if (clampedPos.z > _max.y)
        //{
        //    clampedPos.z = _max.y;
        //}
        //transform.position = clampedPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = _resetCamera;
            _zoom = 0;
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            _dragOrigin = mousePos;
            _isDragging = true;
        }
        
        if(Input.GetMouseButtonUp(1))
        {
            _isDragging = false;
        }
        
        if(_isDragging)
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(new Vector3(mousePos.x, mousePos.y) - _dragOrigin);

            _dragOrigin = mousePos;

            difference.Normalize();

            difference.z = difference.y;

            difference.y = 0f;

            velocity = difference * _dragSpeed;
        }


        if (Input.GetKey(KeyCode.W))
        {
            velocity.z = -_moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.z = _moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x = _moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x = -_moveSpeed;
        }

        transform.position += velocity * Time.deltaTime;

        Vector3 clampedPos = transform.position;
        if(clampedPos.x < _min.x)
        {
            clampedPos.x = _min.x;
        }
        else if(clampedPos.x > _max.x)
        {
            clampedPos.x = _max.x;
        }
        if (clampedPos.z < _min.y)
        {
            clampedPos.z = _min.y;
        }
        else if (clampedPos.z > _max.y)
        {
            clampedPos.z = _max.y;
        }
        
        transform.position = clampedPos;
        
        _zoom += Input.mouseScrollDelta.y * _zoomSpeed;
        
        if(_zoom < _zoomBounds.x)
        {
            _zoom = _zoomBounds.x;
        }
        else if (_zoom > _zoomBounds.y)
        {
            _zoom = _zoomBounds.y;
        }
        else
        {
            transform.position += transform.forward * Input.mouseScrollDelta.y * _zoomSpeed;
        }
    }
}
