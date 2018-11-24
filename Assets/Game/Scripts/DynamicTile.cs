﻿using UnityEngine;

namespace Blokoti.Game.Scripts
{
    public class DynamicTile : MonoBehaviour
    {
        public int Row
        {
            get { return Mathf.RoundToInt(transform.position.x + 4.5f); }
            set { transform.position += new Vector3(value - 4.5f, 0, 0); }
        }

        public int Col
        {
            get { return Mathf.RoundToInt(transform.position.z + 4.5f); }
            set { transform.position += new Vector3(0, 0, value - 4.5f); }
        }

        public int type;

        private bool _isSelected;
        private TileManager _tileManager;

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

        private void Start()
        {
            ApplyType(type);
            _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
            _tileManager.SetAvailable(Row, Col, true);
        }

        private void Update()
        {
            if (_isSelected && Input.GetMouseButtonDown(0))
            {
                ApplyType(type + 1);
            }
            if (transform.Find("Grey").gameObject.active.Equals(true))
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

        private void ApplyType(int newType)
        {
            if (newType > 2)
            {
                newType = 0;
            }

            type = newType;
            switch (newType)
            {
                case 0:
                    transform.Find("Green").gameObject.SetActive(true);
                    transform.Find("White").gameObject.SetActive(false);
                    transform.Find("Grey").gameObject.SetActive(false);
                    break;
                case 1:
                    // todo
                    transform.Find("Green").gameObject.SetActive(false);
                    transform.Find("White").gameObject.SetActive(true);
                    transform.Find("Grey").gameObject.SetActive(false);
                    break;
                case 2:
                    // todo
                    transform.Find("Green").gameObject.SetActive(false);
                    transform.Find("White").gameObject.SetActive(false);
                    transform.Find("Grey").gameObject.SetActive(true);
                    break;
            }
        }
    }
}
