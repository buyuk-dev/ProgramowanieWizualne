using System.Collections.Generic;

namespace Michalski.Models
{
    public interface IMakerStorage
	{
		void Delete(IMakerModel item);
		void Save(IMakerModel item);
		List<IMakerModel> ReadAll();
	}
}
