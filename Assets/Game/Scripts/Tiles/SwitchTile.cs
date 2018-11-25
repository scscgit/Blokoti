using Blokoti.Game.Scripts.Actors.Players;
using Blokoti.Game.Scripts.Tiles;
using UnityEngine.Events;

public class SwitchTile : AbstractTile
{
    public UnityEvent Enter;
    public UnityEvent Exit;

    public override void OnPlayerEnter(Player player)
    {
        Enter.Invoke();
    }

    public override void OnPlayerExit(Player player)
    {
        Exit.Invoke();
    }
}
