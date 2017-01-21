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
    private string _startingResource;
  
  
    public string Resource   // the Name property
    {
        get
        {
            return _startingResource;
        }
    }

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
        _startingResource = typeof(Resources).GetRandomEnumValue().ToString();
        switch (_startingResource)
        {
            case "Gold":
                GetComponent<Renderer>().material.color = Color.yellow ;
                break;

            case "Wood":
                GetComponent<Renderer>().material.color = Color.green ;
                break;

            case "Sugar":
                GetComponent<Renderer>().material.color = Color.red;
                break;

            case "Silk":
                GetComponent<Renderer>().material.color = Color.blue;
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