using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int health;

    private static int destroyed_obs_count = 0;
    private static float obstacleSpeed = 2.5f;
    private readonly float extra_speed = 0.06f;

    public void SetHealth(int _health) { health = _health; }
    public int GetHealth() { return health; }

    public float GetObstacleSpeed() {  return obstacleSpeed; }


    void FixedUpdate()
    {
        if(destroyed_obs_count > 5)
        {
            destroyed_obs_count = 0;
            obstacleSpeed += extra_speed;
        }

        Vector2 _velocity = obstacleSpeed * Time.deltaTime * Vector2.down;
        transform.position += new Vector3(_velocity.x, _velocity.y, 0);
        
        if (transform.position.y < -15)
        {
            Destroy(gameObject);
            destroyed_obs_count += 1;
        }

        if (health <= 0)
        {
            Destroy(gameObject, 0.1f);
            destroyed_obs_count += 1;
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
