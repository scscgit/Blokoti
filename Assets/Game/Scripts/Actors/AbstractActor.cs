using System;
using Blokoti.Game.Scripts.Managers;
using UnityEngine;

namespace Blokoti.Game.Scripts.Actors
{
    public abstract class AbstractActor : MonoBehaviour, IActor, IPositionSupport
    {
        public float stepSpeed;

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

        public abstract void Act();

        public void Awake()
        {
            TileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
            PositionSupport = new PositionSupport(() => transform, () => stepSpeed, () => TileManager, this);
        }

        public virtual void Start()
        {
            PositionSupport.Start();
        }

        public bool Acting { get; protected set; }
    }
}
