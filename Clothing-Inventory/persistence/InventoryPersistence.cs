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
        List<Top> loadTopsByColour(string colour);
        List<Top> loadTopsByDescription(string description);
        List<Top> loadTopsByType(TopType type);
        List<Top> loadTopsByInRotation(bool inRotation);
    }
}
