using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Management : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Hovered;


    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<SelectableCollider>())
            {
                SelectableObject hitSelectable = hit.collider.GetComponent<SelectableCollider>().SelectableObject;

                if (Hovered)
                {
                    if (Hovered != hitSelectable)
                    {
                        Hovered.OnUnhover();

                        Hovered = hitSelectable;
                        Hovered.OnHover();
                    }
                }
                else
                {
                    Hovered = hitSelectable;
                    Hovered.OnHover();
                }
            }
            else
            {
                UnhoverCurrent();
            }
        }
        else
        {
            UnhoverCurrent();
        }
    }


    void UnhoverCurrent()
    {
        if (Hovered)
        {
            Hovered.OnUnhover();
            Hovered = null;
        }
    }
}
