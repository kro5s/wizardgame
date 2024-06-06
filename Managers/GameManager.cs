using System;
using System.IO;
using game.Models;
using mygame.Models;

namespace mygame.Managers;

public class GameManager
{
    private readonly LevelManager _levelManager;

    public GameManager()
    {
        _levelManager = new LevelManager();

        Globals.WindowSize = new(57 * Tile.Size, 29 * Tile.Size);
    }

    public void Update()
    {
        _levelManager.Update();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin();

        _levelManager.Draw(); 

        Globals.SpriteBatch.End();
    }
}