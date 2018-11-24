using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blokoti.Game.Scripts.Managers
{
    public class TileManager : MonoBehaviour
    {
        public GameObject firstTile;
        public GameObject parent;

        public int tileWidth;
        public int tileHeight;

        public int maxSizeRows;
        public int maxSizeCols;

        private IList<IList<Component>> _availableTiles;
        private IList<IList<IList<Component>>> _tileActors;

        private void Awake()
        {
            // Initializing lists of tile positions and their actors
            _availableTiles = new List<IList<Component>>(maxSizeRows);
            _tileActors = new List<IList<IList<Component>>>(maxSizeRows);
            for (var row = 0; row < maxSizeRows; row++)
            {
                _availableTiles.Add(new List<Component>(maxSizeCols));
                _tileActors.Add(new List<IList<Component>>(maxSizeCols));
                for (var col = 0; col < maxSizeCols; col++)
                {
                    _availableTiles[row].Add(null);
                    _tileActors[row].Add(new List<Component>());
                }
            }
        }

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
                    tile.name = "Tile " + row + " " + col;
                }
            }

            // The original tile gets destroyed, as it was replaced by tile [0:0].
            GameObject.Destroy(firstTile.gameObject);
        }

        public Component GetTile(int row, int column)
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

        public void RegisterTile(int row, int column, Component tile)
        {
            Debug.Log("Registering tile " + row + ":" + column);
            _availableTiles[row][column] = tile;
        }

        public IList<Component> GetActors(int row, int column)
        {
            return _tileActors[row][column];
        }

        public void UnregisterActor(int row, int col, Component actor)
        {
            for (var actorIndex = 0; actorIndex < _tileActors[row][col].Count; actorIndex++)
            {
                if (actor == _tileActors[row][col][actorIndex])
                {
                    _tileActors.RemoveAt(actorIndex);
                    return;
                }
            }

            // TODO: prevent
            Debug.Log("ERROR: Attempted to unregister Actor " + actor.name + ", which wasn't registered");
        }

        public void RegisterActor(int row, int col, Component actor)
        {
            _tileActors[row][col].Add(actor);
        }
    }
}
