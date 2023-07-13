using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    public static List<int> enabled_indexes = new();
    private float old_offset = 0f, new_offset = 0f;
    private readonly int obs_last_idx = 8;
    private int fix_idx = 0, plus_ten_idx = 0, random_range_upper_limit = 85, random_range_lower_limit = 15;
    

    void Start()
    {
        ChangeOffset();

        fix_idx = obs_last_idx + 1;
        plus_ten_idx = obs_last_idx + 2;

        for (int i = 0; i <= plus_ten_idx; i++)
            enabled_indexes.Add(i);

        if (PerkHandler.plusTenScoreEnabled)
            enabled_indexes.Remove(plus_ten_idx);
        if (PerkHandler.fixTheBallEnabled)
            enabled_indexes.Remove(fix_idx);

        InvokeRepeating(nameof(Spawn), 3, 2.8f - (Variables.obstacle_speed / 2.8f));
    }

    void Spawn()
    {
        int choose_from_enabled_indexes = Random.Range(0, enabled_indexes.Count);
        int idx = enabled_indexes[choose_from_enabled_indexes];


        if(idx <= obs_last_idx)
            Instantiate(obstacles[idx], transform.position + new Vector3(new_offset, 0, 0), obstacles[idx].transform.rotation);
        else
            PerkSpawner();

        ChangeOffset();
    }

    private void PerkSpawner()
    {
        if (Random.Range(0, 100) > 20f && (PerkHandler.fixTheBallEnabled || PerkHandler.plusTenScoreEnabled))
        {
            int perk_idx = 0;
            if (PerkHandler.fixTheBallEnabled && PerkHandler.plusTenScoreEnabled)
                perk_idx = Random.Range(fix_idx, plus_ten_idx + 1);
            else if (PerkHandler.plusTenScoreEnabled)
                perk_idx = plus_ten_idx;
            else if (PerkHandler.fixTheBallEnabled)
                perk_idx = fix_idx;
            Debug.Log(perk_idx);
            Instantiate(obstacles[perk_idx], transform.position + new Vector3(new_offset, 0, 0), obstacles[perk_idx].transform.rotation);
            ChangeOffset();
        }
    }

    private void ChangeOffset()
    {
        float num = Random.Range(random_range_lower_limit, random_range_upper_limit);

        if (new_offset != 0)
        {
            old_offset = new_offset;
            if (old_offset < 10)
            {
                random_range_upper_limit = 80;
                random_range_lower_limit = 55;
            }
            else
            {
                random_range_upper_limit = 45;
                random_range_lower_limit = 15;
            }
        }
        new_offset = (num % 20);
    }
}
