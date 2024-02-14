namespace RpgSaga.Core.BattleLogic
{
    using RpgSaga.Core.Interfaces;

    public class ProcessArgumentsReader : IProcessArgumentsReader
    {
        public ProcessArgumentsReader(string[] args)
        {
            Args = args;
        }

        public string[] Args { get; set; }

        public string[] GetProcessArguments()
        {
            return Args;
        }
    }
}
