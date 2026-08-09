using UnityEngine;

public class Enemy : Character
{
    private EnemyData _enemyData;

    protected override void Awake()
    {
        base.Awake();
        _enemyData = GetComponent<EnemyData>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
