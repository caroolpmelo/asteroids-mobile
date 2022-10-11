using UnityEngine;

namespace Game.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AmmunitionController : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private float autoDestroyTimer;

        private Rigidbody2D rigidBody;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();

            if (speed == 0)
            {
                speed = 400f;
            }

            if (autoDestroyTimer == 0)
            {
                autoDestroyTimer = 1f;
            }
        }

        private void Start()
        {
            rigidBody.gravityScale = 0;

            rigidBody.AddForce(transform.up * speed);

            Destroy(gameObject, autoDestroyTimer);
        }
    }
}
