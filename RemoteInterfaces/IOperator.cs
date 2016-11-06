namespace DADSTORM.RemoteInterfaces {
    public interface IOperator
    {
        void send(string tuple);
        void registerAtUpstreamOperators(string[] addresses); 
        void addDownstreamOperator(/*may have parameters*/);
    }
}
