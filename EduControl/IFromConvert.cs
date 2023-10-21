namespace EduControl;

public interface IFromConvert<out T, in TFrom>
{
    public static abstract T From(TFrom entity);
}