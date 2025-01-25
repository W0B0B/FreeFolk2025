using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float speed = 100;
    
    public GameObject yourgameobject;
    
    void Update()
    {
          yourgameobject.transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}