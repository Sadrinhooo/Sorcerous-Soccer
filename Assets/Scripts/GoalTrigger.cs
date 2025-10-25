using UnityEngine;

public enum PlayerGoals
{
    Player1Goal, Player2Goal
}
public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private PlayerGoals playerGoals;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (playerGoals == PlayerGoals.Player1Goal)
            {
                GameManager.Player2Score++;
            }

            if (playerGoals == PlayerGoals.Player2Goal)
            {
                GameManager.Player1Score++;
            }
        }
    }
}
