using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float obstacleSpeed = 3.1f, extra_speed = 0.15f;
    private static int destroyed_obs_count = 0;

    public void SetObstacleSpeed(float _obstacleSpeed) { obstacleSpeed = _obstacleSpeed; }
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
            
    }
}
