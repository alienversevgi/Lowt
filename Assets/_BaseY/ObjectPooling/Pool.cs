using System.Collections.Generic;

namespace BaseY
{
    public class Pool<T> where T : IPoolable
    {
        #region Fields

        private List<T> _members;
        private List<T> _unavailables;
        private IFactory<T> _factory;

        #endregion

        #region MyRegion

        #endregion

        #region Public Methods

        public Pool(IFactory<T> factory, int size)
        {
            _members = new List<T>();
            _unavailables = new List<T>();
            this._factory = factory;
            for (int i = 0; i < size; i++)
            {
                Create();
            }
        }

        public T Allocate()
        {
            for (int i = 0; i < _members.Count; i++)
            {
                if (!_unavailables.Contains(_members[i]))
                {
                    _unavailables.Add(_members[i]);
                    return _members[i];
                }
            }

            T newMembers = Create();
            _unavailables.Add(newMembers);
            return newMembers;
        }

        public void Release(T member)
        {
            member.OnDespawned();
            _factory.ResetMember(member);
            _unavailables.Remove(member);
        }

        public void ReleaseAll()
        {
            for (int i = 0; i < _unavailables.Count; i++)
            {
                Release(_unavailables[i]);
            }
        }

        #endregion

        #region Private Methods

        private T Create()
        {
            T member = _factory.Create();
            _members.Add(member);
            return member;
        }

        #endregion
    }
}