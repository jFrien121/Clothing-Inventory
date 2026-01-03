using Clothing_Inventory.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothing_Inventory.persistence
{
    internal interface InventoryPersistence
    {
        void saveTops(List<Top> tops);
        List<Top> loadTops();
    }
}
