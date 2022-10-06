using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private GameObject ammunition;

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

        if (Input.GetMouseButton(0))
        {
            FireAmmunition();
        }
    }

    private void FireAmmunition()
    {
        var position = new Vector3(transform.position.x, transform.position.y, 0f);
        var ammo = Instantiate(ammunition, position, transform.rotation);
        // if outofbounds Destroy()
        // if hit Destroy()
        StartCoroutine(AmmunitionCooldownCoroutine());
    }

    private IEnumerator AmmunitionCooldownCoroutine()
    {
        yield return new WaitForSeconds(2f);
    }
}
