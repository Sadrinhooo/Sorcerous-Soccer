using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class myClass{

    [NonSerialized] public int id;
    public string myString;
}
public class Math : MonoBehaviour
{
    public myClass data;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(3.25E+6 - 1.05E+5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
