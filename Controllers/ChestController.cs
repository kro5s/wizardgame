using game.Models;

namespace mygame.Controllers;

public static class ChestController
{
    public static void Update(Vector2 position, Vector2 playerPosition, ref bool isCollected, ref bool buttonDraw, Score score, int value)
    {
        bool nearEnough = Vector2.Distance(playerPosition, position) < 100;

        if (nearEnough && !isCollected)
        {
            buttonDraw = true;
        }
        else
        {
            buttonDraw = false;
        }

        if (nearEnough && Keyboard.GetState().IsKeyDown(Keys.E) && !isCollected)
        {
            score.Update(value);
            isCollected = true;
        }
    }
}