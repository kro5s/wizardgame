using game.Models;
using mygame.Models;

namespace mygame.Controllers;

public static class CampfireController
{
    public static void Update(Vector2 campfirePosition, Vector2 playerPosition, ref bool _isFired, ref bool _buttonDraw, Animation _fireAnimation)
    {
        bool nearEnough = Vector2.Distance(playerPosition, campfirePosition) < 100;

        if (nearEnough && !_isFired)
        {
            _buttonDraw = true;
        }
        else
        {
            _buttonDraw = false;
        }

        if (nearEnough && Keyboard.GetState().IsKeyDown(Keys.E) && !_isFired)
        {
            _isFired = true;
        }

        if (_isFired)
        {
            _fireAnimation.Update();
        }
    }
}