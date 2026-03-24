namespace UniversitetOppgave.Interface
{
    internal interface ILaaner
    {
        int MaksAntallLaan { get; }
        bool KanLaane(int aktiveLaan);
    }
}