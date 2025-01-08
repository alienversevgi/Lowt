using UnityEngine;

namespace BaseY
{
    public class Test : MonoBehaviour
    {
        public void TestMethod()
        {
            Bullet x = new Bullet();
            GameObject xObject = new GameObject();
            Pool<Bullet> bulletPool = new Pool<Bullet>(new PrefabFactory<Bullet>(xObject),10);
            
        }
        
    }
}