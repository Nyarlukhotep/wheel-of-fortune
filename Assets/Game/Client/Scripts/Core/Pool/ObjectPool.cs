using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Client.Scripts.Core.Factory;

namespace Game.Client.Scripts.Core.Pool
{
	public class ObjectPool<T> : IObjectPool<T> where T : IPoolable
	{
		private readonly Stack<T> _pool = new ();
		private readonly IObjectFactory<T> _factory;
		private readonly int _maxSize;
		private int _activeCount;

		public ObjectPool(IObjectFactory<T> factory, int initialSize = 10, int maxSize = 100)
		{
			_factory = factory;
			_maxSize = maxSize;
			
			InitializeAsync(initialSize);
		}

		public async Task<T> GetAsync()
		{
			if (_pool.TryPop(out var obj))
			{
				return obj;
			}

			if (_activeCount >= _maxSize)
			{
				throw new InvalidOperationException("Pool limit reached");
			}
			
			obj = await _factory.Create();
			
			_activeCount++;
			return obj;
		}

		public void Return(T obj)
		{
			if (obj == null) return;
        
			obj.Reset();
			_pool.Push(obj);
			_activeCount--;
		}

		public void Clear()
		{
			while (_pool.Count > 0)
			{
				var obj = _pool.Pop();
				_factory.Release(obj);
			}
			_activeCount = 0;
		}

		public async Task InitializeAsync(int initialSize)
		{
			for (var i = 0; i < initialSize; i++)
			{
				var obj = await _factory.Create();
				_pool.Push(obj);
			}
		}
	}
}