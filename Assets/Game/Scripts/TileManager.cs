using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blokoti.Game.Scripts
{
    public class TileManager : MonoBehaviour
    {
        public GameObject firstTile;

        public int tileWidth;
        public int tileHeight;

        public int maxSizeRows;
        public int maxSizeCols;

        private IList<IList<bool>> _availableTiles;
        private IList<IList<MonoBehaviour>> _tileActors;

        private void Start()
        {
            // Initializing lists of tile positions and their actors
            _availableTiles = new List<IList<bool>>(maxSizeRows);
            for (int row = 0; row < maxSizeRows; row++)
            {
                _availableTiles.Add(new List<bool>(maxSizeCols));
            }

            _tileActors = new List<IList<MonoBehaviour>>(maxSizeRows);
            for (int row = 0; row < maxSizeRows; row++)
            {
                _tileActors.Add(new List<MonoBehaviour>(maxSizeCols));
            }

            // Spawning tiles
            for (int row = 0; row < maxSizeRows; row++)
            {
                for (int col = 0; col < maxSizeCols; col++)
                {
                    var firstPosition = firstTile.transform.transform.position;

                    GameObject.Instantiate(
                        firstTile,
                        new Vector3(
                            firstPosition.x + row * tileWidth,
                            firstPosition.y,
                            firstPosition.z + col * tileHeight
                        ),
                        Quaternion.identity
                    );
                }
            }
        }

        public bool IsAvailable(int row, int column)
        {
            try
            {
                return _availableTiles[row][column];
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public bool GetActors(int row, int column)
        {
            return _tileActors[row][column];
        }
    }
}
