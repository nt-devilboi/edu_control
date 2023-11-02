namespace EduControl.DataBase;

public class BdConfig //todo question for teacher
{
    public  Schemas Schemas => new Schemas();
    public static Tables Tables => new Tables();
}


public class Schemas
{
    public string TimeControl => "time_control";
}

public class Tables
{
    public static string Account => "account";
    public static string Book => "book";
    public static string Event => "event";
    public static string Status => "status";
}