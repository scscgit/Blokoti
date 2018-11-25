using System;
using System.Collections.Generic;
using Blokoti.Game.Scripts.Actors.Players;
using Blokoti.Game.Scripts.Tiles;
using UnityEngine;

namespace Blokoti.Game.Scripts.Managers
{
    public class TileManager : MonoBehaviour
    {
        public GameObject firstTile;
        public GameObject parent;

        public int tileWidth;
        public int tileHeight;

        [Header("Edit before running game")] public int maxSizeRows;
        public int maxSizeCols;

        private IList<IList<ITile>> _availableTiles;
        private IList<IList<IList<Component>>> _tileActors;

        private void Awake()
        {
            // Initializing lists of tile positions and their actors
            _availableTiles = new List<IList<ITile>>(maxSizeRows);
            _tileActors = new List<IList<IList<Component>>>(maxSizeRows);
            for (var row = 0; row < maxSizeRows; row++)
            {
                _availableTiles.Add(new List<ITile>(maxSizeCols));
                _tileActors.Add(new List<IList<Component>>(maxSizeCols));
                for (var col = 0; col < maxSizeCols; col++)
                {
                    _availableTiles[row].Add(null);
                    _tileActors[row].Add(new List<Component>());
                }
            }
        }

        /// <summary>
        /// Can be activated with a button from the Editor.
        /// </summary>
        public void SpawnTiles()
        {
            for (int row = 0; row < maxSizeRows; row++)
            {
                for (int col = 0; col < maxSizeCols; col++)
                {
                    var firstPosition = firstTile.transform.transform.position;

                    var tile = GameObject.Instantiate(
                        firstTile,
                        new Vector3(
                            firstPosition.x + row * tileWidth,
                            firstPosition.y,
                            firstPosition.z + col * tileHeight
                        ),
                        Quaternion.identity,
                        parent.transform
                    );
                    tile.name = firstTile.name + " " + row + " " + col;
                }
            }

            // The original tile gets destroyed, as it was replaced by tile [0:0].
            firstTile.GetComponent<ITile>().Destroy();
        }

        public ITile GetTile(int row, int column)
        {
            try
            {
                return _availableTiles[row][column];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        public void RegisterTile(int row, int column, ITile tile)
        {
            Debug.Log("Registering tile " + row + ":" + column);
            _availableTiles[row][column] = tile;
        }

        public void UnregisterTile(int row, int column)
        {
            Debug.Log("Unregistering tile " + row + ":" + column);
            _availableTiles[row][column] = null;
        }

        public IList<Component> GetActors(int row, int column)
        {
            return _tileActors[row][column];
        }

        public void UnregisterActor(int row, int col, Component actor)
        {
            var player = actor as Player;
            if (player != null)
            {
                var tile = GetTile(row, col);
                if (tile != null)
                {
                    tile.OnPlayerExit(player);
                }
            }

            for (var actorIndex = 0; actorIndex < _tileActors[row][col].Count; actorIndex++)
            {
                if (actor == _tileActors[row][col][actorIndex])
                {
                    _tileActors[row][col].RemoveAt(actorIndex);
                    return;
                }
            }

            throw new Exception("ERROR: Attempted to unregister Actor " + actor.name + ", which wasn't registered");
        }

        public void RegisterActor(int row, int col, Component actor)
        {
            var onTile = _tileActors[row][col];
            onTile.Add(actor);

            var player = actor as Player;
            if (player != null)
            {
                var tile = GetTile(row, col);
                if (tile != null)
                {
                    tile.OnPlayerEnter(player);
                }

                foreach (var otherActorOnTile in onTile)
                {
                    if (!(otherActorOnTile is Player))
                    {
                        player.OnCollideActor(otherActorOnTile);
                    }
                }
            }
            else
            {
                // Someone other than Player, check if the actor collided with him
                foreach (var otherActorOnTile in onTile)
                {
                    var otherPlayerActor = otherActorOnTile as Player;
                    if (otherPlayerActor != null)
                    {
                        otherPlayerActor.OnCollideActor(actor);
                    }
                }
            }
        }
    }
}