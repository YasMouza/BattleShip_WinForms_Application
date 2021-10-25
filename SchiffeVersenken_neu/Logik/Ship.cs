using BattleshipLibrary.Datenklasse;
using SchiffeVersenken_neu.Datenklasse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchiffeVersenken_neu.Logik;

namespace SchiffeVersenken_neu.Logik
{
    public class Ship
    {
        private static List<ShipData> shipsPlayerList = new List<ShipData>();
        private static List<ShipData> shipsComputerList = new List<ShipData>();
        public static void SetShip(ShipData shipData, bool player)
        {
            if (player)
            {
                shipsPlayerList.Add(shipData);

            }
            else if (!player)
            {
                shipsComputerList.Add(shipData);
            }
        }
        public static List<ShipData> GetShipsPlayer()
        {
            return shipsPlayerList;
        }
        public static List<ShipData> GetShipsComputer()
        {
            return shipsComputerList;
        }
    }
}
