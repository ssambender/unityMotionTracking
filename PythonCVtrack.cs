using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PythonCVtrack : MonoBehaviour
{
    public GameObject particleObj;
    public float reduce = 100;
    public float adjustX = 0;
    public float adjustY = 0;

    public UDPReceive udpRecieve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string cvData = udpRecieve.data;
        cvData = cvData.Remove(0, 1);
        cvData = cvData.Remove(cvData.Length - 1, 1);
        string[] coords = cvData.Split(',');

        //coords[0] = tracked hand x position
        //coords[1] = tracked hand y position

        // Mapping x and y values
        float x = Map(float.Parse(coords[0]), 100f, 1200f, -9f, 9f) + adjustX;
        float y = Map(float.Parse(coords[1]), 0f, 600f, -5f, 5f) + adjustY;
        float z = 0;

        particleObj.transform.localPosition = new Vector3(x, y, z);
    }

    // Function to map values from one range to another
    float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
