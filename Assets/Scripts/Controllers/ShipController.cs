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
                rotationSpeed = 5f;
            }
        }

        public void MoveShip(Transform joystickThumb)
        {
            var rectTransform = joystickThumb.GetComponent<RectTransform>();
            transform.Rotate(0, 0, rectTransform.rect.x * rotationSpeed * Time.deltaTime);
            rigidBody.AddForce(transform.up * rectTransform.rect.y);
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
            yield return new WaitForSeconds(1f);
        }
    }
}