using Game.Common;
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
        private const float ammunitionCooldownCounter = 1f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
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
            var _targetRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, 30f * Time.deltaTime);
        }

        public void FireAmmunition()
        {
            var position = new Vector3(transform.position.x, transform.position.y, 0f);

            GameObject ammo = ObjectPool.SharedInstance.GetPooledObject();
            if (ammo != null)
            {
                ammo.transform.SetPositionAndRotation(position, transform.rotation);
                ammo.SetActive(true);
            }

            StartCoroutine(AmmunitionCooldownCoroutine());
        }

        private IEnumerator AmmunitionCooldownCoroutine()
        {
            yield return new WaitForSeconds(ammunitionCooldownCounter);
        }
    }
}