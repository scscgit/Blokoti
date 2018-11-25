using Blokoti.Game.Scripts.Actors.Players;
using Blokoti.Game.Scripts.Managers;
using UnityEngine;

namespace Blokoti.Game.Scripts.Tiles
{
    public abstract class AbstractTile : MonoBehaviour, IPositionSupport, ITile
    {
        protected TileManager TileManager { get; private set; }
        protected PositionSupport PositionSupport { get; private set; }

        public Transform Transform
        {
            get { return PositionSupport.Transform; }
        }

        public float StepSpeed
        {
            get { return PositionSupport.StepSpeed; }
        }

        public int Row
        {
            get { return PositionSupport.Row; }
            set { PositionSupport.Row = value; }
        }

        public int Col
        {
            get { return PositionSupport.Col; }
            set { PositionSupport.Col = value; }
        }


        public bool Moving
        {
            get { return PositionSupport.Moving; }
        }

        public int TargetRow
        {
            get { return PositionSupport.TargetRow; }
        }

        public int TargetCol
        {
            get { return PositionSupport.TargetCol; }
        }

        public void SetGoal(int row, int col)
        {
            PositionSupport.SetGoal(row, col);
        }

        public bool StepTowardsGoal()
        {
            return PositionSupport.StepTowardsGoal();
        }

        public void OnEnable()
        {
            PositionSupport = new PositionSupport(() => transform, () => 0, () => TileManager, this);
            TileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        }

        public virtual void Start()
        {
            TileManager.RegisterTile(Row, Col, this);
        }

        public Component Component
        {
            get { return this; }
        }

        public void Destroy()
        {
            TileManager.UnregisterTile(Row, Col);
            GameObject.Destroy(gameObject);
        }

        public abstract void OnPlayerEnter(Player player);

        public abstract void OnPlayerExit(Player player);
    }
}