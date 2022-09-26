using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPickup : MonoBehaviour
{

    public float speed = 10;
    private float maxY = -5.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);   
        if (transform.position.y < maxY)
        {
            Destroy(gameObject);
        }
    }
   
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ShipModel")
        {
            Game.BonusPickupScore();
            Destroy(gameObject);
        }
    }
}
