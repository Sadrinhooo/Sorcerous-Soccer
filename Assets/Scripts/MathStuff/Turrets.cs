using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turret
{
    Turret1,Turret2
}
public class Turrets : MonoBehaviour
{

    [SerializeField] public Turret currentTurret;
    [SerializeField] private float a;
    [SerializeField] private float b;
    [SerializeField] private float c;
    private float x1;
    private float x2;

    //private Vector2 turret1Position = new Vector2(x1, 0);
    //private Vector2 turret2Position = new Vector2(x2, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
