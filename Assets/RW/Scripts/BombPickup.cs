using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    public float speed = 8;
    private float maxY = -100.0f;

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
            Debug.Log("starting Speed corou");
            StartCoroutine(SlowPlayer());
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }


    IEnumerator SlowPlayer()
    {
        Debug.Log("Lower Speed");
        Game.PlayerSpeedLower();
        yield return new WaitForSecondsRealtime(4);
        Game.PlayerSpeedHigher();
        Debug.Log("Higehr Speed");
        Destroy(gameObject);
    }
}
