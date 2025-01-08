using UnityEngine;

namespace BaseY
{
    public interface IFactory<T>
    {
        T Create();

        void ResetMember(T member);
    }
}