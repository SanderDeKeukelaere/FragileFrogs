using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _max = new Vector2(19, 22);
    [SerializeField] private Vector2 _min = new Vector2(2, 7);
    [SerializeField] private Vector2 _zoomBounds = new Vector2(-5, 10);

    private float zoom = 0.0f;

    [SerializeField] private Vector2 _epsilon = new Vector2(250, 150);

    [SerializeField] private float _moveSpeed = 8.0f;
    [SerializeField] private float _zoomSpeed = 1.0f;

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        Vector3 velocity = new Vector3();

        if (mousePos.x < _epsilon.x) velocity.x = _moveSpeed;
        else if (mousePos.x > Screen.width - _epsilon.x) velocity.x = -_moveSpeed;

        if (mousePos.y < _epsilon.y) velocity.z = _moveSpeed;
        else if(mousePos.y > Screen.height - _epsilon.y) velocity.z = -_moveSpeed;

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

        zoom += Input.mouseScrollDelta.y * _zoomSpeed;
        if(zoom < _zoomBounds.x)
        {
            zoom = _zoomBounds.x;
        }
        else if (zoom > _zoomBounds.y)
        {
            zoom = _zoomBounds.y;
        }
        else
        {
            transform.position += transform.forward * Input.mouseScrollDelta.y * _zoomSpeed;
        }
    }
}
