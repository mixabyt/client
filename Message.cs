namespace WinFormsApp2
{
    //1 - тільки кидається серверу
    //2 тільки отримується від сервера 

    // відповідь про стан з'єднання (1,2) 
    public class HeartBeetMessage
    {
        public string type;
    }

    

    //
    public class UpdateCountUser
    {
        public string type;
        public int count;
    }

    public class SubMainMenu
    {
        public string type;
        public bool subscription;
    }

    public class FindInterlocutor
    {
        public string type;
    }


    public class StopFindingInterlocutor
    {
        public string type;
    }

    public class TextMessage
    {
        public string type;
        public string text;
    }
    public class LeaveDialog
    {
        public string type;
    }





}
