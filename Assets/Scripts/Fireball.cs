using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float startHeight;
    [SerializeField] float endHeight;

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed;

        if (transform.position.y <= endHeight) transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);
    }
}
