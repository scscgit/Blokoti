using System;
using Blokoti.Game.Scripts.Managers;
using UnityEngine;

namespace Blokoti.Game.Scripts.Actors.Players
{
    public class Player : AbstractActor
    {
        public Player()
        {
            stepSpeed = 0.04f;
        }

        private GameManager _gameManager;

        public override void Act()
        {
        }

        private new void OnEnable()
        {
            base.OnEnable();
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (_gameManager == null)
            {
                throw new Exception("Player couldn't initialize manager references");
            }
        }

        private void Update()
        {
            StepTowardsGoal();
            ProcessInputPrepareTarget();
        }

        private void ProcessInputPrepareTarget()
        {
            // Cannot start a next movement if still moving
            if (Moving || _gameManager.Acting)
            {
                return;
            }

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var targetRow = Row;
            var targetCol = Col;
            var move = false;
            if (horizontal < 0)
            {
                targetRow--;
                move = true;
            }
            else if (horizontal > 0)
            {
                targetRow++;
                move = true;
            }
            else if (vertical < 0)
            {
                targetCol--;
                move = true;
            }
            else if (vertical > 0)
            {
                targetCol++;
                move = true;
            }

            if (move)
            {
                // Handle wrong target and cancel the movement
                if (TileManager.GetTile(targetRow, targetCol) == null
                    || TileManager.GetTile(targetRow, targetCol).Component.transform.lossyScale.y > 1.25f
                )
                {
                    Debug.Log("Player's movement target to " + targetRow + ":" + targetCol + " unavailable");
                    return;
                }

                // Start the movement
                PositionSupport.SetGoal(targetRow, targetCol);
                // Start a next acting round
                _gameManager.ActActors();
            }
        }

        public void OnCollideActor(Component actor)
        {
            // Game Over
            Destroy(gameObject);
        }
    }
}
