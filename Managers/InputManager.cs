namespace mygame.Managers;

public static class InputManager
{
    private static Vector2 _velocity;
    public static Vector2 Velocity => _velocity;

    public static bool IsMoving => _velocity != Vector2.Zero;

    public static void Update()
    {
        
    }
}