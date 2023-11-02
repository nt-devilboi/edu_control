namespace EduControl;

public static class GenerateCode
{
    public static string GenerateToken()
    {
        return Guid.NewGuid().ToString("N");
    }
}