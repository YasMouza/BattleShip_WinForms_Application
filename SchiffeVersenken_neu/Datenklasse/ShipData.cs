using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchiffeVersenken_neu.Logik;
using SchiffeVersenken_neu.Datenklasse;
using System.Windows.Forms;

namespace SchiffeVersenken_neu.Datenklasse
{
    public enum ShipDirection
    {
        Oben, Unten, Rechts, Links
    }

    public class ShipData
    {
        
        public ShipData(int size, int Row, int Column, ShipDirection direction)
        {
            this.Size = size;
            this.Row = Row;
            this.Column = Column;
            this.Direction = direction;
        }
        public ShipDirection Direction { get; set; }
        public int Size { get; set; }
        public bool IsShipSunk { get; set; }
        public bool AllShipsSunk { get; set; }
        //public ShipAttacks(bool IsShipSunk, bool AllShipsSunk)
        //{
        //    this.IsShipSunk = IsShipSunk;
        //    this.AllShipsSunk = AllShipsSunk;
        //}
        
        //public bool IsShipSunk = false;
        //public bool AllShipsSunk = false;
        public int Row { get; set; }
        public int Column { get; set; }
        

    }



}
