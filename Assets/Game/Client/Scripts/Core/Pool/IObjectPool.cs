using System.Threading.Tasks;

namespace Game.Client.Scripts.Core.Pool
{
	public interface IObjectPool<T> where T : IPoolable
	{
		Task<T> GetAsync();
		void Return(T obj);
		void Clear();
		Task InitializeAsync(int initialSize);
	}
}