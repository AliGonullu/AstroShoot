using UnityEngine;

public class BallSlot : MonoBehaviour
{
    [SerializeField] private Ball ball;

    private void Update()
    {
        if (ball.GetBallTaken())
        {
            ball.transform.position = transform.position;
        }
    }
}
