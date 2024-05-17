using mygame;
using mygame.Managers;
using mygame.Models;

namespace game.Models;

public class Enemy : Sprite
{
    private Vector2 _position;

    public Enemy(Texture2D texture, Vector2 position, int frameX, int frameY) : base(texture, position, frameX, frameY)
    {
        Anims = new AnimationManager();
        Anims.AddAnimation(1, new Animation(texture, frameX, frameY, 0.1f));
    }

    public void Update(Hero hero)
    {

        if (Vector2.Distance(hero.position, _position) < 100) hero.Kill();

        Anims.Update(1);
    }
}