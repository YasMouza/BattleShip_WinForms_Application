using SchiffeVersenken_neu.Logik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchiffeVersenken_neu.Properties;
using System.Windows.Forms;


namespace SchiffeVersenken_neu.Datenklasse
{
    public class FieldData
    {
        private static List<ShipData> shipsPlayerListe = new List<ShipData>();
        private static List<ShipData> shipsComputerListe = new List<ShipData>();
        //public FieldProperties Occupation { get; set; }
        public string value;
        public string Value { get; set; }


        public FieldData(string value, FieldProperties occupation)
        {
            this.Value = value;
            //this.Occupation = occupation;
        }

        public FieldData(string playerCoord, string computerCoord, bool player)
        {
            this.playerCoord = playerCoord;
            this.computerCoord = computerCoord;
            this.player = player;
        }

        public FieldData(string playerCoord, bool player)
        {
            this.playerCoord = playerCoord;
            this.player = player;
        }

        public override string ToString()
        {
            return Value;
        }
        //FieldProperties
        public bool ShipIsSet = false;
        public bool FieldIsHit = false;
        public bool ShipIsHit = false;
        public bool IsShipSunk = false;
        public bool AllShipsSunk = false;

        public string playerCoord;
        public string computerCoord;
        public bool player;

    }
}
