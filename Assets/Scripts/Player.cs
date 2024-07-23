using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    [SerializeField]
    private float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
      transform.position = new Vector3(0, -4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //Movement player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        //Limits player in Y
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.5)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }

        //Limits player in X
        if (transform.position.x > 9.4)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }
}
