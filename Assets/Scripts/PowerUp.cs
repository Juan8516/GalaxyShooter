using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private int _powerId; //Triple shoot 0, Power Speed 1

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed *Time.deltaTime);
        
        //Destroy object out screen
        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Access to class player
        Player player = other.GetComponent<Player>();

        if (other.tag == "Player")
        {
            if (player != null)
            {
                //Enable Speed
                if (_powerId == 0)
                {
                    //Enable tripleshoot
                    player.TripleShootPowerOn();
                }
                else if (_powerId == 1)
                {
                    //speed incremented
                    player.SpeedIncrementedOn();
                }
                else if (_powerId == 2)
                {
                    //Active shield
                    player.IsShieldActive();
                }
            }
            //Destroy
            Destroy(this.gameObject);
        }
    }
}
