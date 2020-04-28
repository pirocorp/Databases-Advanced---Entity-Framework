namespace _05._Mediator
{
    public static class MediatorDemo
    {
        public static void Main()
        {
            // Create chatRoom
            var chatRoom = new ChatRoom();

            // Create participants and register them
            var George = new Beatle("George");
            var Paul = new Beatle("Paul");
            var Ringo = new Beatle("Ringo");
            var John = new Beatle("John");
            var Yoko = new NonBeatle("Yoko");

            chatRoom.Register(George);
            chatRoom.Register(Paul);
            chatRoom.Register(Ringo);
            chatRoom.Register(John);
            chatRoom.Register(Yoko);

            // Chatting participants
            Yoko.Send("John", "Hi John!");
            Paul.Send("Ringo", "All you need is love");
            Ringo.Send("George", "My sweet Lord");
            Paul.Send("John", "Can't buy me love");
            John.Send("Yoko", "My sweet love");
        }
    }
}
