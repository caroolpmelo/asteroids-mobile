using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipController : MonoBehaviour
    {
        [SerializeField]
        private float shipSpeed;
        [SerializeField]
        private float rotationSpeed;
        [SerializeField]
        private FixedJoystick joystick;

        private float verticalInput = 0f, horizontalInput = 0f;

        private Rigidbody2D rigidBody;
        private GameObject ammunation;
        private Transform sceneObjectsTransform;

        private const float ammunitionCooldownCounter = 1f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            ammunation = GameManager.Instance.ShipAmmunation;
            sceneObjectsTransform = GameManager.Instance.SceneObjects.transform;


            rigidBody.gravityScale = 0;

            if (shipSpeed == 0)
            {
                shipSpeed = 5f;
            }

            if (rotationSpeed == 0)
            {
                rotationSpeed = 5f;
            }
        }

        private void Update()
        {
            MoveShip();
        }

        private void MoveShip()
        {
            if (!joystick)
            {
                Debug.LogError("Joystick reference not found!");
                return;
            }

            verticalInput = joystick.Vertical;
            horizontalInput = joystick.Horizontal;
            if (verticalInput != 0 || horizontalInput != 0)
            {
                var direction = new Vector3(horizontalInput, 0, verticalInput);
                rigidBody.velocity = new Vector3(horizontalInput * shipSpeed, verticalInput * shipSpeed,
                    rigidBody.velocity.y);

                if (direction != Vector3.zero)
                {
                    RotateShip();
                }
            }
            else
            {
                rigidBody.velocity = Vector3.zero;
            }
        }

        private void RotateShip()
        {
            float rotationZ = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }

        public void FireAmmunition()
        {
            var position = new Vector3(transform.position.x, transform.position.y, 0f);
            var ammo = Instantiate(ammunation, position, transform.rotation, sceneObjectsTransform);
            // if hit Destroy()
            StartCoroutine(AmmunitionCooldownCoroutine());
        }

        private IEnumerator AmmunitionCooldownCoroutine()
        {
            yield return new WaitForSeconds(ammunitionCooldownCounter);
        }
    }
}