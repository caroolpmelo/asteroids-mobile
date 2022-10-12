using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipController : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float rotationSpeed;

        private float thrustForce = 3f;
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

            if (moveSpeed == 0)
            {
                moveSpeed = 5f;
            }
            if (rotationSpeed == 0)
            {
                rotationSpeed = 5f;
            }
        }

        public void MoveShip(Transform joystickThumb)
        {
            var rectTransform = joystickThumb.GetComponent<RectTransform>();
            Vector3 worldPoint = joystickThumb.TransformPoint(
                new Vector3(rectTransform.rect.width, rectTransform.rect.height));
            transform.Rotate(0, 0, worldPoint.x/2 * rotationSpeed * Time.deltaTime);
            rigidBody.AddForce(transform.up * thrustForce);
        }

        public void FireAmmunition()
        {
            var position = new Vector3(transform.position.x, transform.position.y, 0f);
            var ammo = Instantiate(ammunation, position, transform.rotation, sceneObjectsTransform);
            StartCoroutine(AmmunitionCooldownCoroutine());
        }

        private IEnumerator AmmunitionCooldownCoroutine()
        {
            yield return new WaitForSeconds(1f);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // everything is asteroid, except bullet
            if (other.gameObject.tag == "Enemy")
            {
                // ship in center
                transform.position = Vector3.zero;

                // remove ship velocity
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                GameManager.Instance.DecrementLives();
            }
        }
    }
}