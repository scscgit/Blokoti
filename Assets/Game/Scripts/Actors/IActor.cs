namespace Blokoti.Game.Scripts.Actors
{
    public interface IActor
    {
        void Act();
        bool Acting { get; }
    }
}
