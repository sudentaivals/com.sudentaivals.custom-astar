using System;
using UnityEngine;

namespace sudentaivals.CustomAstar
{
    public class GridManager : MonoBehaviour
    {
        #region singletonScripts
        private static GridManager _instance;

        public static GridManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<GridManager>();
                    if(_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(GridManager).Name;
                        _instance = obj.AddComponent<GridManager>();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _instance = null;
        }

        #endregion
        [HideInInspector] public PathfindingGrid CurrentGrid {get; private set;} = null;
        [SerializeField] private bool _overrideShowGrid = false;
        [SerializeField] private bool _showGrid = true;
        [SerializeField] private bool _showObstacles = true;

        public bool OverrideShowGrid => _overrideShowGrid;
        public bool ShowGrid => _showGrid;
        public bool ShowObstacles => _showObstacles;

        public void SetGrid(PathfindingGrid newGrid)
        {
            CurrentGrid = newGrid;
        }
    }
}
