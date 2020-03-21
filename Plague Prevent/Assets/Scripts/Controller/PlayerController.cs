using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region fields
    private static PlayerController _instance;
    
    [SerializeField]public float scrollSpeed = 30.0f;

    public float minDist = 30.0f;

    private const float MaxZoom = 20.0f;

    private const float MinZoom = 1.0f;

    public Camera _camera;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
        _camera = Camera.main;
    }
    #endregion

    #region methods
    public PlayerController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }

    private void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float speed = (_camera.orthographicSize/MaxZoom) * scrollSpeed;
        Vector3 camMov = Vector3.zero;
        float screenBorderProximity = 1000;

        if (mouseX < minDist)
        {
            camMov += Vector3.left;
            screenBorderProximity = Mathf.Min(screenBorderProximity, mouseX);
        } 
        if (mouseX >= Screen.width - minDist)
        {
            camMov += Vector3.right;
            screenBorderProximity = Mathf.Min(screenBorderProximity, Screen.width - mouseX);
        } 
        if (mouseY < minDist)
        {
            camMov += Vector3.down;
            screenBorderProximity = Mathf.Min(screenBorderProximity, mouseY);
        }
        if (mouseY >= Screen.height - minDist)
        {
            camMov += Vector3.up;
            screenBorderProximity = Mathf.Min(screenBorderProximity, Screen.height - mouseY);
        }
        float borderProximityMultiplier = Mathf.InverseLerp(minDist, 0, screenBorderProximity);

        _camera.transform.Translate(Vector3.Normalize(camMov) * (speed * Time.deltaTime) * borderProximityMultiplier);
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel")* (scrollSpeed*2) * scrollSpeed * Time.deltaTime, MinZoom, MaxZoom) ;

    }

    #endregion

    #region properties
    public static PlayerController Instance { get => _instance; }
    #endregion
}
