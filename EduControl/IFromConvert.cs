using EduControl.DataBase.ModelBd;

namespace EduControl;

public interface IFromConvert<in T>
{
    public static abstract TValue From<TValue>(T request, Account account);
}