using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Top
 * A domain model object that represents the characteristics of different tops,
 * and the last time they have been used
 */

namespace Clothing_Inventory.domain
{
    class Top
    {
        //----- INSTANCE VARIABLES

        public string description { get; set; }
        public string mainColour { get; set; }
        public DateOnly lastWorn { get; set; }
        public TopType type { get; set; }
        public bool inUse { get; set; }

        //----- CONSTRUCTORS

        public Top(string description, string mainColour,  DateOnly lastWorn, TopType type, bool inUse)
        {
            this.description = description;
            this.mainColour = mainColour;
            this.lastWorn = lastWorn;
            this.type = type;
            this.inUse = inUse;
        }

        //----- GENERAL METHODS

        //public void changeDescription(string description)
        //{
        //    Debug.Assert(description != null, "Description should not be null.");
        //    this.description = description;
        //}

        //public void changeColour(string colour)
        //{
        //    Debug.Assert(colour != null, "Colour should not be null.");
        //    this.mainColour = colour;
        //}

        //public void updateDate(DateOnly dateOnly)
        //{
        //    this.lastWorn = dateOnly;
        //}

        //public void useRegularly()
        //{
        //    this.inUse = true;
        //}
        //public void outOfRegularUse()
        //{
        //    this.inUse = false;
        //}
    }
}
