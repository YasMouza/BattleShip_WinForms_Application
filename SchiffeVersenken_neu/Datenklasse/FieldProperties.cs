using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchiffeVersenken_neu.Logik;
using System.Windows.Forms;

namespace SchiffeVersenken_neu.Datenklasse
{                                           
    public class FieldProperties
    {
        public FieldProperties(bool ShipIsHit, bool ShipIsSet, bool FieldIsHit)
        {
            this.ShipIsHit = ShipIsHit;
            this.ShipIsSet = ShipIsSet;
            this.FieldIsHit = FieldIsHit;
        }
        public bool ShipIsSet = false;
        public bool FieldIsHit = false;
        public bool ShipIsHit = false;
    }
}
