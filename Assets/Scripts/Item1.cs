using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1 : Accionable
{
    public override void Usame()
    {
        Debug.Log("Item010");
        Debug.Log(this.cantidad);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
