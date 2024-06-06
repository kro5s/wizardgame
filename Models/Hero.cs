using mygame.Controllers;
using mygame.Managers;
using mygame.Models;

namespace mygame;

public class Hero : Sprite
{
    private readonly HeroController _controller;

    public Hero(Texture2D texture, Vector2 position, int frameX, int frameY, Level level) : base(texture, position, frameX, frameY)
    {
        Anims = new AnimationManager();
        Anims.AddAnimation(new Vector2(0, 0), new Animation(Globals.Content.Load<Texture2D>("character"), 6, 1, 0.1f));
        Anims.AddAnimation(new Vector2(1, 0), new Animation(Globals.Content.Load<Texture2D>("run"), 8, 1, 0.09f));
        Anims.AddAnimation(new Vector2(-1, 0), new Animation(Globals.Content.Load<Texture2D>("runleft"), 8, 1, 0.09f));
        Anims.AddAnimation(new Vector2(1, -1), new Animation(Globals.Content.Load<Texture2D>("fall"), 2, 1, 1f));
        Anims.AddAnimation(new Vector2(-1, -1), new Animation(Globals.Content.Load<Texture2D>("fallleft"), 2, 1, 1f));
        Anims.AddAnimation(new Vector2(0, -1), new Animation(Globals.Content.Load<Texture2D>("fall"), 2, 1, 1f));

        _controller = new HeroController(Anims, level, Width, Height);
    }

    public void Update()
    {
        _controller.Update(this);
    }
}