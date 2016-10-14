namespace DADSTORM.PuppetMaster.Services
{
    public abstract class PuppetMasterService
    {
        public abstract void execute();

        public void assyncexecute()
        {
            ServiceThreads.Instance.AssyncInvoke(new ServiceWork(execute));
        }
    }
}