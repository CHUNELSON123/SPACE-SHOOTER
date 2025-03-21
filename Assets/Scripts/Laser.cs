using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int _speed = 8;
   
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);

        if(transform.position.y >= 7.4f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
