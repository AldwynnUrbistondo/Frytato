using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MultipleDrag : MonoBehaviour
{
    List<Draggable> items = new List<Draggable>();
    Camera mainCamera;

    private void Update()
    {
        mainCamera = CameraManager.Instance.activeCamera;

        transform.position = GetMousePos();

        if (Input.GetMouseButton(0))
        {
            
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.Drag(transform.position);
                }
            }
          
        }

        if (Input.GetMouseButtonUp(0))
        {
            foreach (var item in items)
            {
                item.Drop();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Draggable draggable = other.GetComponent<Draggable>();
        if (draggable != null && !items.Contains(draggable))
        {
            items.Add(draggable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Draggable draggable = other.GetComponent<Draggable>();
        if (draggable != null && items.Contains(draggable))
        {
            draggable.Drop();
            items.Remove(draggable);
        }
    }


    Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        return mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1));
    }
}
