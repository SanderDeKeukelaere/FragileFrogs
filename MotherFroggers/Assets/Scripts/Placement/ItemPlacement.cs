using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    private float _maxHitDistance = 5.0f;

    public void HandleClick()
    {
        Debug.Log("Click registered!");
        //Ignore invalid clicks
        if (IsValidClick(out RaycastHit hitInfo) == false)
            return;

        //...
        Debug.Log("Click is Valid!");
    }

    private bool IsValidClick(out RaycastHit hitInfo)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ignore other checks if we didn't hit anything
        if (Physics.Raycast(ray, out hitInfo, _maxHitDistance, LayerMask.GetMask("Tile")) == false)
            return false;

        Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up * 2.0f, Color.red, 2.0f);

        //Check if we hit a clickable object
        GameObject clickedObject = hitInfo.collider.gameObject;

        return clickedObject.GetComponent<Clickable>().IsClickable;
    }
}
