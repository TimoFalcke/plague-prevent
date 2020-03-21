using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField]
    Texture2D _map;
    [SerializeField]
    GameObject _streetContainer;
    [SerializeField]
    GameObject _locationContainer;
    [SerializeField]
    float _distanceFactor;

    Color32 _streetColor = new Color32(255, 255, 255, 255);
    Color32 _residentalColor = new Color32(0,138,255,255);
    Color32 _officeColor = new Color32(0, 4, 255, 255);
    Color32 _factoryColor = new Color32(106, 0, 199, 255);
    Color32 _hospitalColor = new Color32(65, 119, 0, 255);
    Color32 _garstronmyColor = new Color32(255, 115, 0, 255);
    Color32 _partyColor = new Color32(255, 0, 42, 255);
    Color32 _cultureColor = new Color32(255, 0, 255, 255);
    Color32 _superMarketColor = new Color32(206, 192, 0, 255);
    Color32 _shoppingColor = new Color32(20, 202, 0, 255);

    public void Awake()
    {
        GenerateMap();
    }
    public void GenerateMap()
    {

        for (int x = 0; x < 64; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                Color32 color = _map.GetPixel(x, y);

                if (color.r == _streetColor.r && color.g == _streetColor.g && color.b == _streetColor.b)
                {
                   GameObject.Instantiate(LocationController.Instance.StreetPrefab, new Vector3(x* _distanceFactor,y*_distanceFactor,0), Quaternion.identity, _streetContainer.transform);
                }

                if (color.r == _officeColor.r && color.g == _officeColor.g && color.b == _officeColor.b)
                {
                    GameObject.Instantiate(LocationController.Instance.OfficePrefab, new Vector3(x * _distanceFactor, y * _distanceFactor, 0), Quaternion.identity, _locationContainer.transform);
                }

                if (color.r == _factoryColor.r && color.g == _factoryColor.g && color.b == _factoryColor.b)
                {
                    GameObject.Instantiate(LocationController.Instance.FactroyPrefab, new Vector3(x * _distanceFactor, y * _distanceFactor, 0), Quaternion.identity, _locationContainer.transform);
                }

                if (color.r == _hospitalColor.r && color.g == _hospitalColor.g && color.b == _hospitalColor.b)
                {
                    GameObject.Instantiate(LocationController.Instance.HospitalPrefab, new Vector3(x * _distanceFactor, y * _distanceFactor, 0), Quaternion.identity, _locationContainer.transform);
                }

                if (color.r == _garstronmyColor.r && color.g == _garstronmyColor.g && color.b == _garstronmyColor.b)
                {
                    GameObject.Instantiate(LocationController.Instance.GastronomyPrefab, new Vector3(x * _distanceFactor, y * _distanceFactor, 0), Quaternion.identity, _locationContainer.transform);
                }

                if (color.r == _partyColor.r && color.g == _partyColor.g && color.b == _partyColor.b)
                {
                    GameObject.Instantiate(LocationController.Instance.PartyPrefab, new Vector3(x * _distanceFactor, y * _distanceFactor, 0), Quaternion.identity, _locationContainer.transform);
                }

                if (color.r == _cultureColor.r && color.g == _cultureColor.g && color.b == _cultureColor.b)
                {
                    GameObject.Instantiate(LocationController.Instance.CulturePrefab, new Vector3(x * _distanceFactor, y * _distanceFactor, 0), Quaternion.identity, _locationContainer.transform);
                }

                if (color.r == _superMarketColor.r && color.g == _superMarketColor.g && color.b == _superMarketColor.b)
                {
                    GameObject.Instantiate(LocationController.Instance.SupermarketPrefab, new Vector3(x * _distanceFactor, y * _distanceFactor, 0), Quaternion.identity, _locationContainer.transform);
                }

                if (color.r == _shoppingColor.r && color.g == _shoppingColor.g && color.b == _shoppingColor.b)
                {
                    GameObject.Instantiate(LocationController.Instance.ShoppingPrefab, new Vector3(x * _distanceFactor, y * _distanceFactor, 0), Quaternion.identity, _locationContainer.transform);
                }
            }
        }


        for (int x = 0; x < 64; x++)
        {
            for (int y = 0; y < 64; y++)
            {
                Color32 color = _map.GetPixel(x, y);


                if (color.r == _residentalColor.r && color.g == _residentalColor.g && color.b == _residentalColor.b)
                {
                    GameObject.Instantiate(LocationController.Instance.ResidentalPrefab, new Vector3(x * _distanceFactor, y * _distanceFactor, 0), Quaternion.identity, _locationContainer.transform);
                }

            }
        }


    }


}
