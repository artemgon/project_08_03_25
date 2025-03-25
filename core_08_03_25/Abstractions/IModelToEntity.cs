using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core_08_03_25.Entity;
using data_08_03_25.Abstractions;

namespace core_08_03_25.Abstractions
{
    public interface IModelToEntity
    {
        public IEntity ToEntity(IModel model);  
    }
}
