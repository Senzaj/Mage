
namespace Sources.Modules.Common
{
    public class Saver<T>
    {
        protected T MyData;

        public virtual void Init(T data)
        {
#if UNITY_EDITOR
            return;
#endif

            MyData = data;
        }
        
        public virtual T GetData()
        {
            return MyData;
        }

        public virtual void SaveData(T data)
        {
#if UNITY_EDITOR
            return;
#endif
            
            MyData = data;
        }
    }
}