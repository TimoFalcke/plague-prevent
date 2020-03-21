using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationController : MonoBehaviour
{
    #region fields
    private static LocationController _instance;
    private static List<Location> _workspaces;
    private static List<Location> _entertainments;


    [SerializeField]
    GameObject _streetPrefab;
    [SerializeField]
    GameObject _residentalPrefab;
    [SerializeField]
    GameObject _officePrefab;
    [SerializeField]
    GameObject _factroyPrefab;
    [SerializeField]
    GameObject _hospitalPrefab;
    [SerializeField]
    GameObject _partyPrefab;
    [SerializeField]
    GameObject _gastronomyPrefab;
    [SerializeField]
    GameObject _culturePrefab;
    [SerializeField]
    GameObject _supermarketPrefab;
    [SerializeField]
    GameObject _shoppingPrefab;
    #endregion

    #region initilization
    private void Awake()
    {
        _workspaces = new List<Location>();
        _entertainments = new List<Location>();
        GetInstance();
    }
    #endregion

    #region methods
    public LocationController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }

    public void RegisterWorkspace(Location location)
    {
        _workspaces.Add(location);
    }
    public void RegisterEntertainment(Location location)
    {
        _entertainments.Add(location);
    }

    public Location GetRandomWorkplace()
    {
        return _workspaces[Random.Range(0, _workspaces.Count)];
    }
    public Location GetRandomEntertainment()
    {
        return _entertainments[Random.Range(0, _entertainments.Count)];
    }

    #endregion

    #region properties
    public static LocationController Instance { get => _instance; }
    public static List<Location> Workspaces { get => _workspaces;  }
    public static List<Location> Entertainments { get => _entertainments; }
    public GameObject StreetPrefab { get => _streetPrefab; }
    public GameObject ResidentalPrefab { get => _residentalPrefab; }
    public GameObject OfficePrefab { get => _officePrefab; }
    public GameObject FactroyPrefab { get => _factroyPrefab; }
    public GameObject HospitalPrefab { get => _hospitalPrefab;  }
    public GameObject PartyPrefab { get => _partyPrefab;  }
    public GameObject GastronomyPrefab { get => _gastronomyPrefab; }
    public GameObject CulturePrefab { get => _culturePrefab; }
    public GameObject SupermarketPrefab { get => _supermarketPrefab; }
    public GameObject ShoppingPrefab { get => _shoppingPrefab; }
    #endregion

}
