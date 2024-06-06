using System.Collections.Generic;
using game.Models;

namespace mygame.Controllers;

public static class LevelController
{
    public static void Update(Hero hero, List<Chest> chests, List<Campfire> campfires, List<Enemy> enemies, EndRock end)
    {
        foreach (var chest in chests)
        {
            chest.Update(hero.position);
        }

        foreach (var enemy in enemies)
        {
            if (!enemy.IsDead) enemy.Update(hero);
        }

        foreach (var campfire in campfires)
        {
            campfire.Update(hero.position);
        }

        end.Update(hero.position);

        if (!hero.IsDead) hero.Update();
    }
}