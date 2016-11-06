namespace DADSTORM.RemoteInterfaces {
    public interface ICommands
    {
        void start(string op_id);

        void interval(string op_id, int ms);

        void status();

        void crash(string process);

        void freeze(string process);

        void unfreeze(string process);

        void wait(int ms);
    }
}
