using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour
{
    private Rigidbody2D rigidBody;

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
}
