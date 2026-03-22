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

        public List<Top> loadTopsByColour(string colour)
        {
            List<Top> tops = loadTops();
            List<Top> updatedList = new List<Top>();

            // Put each top with matching colour in the new list
            foreach (Top top in tops)
            {
                if (top.mainColour.Equals(colour))
                {
                    updatedList.Add(top);
                }
            }

            return updatedList;
        }

        public List<Top> loadTopsByDescription(string description)
        {
            List<Top> tops = loadTops();
            List<Top> updatedList = new List<Top>();

            // Put each top with matching description in the new list
            foreach (Top top in tops)
            {
                if (top.description.Equals(description))
                {
                    updatedList.Add(top);
                }
            }

            return updatedList;
        }

        public List<Top> loadTopsByType(TopType type)
        {
            List<Top> tops = loadTops();
            List<Top> updatedList = new List<Top>();

            // Put each top with matching type in the new list
            foreach (Top top in tops)
            {
                if (top.type.Equals(type))
                {
                    updatedList.Add(top);
                }
            }

            return updatedList;
        }

        public List<Top> loadTopsByInRotation(bool inRotation)
        {
            List<Top> tops = loadTops();
            List<Top> updatedList = new List<Top>();

            // Put each top that matches the rotation condition given
            foreach (Top top in tops)
            {
                if (top.inUse.Equals(inRotation))
                {
                    updatedList.Add(top);
                }
            }

            return updatedList;
        }
    }
}
