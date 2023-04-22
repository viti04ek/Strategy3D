using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public float CellSize = 1;
    public Camera RaycastCamera;

    private Plane _plane;

    public Building CurrentBuilding;


    void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }


    void Update()
    {
        if (CurrentBuilding == null) return;

        Ray ray = RaycastCamera.ScreenPointToRay(Input.mousePosition);

        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance) / CellSize;

        int x = Mathf.RoundToInt(point.x);
        int z = Mathf.RoundToInt(point.z);

        CurrentBuilding.transform.position = new Vector3(x, 0, z) * CellSize;

        if (Input.GetMouseButtonDown(0))
        {
            CurrentBuilding = null;
        }
    }
}