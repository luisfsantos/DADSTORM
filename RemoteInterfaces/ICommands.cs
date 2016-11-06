namespace DADSTORM.RemoteInterfaces {
    public interface ICommands
    {
        void start();

        void interval(int ms);

        void status();

        void crash();

        void freeze();

        void unfreeze();

        void wait(int ms);
    }
}
