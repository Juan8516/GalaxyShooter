using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Triple shoot variable
    public bool canTripleShoot = false;
    public bool canFlySpeed = false;
    public bool canProtectedShield = false;

    //Lives player
    public int lives = 3;

    //Variables
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShootPrefab;    
    [SerializeField]
    private GameObject _playerAnimationExplosion;
    [SerializeField]
    private GameObject _shieldActive;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
      transform.position = new Vector3(0, -4, 0);
      _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
      
        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }
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

        if(canFlySpeed == true)
        {
            transform.Translate(Vector3.right * (_speed * 1.5f) * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * (_speed * 1.5f) * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

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
                if (canTripleShoot == true)
                {
                    //Triple Shoot
                    Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);
                    _canFire = Time.time + _fireRate;
                }
                else 
                {
                    Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
                    _canFire = Time.time + _fireRate;
                }
            }
        }
    }

    public void Demage()
    {
        if (canProtectedShield == true)
        {
            canProtectedShield = false;
            _shieldActive.gameObject.SetActive(false);
            return;
        }   
            lives--;
            _uiManager.UpdateLives(lives);

            if (lives <= 0)
            {
                Instantiate(_playerAnimationExplosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
    }

    public void SpeedIncrementedOn()
    {
        canFlySpeed = true;
        StartCoroutine(SpeedIncremented());
    }

    public void TripleShootPowerOn()
    {
        canTripleShoot = true;
        StartCoroutine(TripleShootPowerRoutine());
    }

    public void IsShieldActive()
    {
        canProtectedShield = true;
        _shieldActive.gameObject.SetActive(true);
    }

    public IEnumerator SpeedIncremented()
    {
        yield return new WaitForSeconds(5.0f);
        canFlySpeed = false;
    }

    public IEnumerator TripleShootPowerRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShoot = false;
    }

}
