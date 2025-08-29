using System;
using System.Threading.Tasks;

namespace Game.Client.Scripts.Core.Factory
{
	public interface IObjectFactory<T> : IDisposable
	{
		Task<T> Create();
		void Release(T obj);
	}
}