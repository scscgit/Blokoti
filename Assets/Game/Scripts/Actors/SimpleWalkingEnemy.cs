using System.Collections.Generic;
using Blokoti.Game.Scripts.Managers;
using UnityEngine;

namespace Blokoti.Game.Scripts.Actors
{
    public class SimpleWalkingEnemy : AbstractActor
    {
        public List<GridPosition> walkingTemplate;
        public int walkingTemplateIndex;
        private bool _end;

        private GameManager _gameManager;

        public new void OnEnable()
        {
            base.OnEnable();
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public override void Start()
        {
            base.Start();
            _gameManager.RegisterActor(this);
        }

        public override void Act()
        {
            if (_end)
            {
                return;
            }

            Acting = true;
            var walkingGoal = walkingTemplate[walkingTemplateIndex++];
            if (walkingTemplateIndex >= walkingTemplate.Count)
            {
                walkingTemplateIndex = 0;
            }

            if (walkingGoal.Row == Row)
            {
                if (walkingGoal.Column < Col)
                {
                    Transform.Find("Mesh").localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    for (var col = Col - 1; col >= walkingGoal.Column; col--)
                    {
                        if (TileManager.GetTile(Row, col) == null)
                        {
                            Moving = false;
                            _end = true;
                            return;
                        }
                    }
                }

                if (walkingGoal.Column > Col)
                {
                    Transform.Find("Mesh").localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    for (var col = Col + 1; col <= walkingGoal.Column; col++)
                    {
                        if (TileManager.GetTile(Row, col) == null)
                        {
                            Moving = false;
                            _end = true;
                            return;
                        }
                    }
                }
            }
            else if (walkingGoal.Column == Col)
            {
                if (walkingGoal.Row < Row)
                {
                    Transform.Find("Mesh").localRotation = Quaternion.Euler(new Vector3(0, 270, 0));
                    for (var row = Row - 1; row >= walkingGoal.Row; row--)
                    {
                        if (TileManager.GetTile(row, Col) == null)
                        {
                            Moving = false;
                            _end = true;
                            return;
                        }
                    }
                }

                if (walkingGoal.Row > Row)
                {
                    Transform.Find("Mesh").localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    for (var row = Row + 1; row <= walkingGoal.Row; row++)
                    {
                        if (TileManager.GetTile(row, Col) == null)
                        {
                            Moving = false;
                            _end = true;
                            return;
                        }
                    }
                }
            }

            SetGoal(walkingGoal.Row, walkingGoal.Column);
        }

        private void Update()
        {
            if (StepTowardsGoal())
            {
                Acting = false;
            }
        }
    }
}
