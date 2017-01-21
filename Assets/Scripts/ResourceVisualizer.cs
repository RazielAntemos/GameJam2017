using System;
using System.Linq;
using UnityEngine;

[SerializeField]
public enum Resources
{
    Gold = 1,
    Wood = 2,
    Sugar = 3,
    Silk = 4,
}

public class ResourceVisualizer : MonoBehaviour
{
    public Resources _startingResource;
    public GameObject[] m_ToColor;
    public bool m_RandomizeResource = false;
    public Resources Resource   // the Name property
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
        if (m_RandomizeResource)
        {

            _startingResource = RandomEnum<Resources>();

        }
        foreach (var toColor in m_ToColor)
        {
            var renderer = toColor.GetComponent<Renderer>();
            Color c = Color.white;
            switch (_startingResource)
            {
                case Resources.Gold:
                    c = Color.yellow;
                    break;

                case Resources.Wood:
                    c = Color.green;
                    break;

                case Resources.Sugar:
                    c = Color.red;
                    break;

                case Resources.Silk:
                    c = Color.blue;
                    break;
            }
            renderer.material.color = c;
        }
    }

    public T RandomEnum<T>()
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        new System.Random();
        return values[g_Random.Next(0, values.Length)];
    }

    static System.Random g_Random = new System.Random();
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