using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

    // Movement boundaries
    [SerializeField]
    private float MIN_X = -9.4f;
    [SerializeField]
    private float MAX_X = 9.4f;
    [SerializeField]
    private float MIN_Y = -4.5f;
    [SerializeField]
    private float MAX_Y = 0.0f;


    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -4, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
      
        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>(); 

        if(_spawnManager != null)
        {
            _spawnManager.StarSpawnRoutines();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Movement to player
        Movement();

        //Player fire
        Fire();
        
        // Obt�n la direcci�n de entrada y mueve al jugador.
        Vector3 direction = GetInputDirection();
        MovePlayer(direction);
    }

    private void Movement()
    {

        // Obt�n la direcci�n de entrada
        Vector3 inputDirection = GetInputDirection();

        // Mueve al jugador
        MovePlayer(inputDirection);

        // Mant�n al jugador dentro de los l�mites
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
            Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
            transform.position.z
        );
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
                _gameManager.gameOver = false;
                _uiManager.ShowTitleScreen();
                Destroy(this.gameObject);
            }
    }

    // Moviemiento nave
    private Vector3 GetInputDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        return new Vector3(horizontalInput, verticalInput, 0);
    }

    private void MovePlayer(Vector3 direction)
    {
        float adjustedSpeed = canFlySpeed ? _speed * 1.5f : _speed;

        transform.Translate(direction * adjustedSpeed * Time.deltaTime);

        // Mantener al jugador dentro de los l�mites.
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
            Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
            transform.position.z
        );
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
