using UnityEngine;

namespace BaseY
{
    public class PrefabFactory<T> : IFactory<T> where T : MonoBehaviour
    {
        #region Fields

        private GameObject _root;
        private GameObject _prefab;
        private string _name;
        private int _index = 0;

        #endregion

        #region Public Methods

        public PrefabFactory(GameObject prefab) : this(prefab, prefab.name) { }

        public PrefabFactory(GameObject prefab, string name)
        {
            this._prefab = prefab;
            this._name = name;
            _root = new GameObject
            {
                name = $"{name} Pool"
            };
        }

        public T Create()
        {
            var tempGameObject = GameObject.Instantiate(_prefab,_root.transform) ;
            tempGameObject.name = $"{_name}_{_index.ToString()}";
            tempGameObject.transform.SetParent(_root.transform);
            T objectOfType = tempGameObject.GetComponent<T>();
            _index++;
            return objectOfType;
        }

        public void ResetMember(T member)
        {
            member.GetComponent<Transform>().SetParent(_root.transform);
        }

        #endregion
    }
}