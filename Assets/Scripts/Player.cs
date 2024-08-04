using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
      transform.position = new Vector3(0, -4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement to player
        Movement();

        //Player fire
        Fire();
    }

    private void Movement()
    {
        //Movement player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

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

    private void Fire()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (Time.time > _canFire)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
                _canFire = Time.time + -_fireRate;
            }
        }
    }
}
