using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Inventory
 * A class that keep tracks of a list of tops
 */

namespace Clothing_Inventory.domain
{
    internal class Inventory
    {
        private List<Top> allTops;

        public Inventory()
        {
            this.allTops = new List<Top>();
        }
    }
}
