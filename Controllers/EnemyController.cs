using game.Models;
using mygame.Managers;

namespace mygame.Controllers;

public static class EnemyController
{
    public static void Update(Enemy enemy, Hero hero)
    {
        if (Vector2.Distance(enemy.position, hero.position) < 50)
        {
            hero.Kill();
            LevelManager.ChangeLevel(0);
        }

        enemy.Anims.Update(1);
    }
}