using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_08_03_25.Abstractions;
using core_08_03_25.Abstractions;

namespace core_08_03_25.Entity
{
    public class Settings : IEntity, IModelToEntity
    {
        public IEntity ToEntity(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
