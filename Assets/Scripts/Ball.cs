using TMPro;
using UnityEngine;  

public class Ball : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D material;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private SceneMNG scene_ref;

    public static int score = 0, first_health = 2;

    private static int throwForceLVL = 0, health = first_health, health_level = 0;
    private readonly float friction = 0.32f, force = 17f, def_bounciness = 0.1f;
    private int score_multiplier = 1;
    private SpriteRenderer _renderer;
    private bool ballTaken = false;
    private Player player = null;
    private Vector3 first_size;
    private Rigidbody2D rb;
    
    
    public bool GetBallTaken(){return ballTaken;}

    //public int GetHealth() { return health; }
    public void SetHealth(int _new_value) { health = _new_value; }

    public int GetHealthLVL() { return health_level; }
    public void SetHealthLVL(int _new_value) { health_level = _new_value; }


    public int GetThrowForceLVL() { return throwForceLVL; }
    public void SetThrowForceLVL(int _new_value) { throwForceLVL = _new_value; }

    private void ScoreChanged(int _new_value) 
    {
        score = _new_value;
        scoreText.text = score.ToString();
    }

    void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        health += health_level;
        first_size = transform.localScale;
        scoreText.text = score.ToString();
        material.bounciness = def_bounciness;
        BallColorHandling();
    }

    void FixedUpdate()
    {
        
        material.bounciness = (ballTaken == true) ? 0f : def_bounciness;
 
        if (transform.position.y < -9)
        {
            scene_ref.OpenGameOverMenu();
        }

        if (rb.velocity != Vector2.zero)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, friction * Time.deltaTime);
        }

        if (player != null)
        { 
            if (player.GetKick())
            {
                float level_benefit = (throwForceLVL * 1.45f);
                rb.velocity = Vector2.Lerp(rb.velocity, (player.transform.position - transform.position) * -(force + level_benefit), ((force + 20) + level_benefit) * Time.deltaTime);
                player = null;
                ballTaken = false;
            }
            else
            {
                ballTaken = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacle obs = collision.gameObject.GetComponent<Obstacle>();
        if (collision.gameObject.name.StartsWith("FireWall") && !ballTaken)
        {
            health = Mathf.Clamp(health - 1, 0, health);
            if (health <= 0)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
                scene_ref.OpenGameOverMenu();
            }
            transform.localScale = first_size;
        }
        if (collision.gameObject.name.StartsWith("Player1"))
        {
            player = collision.gameObject.GetComponent<Player>();
            player.SetKick(false);
            material.bounciness = def_bounciness;
            score_multiplier = 1;
            transform.localScale = first_size;
        }
        else if (collision.gameObject.name.StartsWith("Obstacle") || collision.gameObject.name.StartsWith("Gold"))
        {
            if (!ballTaken)
            {
                obs.SetHealth(obs.GetHealth() - 1);
                ScoreChanged(score + score_multiplier);
                score_multiplier += 1;
                material.bounciness += 0.05f;
                rb.velocity *= 1.01f;
                transform.localScale *= 1.04f;
            }
        }
        else if (collision.gameObject.name.StartsWith("Material"))
        {
            if (!ballTaken)
            {
                obs.SetHealth(obs.GetHealth() - 1);
                PerkHandler.materials += 1;
                material.bounciness += 0.05f;
                rb.velocity *= 1.01f;
                transform.localScale *= 1.04f;
            }
        }
        else if (collision.gameObject.name.StartsWith("Fix"))
        {
            if (!ballTaken)
            {
                obs.SetHealth(obs.GetHealth() - 1);
                health = Mathf.Clamp(health + 1, 0, 3);
                material.bounciness += 0.05f;
                rb.velocity *= 1.01f;
                transform.localScale *= 1.04f;
            }
        }
        else if (collision.gameObject.name.StartsWith("PlusTen"))
        {
            if (!ballTaken)
            {
                obs.SetHealth(obs.GetHealth() - 1);
                ScoreChanged(score + 10);
                material.bounciness += 0.05f;
                rb.velocity *= 1.01f;
                transform.localScale *= 1.04f;
            }
        }
        else
        {
            transform.localScale = first_size;
            material.bounciness -= 0.03f;
        }

        BallColorHandling();
    }

    private void BallColorHandling()
    {
        if (health == 3)
            _renderer.material.color = Color.green;
        else if (health == 2)
            _renderer.material.color = Color.yellow;
        else if (health == 1)
            _renderer.material.color = Color.red;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Player1"))
        {
            player = collision.gameObject.GetComponent<Player>();
            player.SetKick(false);
        }
    }

}
