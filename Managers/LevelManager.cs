using System.IO;

namespace mygame.Managers;

public class LevelManager
{
    private static Level _level;
    public static int CurrentLevel;

    public LevelManager()
    {
        using (Stream fileStream = TitleContainer.OpenStream("Content/Levels/0.txt"))
        {
            _level = new(fileStream);
        }
    }

    public static void ChangeLevel(int id)
    {
        if (id > 1)
        {
            CurrentLevel = 0;
        }
        else
        {
            CurrentLevel = id;
        }

        using (Stream fileStream = TitleContainer.OpenStream($"Content/Levels/{CurrentLevel}.txt"))
        {
            _level = new(fileStream);
        }
    }

    public void Update()
    {
        _level.Update();
    }

    public void Draw()
    {
        _level.Draw();
    }
}