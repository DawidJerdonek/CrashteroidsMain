using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    public float speed = 8;
    private float maxY = -5.0f;

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
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y < maxY)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ShipModel")
        {
            
            Destroy(gameObject);
        }
    }
}
