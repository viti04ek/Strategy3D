using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SelectionState
{
    UnitsSelected,
    Frame,
    Other
}


public class Management : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Hovered;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();

    public Image FrameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    public SelectionState CurrentSelectionState;


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

                CurrentSelectionState = SelectionState.UnitsSelected;
                Select(Hovered);
            }
        }

        if (Input.GetMouseButtonUp(0) && CurrentSelectionState == SelectionState.UnitsSelected)
        {
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
            _frameEnd = Input.mousePosition;

            Vector2 min = Vector2.Min(_frameStart, _frameEnd);
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);

            Vector2 size = max - min;

            if (size.magnitude > 10)
            {
                FrameImage.enabled = true;

                FrameImage.rectTransform.anchoredPosition = min;
                FrameImage.rectTransform.sizeDelta = size;


                Rect rect = new Rect(min, size);

                UnselectAll();

                Unit[] allUnits = FindObjectsOfType<Unit>();
                foreach (var unit in allUnits)
                {
                    Vector2 screenPosition = Camera.WorldToScreenPoint(unit.transform.position);
                    if (rect.Contains(screenPosition))
                    {
                        Select(unit);
                    }
                }

                CurrentSelectionState = SelectionState.Frame;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            FrameImage.enabled = false;

            if (ListOfSelected.Count > 0)
                CurrentSelectionState = SelectionState.UnitsSelected;
            else
                CurrentSelectionState = SelectionState.Other;
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

        CurrentSelectionState = SelectionState.Other;
    }


    void Select(SelectableObject selectableObject)
    {
        if (!ListOfSelected.Contains(selectableObject))
        {
            ListOfSelected.Add(selectableObject);
            selectableObject.Select();
        }
    }


    public void Unselect(SelectableObject selectableObject)
    {
        if (ListOfSelected.Contains(selectableObject))
        {
            ListOfSelected.Remove(selectableObject);
        }
    }
}
