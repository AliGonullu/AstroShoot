using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int health;

    private static int destroyed_obs_count = 0;
    private const float extra_speed = 0.2f;
    private float obstacleSpeed = 3f;

    public void SetHealth(int _health) { health = _health; }
    public int GetHealth() { return health; }

    //public void SetObstacleSpeed(float _obstacleSpeed) { obstacleSpeed = _obstacleSpeed; }
    public float GetObstacleSpeed() {  return obstacleSpeed; }

    void FixedUpdate()
    {
        if(destroyed_obs_count > 3)
        {
            destroyed_obs_count = 0;
            obstacleSpeed += extra_speed;
        }

        Vector2 _velocity = obstacleSpeed * Time.deltaTime * Vector2.down;
        transform.position += new Vector3(_velocity.x, _velocity.y, 0);
        
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            destroyed_obs_count++;
        }

        if (health <= 0)
        {
            Destroy(gameObject, 0.2f);
            destroyed_obs_count++;
            Ball.score++;
        }
    }

}
