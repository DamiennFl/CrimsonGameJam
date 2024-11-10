using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            FallingPlatform.Instance.StartCoroutine("SpawnPlatform",new Vector2(transform.position.x, transform.position.y));
            Invoke("DropPlatform", 0.5f);
            Destroy(gameObject, 2f);
        }
    }
    void DropPlatform()
    {
        rb.isKinematic = false;
    }
}
