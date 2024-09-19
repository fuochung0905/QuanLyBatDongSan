using System.Runtime.Caching;

namespace FE.Helper
{
	public class InMemoryCache : ICachService
	{
		public T? Get<T>(string cachKey) where T : class
		{
			T? item = MemoryCache.Default.Get(cachKey) as T;
			return item;
		}

		public void Set<T>(string cachKey, T item, int minute = 2) where T : class
		{
			T? isExists = MemoryCache.Default.Get(cachKey) as T;
			if (item != null)
			{
				MemoryCache.Default.Remove(cachKey);
			}

			MemoryCache.Default.Add(cachKey, item, DateTime.Now.AddMinutes(minute));
		}
	}

	public interface ICachService
	{
		T? Get<T>(string cachKey) where T : class;
		void Set<T> (string cachKey, T item, int minute = 2) where T : class;
	}

}
