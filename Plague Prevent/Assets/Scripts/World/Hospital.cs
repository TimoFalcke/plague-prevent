using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : Location
{
    [SerializeField]
    int _maxCapacity;
    int _currentCapacity;

    public bool HasCapacity()
    {
        if (_maxCapacity > _currentCapacity)
        {
            return true;
        }
        else return false;
    }

    public int MaxCapacity { get => _maxCapacity; }
    public int CurrentCapacity { get => _currentCapacity; }
}
