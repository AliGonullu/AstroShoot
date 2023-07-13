using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int health;

    public void SetHealth(int _health) { health = _health; }
    public int GetHealth() { return health; }

    void FixedUpdate()
    {
        if(Variables.destroyed_obstacle_count > 5)
        {
            Variables.destroyed_obstacle_count = 0;
            Variables.obstacle_speed += Variables.obstacle_extra_speed;
        }

        Vector2 _velocity = Variables.obstacle_speed * Time.deltaTime * Vector2.down;
        transform.position += new Vector3(_velocity.x, _velocity.y, 0);
        
        if (transform.position.y < -15)
        {
            Destroy(gameObject);
            Variables.destroyed_obstacle_count += 1;
        }

        if (health <= 0)
        {
            Destroy(gameObject, 0.1f);
            Variables.destroyed_obstacle_count += 1;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7 || collision.gameObject.name.StartsWith("Obstacle") || collision.gameObject.name.StartsWith("Gold"))
        {
            Destroy(collision.gameObject);
        }
    }

}
