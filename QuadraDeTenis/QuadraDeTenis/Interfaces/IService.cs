using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuadraDeTenis
{
	public interface IService
	{
		Task<IEnumerable<Quadra>> GetQuadrasAsync();
		Task<Quadra> AddQuadraAsync(Quadra store);
		Task Init();
	}
}
