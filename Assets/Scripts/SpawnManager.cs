//using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    public static System.Collections.Generic.List<int> enabled_indexes = new();
    private float offset_first_x = 0.0f;
    private readonly int obs_last_idx = 8;
    private int fix_idx = 0, plus_ten_idx = 0, random_range_upper_limit = 90, random_range_lower_limit = 5;
    

    void Start()
    {
        float num = Random.Range(random_range_lower_limit, random_range_upper_limit);
        offset_first_x = (num % 20);
        if(offset_first_x < 10)
        {
            random_range_upper_limit = 90;
            random_range_lower_limit = 45;
        }
        else
        {
            random_range_upper_limit = 45;
            random_range_lower_limit = 5;
        }

        fix_idx = obs_last_idx + 1;
        plus_ten_idx = obs_last_idx + 2;

        for (int i = 0; i <= obs_last_idx; i++)
            enabled_indexes.Add(i);
        InvokeRepeating(nameof(Spawn), 3, 2.7f - (obstacles[0].GetComponent<Obstacle>().GetObstacleSpeed() / 3f));
    }

    void Spawn()
    {
        int choose_from_enabled_indexes = Random.Range(0, enabled_indexes.Count);
        int idx = enabled_indexes[choose_from_enabled_indexes];


        if(idx <= obs_last_idx)
            Instantiate(obstacles[idx], transform.position + new Vector3(offset_first_x, 0, 0), obstacles[idx].transform.rotation);
        else
            PerkSpawner();

        float num = Random.Range(random_range_lower_limit, random_range_upper_limit);
        offset_first_x = (num % 20);
        if (offset_first_x < 10)
        {
            random_range_upper_limit = 90;
            random_range_lower_limit = 45;
        }
        else
        {
            random_range_upper_limit = 45;
            random_range_lower_limit = 5;
        }
    }

    private void PerkSpawner()
    {
        PerkHandler ph = new();
        if ((Random.Range(0, 10) > 4) && (ph.GetFixTheBallEnabled() || ph.GetPlusTenScoreEnabled()))
        {
            int perk_idx = 0;
            if (ph.GetFixTheBallEnabled() && ph.GetPlusTenScoreEnabled())
                perk_idx = Random.Range(fix_idx, plus_ten_idx + 1);
            else if (ph.GetPlusTenScoreEnabled())
                perk_idx = plus_ten_idx;
            else if (ph.GetFixTheBallEnabled())
                perk_idx = fix_idx;

            Instantiate(obstacles[perk_idx], transform.position + new Vector3(offset_first_x, 0, 0), obstacles[perk_idx].transform.rotation);

            float num = Random.Range(random_range_lower_limit, random_range_upper_limit);
            offset_first_x = (num % 20);
            if (offset_first_x < 10)
            {
                random_range_upper_limit = 90;
                random_range_lower_limit = 45;
            }
            else
            {
                random_range_upper_limit = 45;
                random_range_lower_limit = 5;
            }
        }
    }

}
