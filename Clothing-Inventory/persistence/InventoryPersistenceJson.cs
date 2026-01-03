using Clothing_Inventory.domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clothing_Inventory.persistence
{
    internal class InventoryPersistenceJson : InventoryPersistence
    {
        private string fileName;

        public InventoryPersistenceJson(string fileName)
        {
            Debug.Assert(fileName != null, "File name should not be null.");
            //JsonSerializerOptions.Default.IncludeFields = true;
            this.fileName = fileName;
        }

        public void saveTops(List<Top> tops)
        {
            string topJSON = JsonSerializer.Serialize(tops);
            File.WriteAllText(this.fileName, topJSON);
        }

        public List<Top> loadTops()
        {
            string allTops = File.ReadAllText(this.fileName);
            
            List<Top>? tops = JsonSerializer.Deserialize<List<Top>>(allTops);

            // Ensure the list returned is never null
            if (tops == null)
            {
                tops = new List<Top>();
            }

            return tops;
        }
    }
}
