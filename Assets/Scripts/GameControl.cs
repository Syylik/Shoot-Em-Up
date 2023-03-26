using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] private EnemyCreator _enemyCreator;

    internal int enemySpawned;
    internal int enemyLeft;

    public void SetEnemyCount(int spawned)
    {
        enemySpawned = spawned;
        enemyLeft = spawned;
    }

    public void ReduceEnemyLeft() => enemyLeft--;
}
