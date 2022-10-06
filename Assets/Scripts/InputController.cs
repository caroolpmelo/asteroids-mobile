using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidBody.gravityScale = 0;

        if (rotationSpeed == 0)
        {
            rotationSpeed = 100f;
        }
    }

    private void FixedUpdate()
    {
        // TODO: change input axis to touch
        transform.Rotate(
            0,
            0,
            -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        rigidBody.AddForce(transform.up * Input.GetAxis("Vertical"));
    }
}
