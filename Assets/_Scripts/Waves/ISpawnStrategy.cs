using System.Collections;

public interface ISpawnStrategy
{
    public IEnumerator Spawn(int num, EnemyCreator enemyCreator);
}
