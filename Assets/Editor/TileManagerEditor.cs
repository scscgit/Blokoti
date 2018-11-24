using Blokoti.Game.Scripts;
using UnityEditor;
using UnityEngine;

namespace Blokoti.Editor
{
    [CustomEditor(typeof(TileManager))]
    public class TileManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Spawn Tiles"))
            {
                ((TileManager) target).SpawnTiles();
            }
        }
    }
}
