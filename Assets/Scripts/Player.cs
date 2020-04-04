using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 movement = new Vector2();
        movement.x = speed * Time.fixedDeltaTime * Input.GetAxis("Horizontal");
        movement.y = speed * Time.fixedDeltaTime * Input.GetAxis("Vertical");
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        rb2d.MovePosition(pos + movement);
    }
}
