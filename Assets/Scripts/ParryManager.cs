using UnityEngine;

public class ParryManager : MonoBehaviour
{

    private bool canParry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartParryWindow() 
    { 
        canParry = true;
    }

    public void EndParryWindow()
    {
        canParry = false;
    }

    public bool GetCanParry() {  return canParry; }

}
