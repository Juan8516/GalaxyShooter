using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class EnemyIA : MonoBehaviour
{
    public float speed = 4.0f;
    public GameObject prefabEnemy;

    [SerializeField]
    private GameObject _enemyAnimation;
    [SerializeField]
    private GameObject _playerAnimationExplotion;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 6, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if(transform.position.y < -10)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        Laser laser = other.GetComponent<Laser>();

        if(other.tag == "Player")
        {
            player.lives--;

            if(player.lives <= 0)
            {
                Instantiate(_playerAnimationExplotion, transform.position, Quaternion.identity);
                Destroy(other.gameObject);

            }else if(other.tag == "Player")
            {
                Instantiate(_enemyAnimation, transform.position, Quaternion.identity);
            }
        }

        if(other.tag == "Laser")
        {
            Instantiate(_enemyAnimation, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
