using Game.Managers;
using UnityEngine;

public class EuclideanTorusController : MonoBehaviour
{
    private float width;
    private float height;

    private void Start()
    {
        var rectTransform = 
            GameManager.Instance.CanvasTransform.GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
    }

    private void Update()
    {
        CheckBounds();
    }

    private void CheckBounds()
    {
        if (transform.position.x > width)
        {
            transform.position = new Vector3(-width, transform.position.y);
        }
        else if (transform.position.x < -width)
        {
            transform.position = new Vector3(width, transform.position.y);
        }
        else if (transform.position.y > height)
        {
            transform.position = new Vector3(transform.position.x, -height);
        }
        else if (transform.position.y < -height)
        {
            transform.position = new Vector3(transform.position.x, height);
        }
    }
}
