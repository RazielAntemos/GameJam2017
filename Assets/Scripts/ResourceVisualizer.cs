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
    public GameObject[] m_ToColor;

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
        if (m_ToColor == null)
        {
            if (m_ToColor == null || m_ToColor.Length == 0)
            {
                m_ToColor = new GameObject[] { this.gameObject };
            }

        }
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.back, Time.deltaTime * 100);
    }

    private void DefineResource()
    {
        _startingResource = typeof(Resources).GetRandomEnumValue().ToString();
        foreach (var toColor in m_ToColor)
        {
            var renderer = toColor.GetComponent<Renderer>();
            Color c = Color.white;
            switch (_startingResource)
            {
                case "Gold":
                    c = Color.yellow;
                    break;

                case "Wood":
                    c = Color.green;
                    break;

                case "Sugar":
                    c = Color.red;
                    break;

                case "Silk":
                    c = Color.blue;
                    break;
            }
            renderer.material.color = c;
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