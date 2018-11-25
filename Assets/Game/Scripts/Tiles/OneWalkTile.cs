using Blokoti.Game.Scripts.Actors.Players;

namespace Blokoti.Game.Scripts.Tiles
{
    public class OneWalkTile : AbstractTile
    {
        private bool _stepped;

        public override void OnPlayerEnter(Player player)
        {
            _stepped = true;
        }

        public override void OnPlayerExit(Player player)
        {
            if (_stepped)
            {
                Destroy();
            }
        }
    }
}