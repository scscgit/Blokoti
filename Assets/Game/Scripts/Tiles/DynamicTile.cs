using Blokoti.Game.Scripts.Actors.Players;
using UnityEngine;

namespace Blokoti.Game.Scripts.Tiles
{
    [SelectionBase]
    public class DynamicTile : AbstractTile
    {
        public int type;

        private bool _isSelected;

        private void OnMouseEnter()
        {
            //transform.Translate(0, 0.25f, 0);
            _isSelected = true;
        }

        private void OnMouseExit()
        {
            //transform.Translate(0, -0.25f, 0);
            _isSelected = false;
        }

        public override void Start()
        {
            base.Start();
            ApplyType(type);
        }

        public override void OnPlayerEnter(Player player)
        {
        }

        public override void OnPlayerExit(Player player)
        {
        }

        private void Update()
        {
            if (_isSelected && Input.GetMouseButtonDown(0))
            {
                ApplyType(type + 1);
            }

            if (_isSelected && Input.GetMouseButtonDown(1))
            {
                TransformType(type);
            }

            if (transform.Find("Grey").gameObject.activeInHierarchy.Equals(true))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.localScale += new Vector3(0, 0.5F, 0);
                    transform.position += new Vector3(0, 0.25F, 0);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (transform.localScale.y > 0.5F)
                    {
                        transform.localScale -= new Vector3(0, 0.5F, 0);
                        transform.position -= new Vector3(0, 0.25F, 0);
                    }
                }
            }
        }

        private void TransformType(int transformType)
        {
            switch (transformType)
            {
                case 0:
                    transform.Find("Green").gameObject.SetActive(false);
                    transform.Find("White").gameObject.SetActive(false);
                    transform.Find("Grey").gameObject.SetActive(false);
                    transform.Find("Broken").gameObject.SetActive(true);
                    Debug.Log("Transformed tile " + Row + ":" + Col + " into " + typeof(OneWalkTile).Name);
                    gameObject.AddComponent<OneWalkTile>();
                    Destroy(this);
                    break;
                case 1:
                    transform.Find("Green").gameObject.SetActive(false);
                    transform.Find("White").gameObject.SetActive(false);
                    transform.Find("Grey").gameObject.SetActive(true);
                    transform.Find("Broken").gameObject.SetActive(false);
                    Debug.Log("Transformed tile " + Row + ":" + Col + " into " + typeof(SwitchTile).Name);
                    var switchTile = gameObject.AddComponent<SwitchTile>();
                    var clips = switchTile.GetComponent<Animator>().runtimeAnimatorController.animationClips;
                    // TODO: clips
                    Destroy(this);
                    break;
            }
        }

        private void ApplyType(int newType)
        {
            if (newType >= 2)
            {
                newType = 0;
            }

            type = newType;
            switch (newType)
            {
                case 0:
                    transform.Find("Green").gameObject.SetActive(true);
                    transform.Find("White").gameObject.SetActive(false);
                    break;
                case 1:
                    transform.Find("Green").gameObject.SetActive(false);
                    transform.Find("White").gameObject.SetActive(true);
                    break;
            }
        }
    }
}
