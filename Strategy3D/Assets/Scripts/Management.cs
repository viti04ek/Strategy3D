using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Management : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Hovered;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();


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


        if (Input.GetMouseButtonUp(0))
        {
            if (Hovered)
            {
                if (!Input.GetKey(KeyCode.LeftControl))
                    UnselectAll();

                Select(Hovered);
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            UnselectAll();
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


    void UnselectAll()
    {
        foreach (var selected in ListOfSelected)
        {
            selected.Unselect();
        }
        ListOfSelected.Clear();
    }


    void Select(SelectableObject selectableObject)
    {
        if (!ListOfSelected.Contains(selectableObject))
        {
            ListOfSelected.Add(selectableObject);
            selectableObject.Select();
        }
    }
}
