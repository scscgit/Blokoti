using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blokoti.Game.Scripts
{
    public class TileManager : MonoBehaviour
    {
        public GameObject firstTile;
        public GameObject parent;

        public int tileWidth;
        public int tileHeight;

        public int maxSizeRows;
        public int maxSizeCols;

        private IList<IList<bool>> _availableTiles;
        private IList<IList<IList<MonoBehaviour>>> _tileActors;

        private void Awake()
        {
            // Initializing lists of tile positions and their actors
            const bool availableByDefault = false;
            _availableTiles = new List<IList<bool>>(maxSizeRows);
            _tileActors = new List<IList<IList<MonoBehaviour>>>(maxSizeRows);
            for (var row = 0; row < maxSizeRows; row++)
            {
                _availableTiles.Add(new List<bool>(maxSizeCols));
                _tileActors.Add(new List<IList<MonoBehaviour>>(maxSizeCols));
                for (var col = 0; col < maxSizeCols; col++)
                {
                    _availableTiles[row].Add(availableByDefault);
                    _tileActors[row].Add(new List<MonoBehaviour>());
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

        public bool IsAvailable(int row, int column)
        {
            try
            {
                return _availableTiles[row][column];
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        public void SetAvailable(int row, int column, bool value)
        {
            Debug.Log("Set available tile " + row + ":" + column);
            _availableTiles[row][column] = value;
        }

        public IList<MonoBehaviour> GetActors(int row, int column)
        {
            return _tileActors[row][column];
        }
    }
}
