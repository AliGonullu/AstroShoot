using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SoundEffect _soundEffect;

    [SerializeField] private ScreenShake screenShake;
    [SerializeField] private PhysicsMaterial2D material;
    [SerializeField] private TextMeshProUGUI scoreText, scoreMtpText;
    [SerializeField] private SceneMNG scene_ref;

    private int score_multiplier = 1;
    private bool ballTaken = false;

    private SpriteRenderer _renderer;
    private Player player = null;
    private Vector3 first_size = Vector3.zero;
    private Rigidbody2D rb;
    
    public bool GetBallTaken(){return ballTaken;}

    private void ScoreChanged(int _new_value) 
    {
        Variables.score = _new_value;
        scoreText.text = Variables.score.ToString();
    }

    void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _soundEffect = GetComponentInChildren<SoundEffect>();
        rb = GetComponent<Rigidbody2D>();

        Variables.ball_health += Variables.ball_health_level;
        first_size = transform.localScale;
        scoreText.text = Variables.score.ToString();
        ScoreMultiplierChange(score_multiplier);
        Variables.default_ball_bounciness += (Variables.throw_force_lvl / 100);
        material.bounciness = Variables.default_ball_bounciness;
        BallColorHandling();
    }

    void FixedUpdate()
    {
        if (ballTaken)
        {
            material.bounciness = 0f;
        }
        else
        {
            material.bounciness = Variables.default_ball_bounciness;
        }
 
        if (transform.position.y < -6.68f)
        {
            scene_ref.OpenGameOverMenu();
        }
            

        if (rb.velocity != Vector2.zero)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Variables.ball_friction * Time.deltaTime);
        }
            

        if (player != null)
        { 
            if (player.GetKick())
            {
                float level_benefit = Variables.throw_force_lvl * (Variables.ship_no / 5.0f);
                rb.velocity = Vector2.Lerp(rb.velocity, (player.transform.position - transform.position) * -(Variables.ball_force + level_benefit), (Variables.ball_force + 20 + level_benefit) * Time.deltaTime);
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
            Variables.ball_health = Mathf.Clamp(Variables.ball_health - 1, 0, Variables.ball_health);
            if (Variables.ball_health <= 0)
            {
                gameObject.SetActive(false);
                scene_ref.OpenGameOverMenu();
            }
            else
            {
                _soundEffect.MetalSoundEffect();
                StartCoroutine(screenShake.Shake(0.2f, 0.5f));
            }
            transform.localScale = first_size;
        }
        if (collision.gameObject.name.StartsWith("Player1"))
        {
            player = collision.gameObject.GetComponent<Player>();
            player.SetKick(false);
            material.bounciness = Variables.default_ball_bounciness;
            //score_multiplier = 1;
            ScoreMultiplierChange(1);
            transform.localScale = first_size;
        }
        else if (collision.gameObject.name.StartsWith("Obstacle") || collision.gameObject.name.StartsWith("Gold"))
        {
            if (!ballTaken)
            {
                _soundEffect.PopSoundEffect();

                obs.SetHealth(obs.GetHealth() - 1);
                ScoreChanged(Variables.score + score_multiplier);
                //score_multiplier += 1;
                ScoreMultiplierChange(score_multiplier + 1);
                material.bounciness += Variables.ball_bounciness_bonus;
                rb.velocity *= 1.01f;
                transform.localScale *= 1.04f;
            }
        }
        else if (collision.gameObject.name.StartsWith("Material"))
        {
            if (!ballTaken)
            {
                _soundEffect.PopSoundEffect();
                
                obs.SetHealth(obs.GetHealth() - 1);
                PerkHandler.materials += 1;
                material.bounciness += Variables.ball_bounciness_bonus;
                rb.velocity *= 1.01f;
                transform.localScale *= 1.04f;
            }
        }
        else if (collision.gameObject.name.StartsWith("Fix"))
        {
            if (!ballTaken)
            {
                _soundEffect.PopSoundEffect();

                obs.SetHealth(obs.GetHealth() - 1);
                Variables.ball_health = Mathf.Clamp(Variables.ball_health + 1, 0, Variables.ball_max_health);
                material.bounciness += Variables.ball_bounciness_bonus;
                rb.velocity *= 1.01f;
                transform.localScale *= 1.04f;
            }
        }
        else if (collision.gameObject.name.StartsWith("PlusTen"))
        {
            if (!ballTaken)
            {
                _soundEffect.PopSoundEffect();

                obs.SetHealth(obs.GetHealth() - 1);
                ScoreChanged(Variables.score + 10);
                material.bounciness += Variables.ball_bounciness_bonus;
                rb.velocity *= 1.01f;
                transform.localScale *= 1.04f;
            }
        }
        else
        {
            transform.localScale = first_size;
            material.bounciness -= Variables.ball_bounciness_bonus;
        }

        BallColorHandling();
    }

    private void BallColorHandling()
    {
        if (Variables.ball_health == 3)
            _renderer.material.color = Color.green;
        else if (Variables.ball_health == 2)
            _renderer.material.color = Color.yellow;
        else if (Variables.ball_health == 1)
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

    private void ScoreMultiplierChange(int new_value)
    {
        score_multiplier = new_value;
        scoreMtpText.text = "x" + score_multiplier.ToString();
    }



}
