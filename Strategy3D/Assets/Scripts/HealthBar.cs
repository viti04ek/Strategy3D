using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform ScaleTransform;
    public Transform Target;
    private Transform _cameraTransform;


    void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    
    void LateUpdate()
    {
        transform.position = Target.position + Vector3.up * 2f;
        transform.rotation = _cameraTransform.rotation;
    }


    public void Setup(Transform target)
    {
        Target = target;
    }


    public void SetHealth(int health, int maxHealth)
    {
        float xScale = Mathf.Clamp01((float)health / maxHealth);
        ScaleTransform.localScale = new Vector3(xScale, 1f, 1f);
    }
}
