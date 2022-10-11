using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipController : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed;

        private Rigidbody2D rigidBody;
        private GameObject ammunation;
        private Transform sceneObjectsTransform;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            ammunation = GameManager.Instance.ShipAmmunation;
            sceneObjectsTransform = GameManager.Instance.SceneObjects.transform;


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
            var ammo = Instantiate(ammunation, position, transform.rotation, sceneObjectsTransform);
            // if hit Destroy()
            StartCoroutine(AmmunitionCooldownCoroutine());
        }

        private IEnumerator AmmunitionCooldownCoroutine()
        {
            yield return new WaitForSeconds(1f);
        }
    }
}