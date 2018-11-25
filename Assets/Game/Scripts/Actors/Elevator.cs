namespace Blokoti.Game.Scripts.Actors
{
    public class Elevator : AbstractActor
    {
        private bool _launched;

        public override void Act()
        {
        }

        public void Launch()
        {
            _launched = true;
        }

        private void Update()
        {
            if (_launched)
            {
                transform.Translate(0, 0.15f, 0);
            }
        }
    }
}
