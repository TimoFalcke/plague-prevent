using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    #region fields
    private static PlayerController _instance;
    
    [SerializeField]public float scrollSpeed = 300.0f;

    public float minDist = 30.0f;

    private const float MaxZoom = 500.0f;

    private const float MinZoom = 1.0f;

    private Vector3 _initMousePos;
    [SerializeField] private float dragSpeed = -14f;

    [SerializeField]
    private float _moveSpeed;

    private Camera _camera;

    public float minCameraX;
    public float maxCameraX;
    public float minCameraY;
    public float maxCameraY;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
        _camera = Camera.main;
        _initMousePos = Input.mousePosition;
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
        float speed = (_camera.orthographicSize/MaxZoom) * scrollSpeed;
        Vector3 camMov = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            camMov += HandleKeyMovement();
        }
        else
        {
            //Camera Movement with Mouse on Window Border
            camMov += HandleMouseMovement();
            speed *= dragSpeed;
        }
        
        _camera.transform.Translate(Vector3.Normalize(camMov) * (speed * Time.unscaledDeltaTime));
        float x = Mathf.Clamp(_camera.transform.position.x, minCameraX, maxCameraX);
        float y = Mathf.Clamp(_camera.transform.position.y, minCameraY, maxCameraY);
        _camera.transform.position = new Vector3(x,y,-10);
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel")* (scrollSpeed*2) * scrollSpeed * Time.unscaledDeltaTime, MinZoom, MaxZoom) ;
    }
    
    
    //private 

    private Vector3 HandleKeyMovement()
    {
        Vector3 camMov = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            camMov += Vector3.left * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            camMov += Vector3.right * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            camMov += Vector3.down * _moveSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            camMov += Vector3.up * _moveSpeed;
        }

        return camMov;
    }

    private Vector3 HandleMouseMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        {
            
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 ret = _camera.ScreenToViewportPoint(Input.mousePosition - _initMousePos);
            _initMousePos = Input.mousePosition;
            return ret;
        }
        else
        {
            return Vector3.zero;
        }

    }

    private void HandleTimeScale()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Time.timeScale = 10;
        }
    }

    private void infectRandom()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            var p = GameObject.Find("Agents");
            if (p)
            {
                InfectionStateMachine[] a = p.GetComponentsInChildren<InfectionStateMachine>();
                var aa = a[Random.Range(0, a.Length)];
                aa.OnContact(1.0f);
            }
        }
    }

    #endregion

    #region properties
    public static PlayerController Instance { get => _instance; }
    #endregion
}
