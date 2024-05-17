using mygame;

namespace game.Models;

public class Score
{
    private int _score;

    public void Update(int value)
    {
        _score += value;
    }

    public void Draw()
    {
        Globals.SpriteBatch.DrawString(Globals.Content.Load<SpriteFont>("gameFont"), $"Score: {_score}", new Vector2(20, 20), Color.White);
    }
}