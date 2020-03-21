using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    #region fields
    static string[] _gerNamesMale = { "Hans", "Peter", "Torben", "Dennis" };
    static string[] _gerNamesFemale = { "Julia", "Anke", "Lea", "Elena" };
    static string[] _gerSurNames = { "Müller", "Baecker", "Ewald" };

    static string[] _itaNamesMale = { "Marco", "Alessandro", "Giuseppe", "Flavio" };
    static string[] _itaNamesFemale = { "Maria", "Anna", "Valentina", "Rosa" };
    static string[] _itaSurNames = { "Russo", "Esposito", "Bianchi" };

    static string[] _spainNamesMale = { "Lucas", "Leo", "Miguel", "Adrian" };
    static string[] _spainNamesFemale = { "Laia", "Laura", "Clara", "Celia" };
    static string[] _spainSurNames = { "Lopez", "Garcia", "Perez" };
    #endregion

    #region methods
    static public string GenerateName(Gender gender, Origin origin)
    {
        string name = "";
        switch (gender)
        {
            case Gender.FEMALE:
                name = GetFemaleName(origin);
                break;
            case Gender.MALE:
                name = GetMaleName(origin);
                break;
        }

        return name;
    }

    static public string GetFemaleName(Origin origin)
    {
        string name = "";
        switch (origin)
        {
            case Origin.GERMANY:
                name += _gerNamesFemale[Random.Range(0, _gerNamesFemale.Length)];
                name += _gerSurNames[Random.Range(0, _gerSurNames.Length)];
                break;
            case Origin.ITALY:
                name += _itaNamesFemale[Random.Range(0, _itaNamesFemale.Length)];
                name += _itaSurNames[Random.Range(0, _itaSurNames.Length)];
                break;
            case Origin.SPAIN:
                name += _spainNamesFemale[Random.Range(0, _spainNamesFemale.Length)];
                name += _spainSurNames[Random.Range(0, _spainSurNames.Length)];
                break;

        }
        return name;
    }

    static public string GetMaleName(Origin origin)
    {
        string name = "";
        switch (origin)
        {
            case Origin.GERMANY:
                name += _gerNamesMale[Random.Range(0, _gerNamesMale.Length)];
                name += _gerSurNames[Random.Range(0, _gerSurNames.Length)];
                break;
            case Origin.ITALY:
                name += _itaNamesMale[Random.Range(0, _itaNamesMale.Length)];
                name += _itaSurNames[Random.Range(0, _itaSurNames.Length)];
                break;
            case Origin.SPAIN:
                name += _spainNamesMale[Random.Range(0, _spainNamesMale.Length)];
                name += _spainSurNames[Random.Range(0, _spainSurNames.Length)];
                break;

        }
        return name;
    }

    private static void GetRandomString(string[] names)
    {

    }
    #endregion
}
