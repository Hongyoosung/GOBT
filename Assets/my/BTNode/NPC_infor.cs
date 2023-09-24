using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_infor : MonoBehaviour
{
    public int Money;
    public bool isHungry;
    public bool isTired;
    void Start()
    {
        Money = 0;
        isHungry=false;
        isTired=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
