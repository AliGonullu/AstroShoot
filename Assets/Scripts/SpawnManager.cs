using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    public static List<int> enabled_indexes = new();
    
    float j_first = 0.0f;

    void Start()
    {
        for (int i = 0; i <= 7; i++)
            enabled_indexes.Add(i);
        InvokeRepeating(nameof(Spawn), 3, 3f - (obstacles[0].GetComponent<Obstacle>().GetObstacleSpeed() / 5f));   
    }

    void Spawn()
    {
        int choose_from_enabled_indexes = Random.Range(0, enabled_indexes.Count);
        int idx = enabled_indexes[choose_from_enabled_indexes];
        float j = Random.Range(-4f, 4f);
        j_first = j;

        if (Mathf.Abs(j_first - j) < 2)
        {
            j = RandomizeOffset(j);
        }
        if(idx <= 7)
            Instantiate(obstacles[idx], transform.position + new Vector3(j, 0, 0), obstacles[idx].transform.rotation);
        else
            PerkSpawner();
    }

    private void PerkSpawner()
    {
        PerkHandler ph = new();
        float j = Random.Range(-4f, 4f);
        if ((Random.Range(0, 10) > 5) && (ph.GetFixTheBallEnabled() || ph.GetPlusTenScoreEnabled()))
        {
            int perk_idx = 0;
            if (ph.GetFixTheBallEnabled() && ph.GetPlusTenScoreEnabled())
                perk_idx = Random.Range(8, 9);
            else if (ph.GetPlusTenScoreEnabled())
                perk_idx = 9;
            else if (ph.GetFixTheBallEnabled())
                perk_idx = 8;
            j = RandomizeOffset(j);
            Instantiate(obstacles[perk_idx], transform.position + new Vector3(j, 0, 0), obstacles[perk_idx].transform.rotation);
        }
    }


    private float RandomizeOffset(float _j) 
    {
        int offset = Random.Range(-1, 1);
        if (offset != 0)
            _j += 4 * offset;
        else
            _j += -3 * (offset + 1);
        return _j;
    }

}
