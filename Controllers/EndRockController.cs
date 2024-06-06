using mygame.Managers;

namespace mygame.Controllers;

public static class EndRockController
{
    public static void Update(Vector2 position, Vector2 playerPosition, ref bool buttonDraw)
    {
        bool nearEnough = Vector2.Distance(playerPosition, position) < 100;

        if (nearEnough)
        {
            buttonDraw = true;
        }
        else
        {
            buttonDraw = false;
        }

        if (nearEnough && Keyboard.GetState().IsKeyDown(Keys.E))
        {
            LevelManager.ChangeLevel(LevelManager.CurrentLevel + 1);
        }
    }
}