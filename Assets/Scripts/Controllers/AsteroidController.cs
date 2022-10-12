using Game.Managers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public bool isBig = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidBody.gravityScale = 0;

        rigidBody.AddForce(transform.up * Random.Range(-50.0f, 150.0f));
        rigidBody.angularVelocity = Random.Range(-0.0f, 90.0f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            Destroy(other.gameObject); // destroy bullet

            // large asteroid spawn new ones
            if (tag.Equals("Enemy") && isBig)
            {
                Destroy(this);
                SpawnSmallAsteroids();
            }

            GameManager.Instance.IncrementScore();

            Destroy(gameObject); // destroy current asteroid
        }
    }

    private void SpawnSmallAsteroids()
    {
        GameManager.Instance.SpawnSmallerAsteroids(transform.position);
    }
}
