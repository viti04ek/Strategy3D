using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Management : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Hovered;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();

    public Image FrameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;


    void Update()
    {
        // Selecting and moving SelectableObject

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

            if (hit.collider.tag == "Ground")
            {
                foreach (var selected in ListOfSelected)
                {
                    selected.WhenClickOnGround(hit.point);
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            UnselectAll();
        }


        // Box selection

        if (Input.GetMouseButtonDown(0))
        {
            _frameStart = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            FrameImage.enabled = true;

            _frameEnd = Input.mousePosition;

            Vector2 min = Vector2.Min(_frameStart, _frameEnd);
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);

            FrameImage.rectTransform.anchoredPosition = min;

            Vector2 size = max - min;
            FrameImage.rectTransform.sizeDelta = size;
        }

        if (Input.GetMouseButtonUp(0))
        {
            FrameImage.enabled = false;
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
