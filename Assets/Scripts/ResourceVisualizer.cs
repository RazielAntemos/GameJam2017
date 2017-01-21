using System;
using System.Linq;
using UnityEngine;

[SerializeField]
public enum Resources
{
    Gold = 1,
    Wood = 2,
    Sugar = 3,
    Silk = 4
}



public class ResourceVisualizer : MonoBehaviour
{
    private string _StartingResource;


    // Use this for initialization
    private void Start()
    {
        DefineResource();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.back, Time.deltaTime * 100);
    }

    private void DefineResource()
    {
        _StartingResource = typeof(Resources).GetRandomEnumValue().ToString();
        switch (_StartingResource)
        {
            case "Gold":
                GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 1.0f);
                break;

            case "Wood":
                GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1.0f);
                break;

            case "Sugar":
                GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f, 1.0f);
                break;

            case "Silk":
                GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f, 1.0f);
                break;
        }
    }
}

public static class EnumExtensions
{
    public static Enum GetRandomEnumValue(this Type t)
    {
        return Enum.GetValues(t)          // get values from Type provided
            .OfType<Enum>()               // casts to Enum
            .OrderBy(e => Guid.NewGuid()) // mess with order of results
            .FirstOrDefault();            // take first item in result
    }
}