using System.Collections.Generic;

namespace Michalski.Models
{
    public interface IViolinStorage
	{
		void Delete(IViolinModel item);
		void Save(IViolinModel item);
		List<IViolinModel> ReadAll();
	}
}
