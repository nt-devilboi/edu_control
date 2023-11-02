namespace EduControl.Controllers.AppController;

public static class ExceptionEntity
{
    public static ApiResult<T> NotFound<TE, T>() =>
        new($"app:{typeof(TE).Name}-not-found", string.Empty, 404);
    
    public static ApiResult<T> NotFound<T>() =>
        new($"app:{typeof(T).Name}-not-found", string.Empty, 404);
    
    public static ApiResult<T> BelongsToOtherUser<TE, T>() =>
        new($"app:{typeof(TE).Name}-not-your", string.Empty, 403);

    public static ApiResult<T> BusyName<TE, T>() =>
        new($"app:{typeof(TE).Name}-name-is-busy", string.Empty, 403);

 
    public static ApiResult<T> BelongsToOtherUser<T>() =>
        new($"app:{typeof(T).Name}-not-your", string.Empty, 403);

    public static ApiResult<T> BusyName<T>() =>
        new($"app:{typeof(T).Name}-name-is-busy", string.Empty, 403);
}