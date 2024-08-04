using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Variables
    private float _speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Fire
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //Destroy fire
        DestroyFire();
    }

    private void DestroyFire()
    {
        if (transform.position.y >= 6.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
