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
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            Instantiate(_enemyAnimation, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Demage();
            }

            Instantiate(_enemyAnimation, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
