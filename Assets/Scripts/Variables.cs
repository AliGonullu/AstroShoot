using UnityEngine;

public class Variables : MonoBehaviour
{
    //ShipVariables
    private const int ship_first_health = 3;
    public static int 
        ship_engine_effect_idx = 0, 
        ship_speed_lvl = 1, 
        ship_no = 1, 
        ship_health = ship_first_health;

    public static void ResetShipHealth()
    {
        ship_health = ship_first_health;
    }


    //BallVariables
    private const int ball_first_health = 2;
    public static int 
        score = 0, 
        ball_max_health = 3, 
        throw_force_lvl = 1,
        ball_health = ball_first_health, 
        ball_health_level = 0;

    public static float 
        ball_friction = 0.25f, 
        ball_force = 17f,
        ball_bounciness_bonus = 0.08f,
        default_ball_bounciness = 0.15f;

    public static void ResetBallHealth()
    {
        ball_health = ball_first_health;
    }



    //ObstacleVariables
    private const float obstacle_first_speed = 2.5f;

    public static float 
        obstacle_speed = obstacle_first_speed, 
        obstacle_extra_speed = 0.06f;

    public static int destroyed_obstacle_count = 0;

    public static void ResetObstacleSpeed()
    {
        obstacle_speed = obstacle_first_speed;
        destroyed_obstacle_count = 0;
    }

}
