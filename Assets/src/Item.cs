using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public string type, subType;
    public bool onInventory = false;

    void Start()
    {
        type = "Weapon";
        subType = "Gun";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
