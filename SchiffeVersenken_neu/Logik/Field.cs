
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchiffeVersenken_neu;
using SchiffeVersenken_neu.Datenklasse;

namespace SchiffeVersenken_neu.Logik
{
    public class Field
    {
        static Random random = new Random();
        private FieldData[,] playerField = new FieldData[6, 6];
        private FieldData[,] computerField = new FieldData[6, 6];


        public Field(Button[,] buttons, bool player)
        {
            CreateField(buttons, player);
        }



        public FieldData[,] FieldArray()
        {
            return playerField;
            
        }

        public void CreateField(Button[,] buttons, bool player)
        {
            int iX = 0;
            int iY = 0;
            if (player)
            {
                string playerCoord = buttons[iX, iY].Text;
                for (iX = 0; iX < playerField.GetLength(0); iX++)
                {
                    for (iY = 0; iY < playerField.GetLength(1); iY++)
                    {
                        playerCoord = buttons[iX, iY].Text;
                        playerField[iX, iY] = new FieldData(playerCoord, player);
                    }
                }
            }
            else
            {
                string coord = buttons[iX, iY].Text;
                for (iX = 0; iX < computerField.GetLength(0); iX++)
                {
                    for (iY = 0; iY < computerField.GetLength(1); iY++)
                    {
                        coord = buttons[iX, iY].Text;
                        computerField[iX, iY] = new FieldData(coord, player);
                    }
                }
            }
        }


        private bool AllPlayerShipsSunk() //Schiff überall getroffen = gesunken
        {
            bool shipSunk = false;
            var shipList = Ship.GetShipsPlayer();         
            foreach (ShipData ship in shipList)
            {
                shipSunk = false;
                if (ship.IsShipSunk)
                {
                    shipSunk = true;
                    MessageBox.Show("Dein Schiff wurde versenkt");
                }
            }
            return shipSunk;
        }

        private bool AllComputerShipsSunk()
        {
            //1.Variante
            bool shipSunk = false;
            var shipList = Ship.GetShipsComputer();
            foreach (ShipData ship in shipList)
            {
                shipSunk = false;
                if (ship.IsShipSunk)
                {
                    shipSunk = true;
                    MessageBox.Show("Gegnerisches Schiff versenkt");
                }
            }
            return shipSunk;

            //2.Variante

            //bool IsShipSunk = false;
            //foreach (ShipData ship in shipList)
            //{
            //    while (computerField[iX, iY].ShipIsHit == true)
            //    {
            //        IsShipSunk = true;
            //        MessageBox.Show("gegnerisches Schiff versenkt!");
            //    }


            //}
            //return IsShipSunk;
        }


        public void FireComputersShip(int xCoordinate, int yCoordinate) //Schiffe des Computers wird angegriffen
        {
            
            if (!(AllComputerShipsSunk()))
            {
                if (xCoordinate >= 0 && xCoordinate < 6 && yCoordinate >= 0 && yCoordinate < 6)
                {                    
                    if (computerField[xCoordinate, yCoordinate].ShipIsSet == true)
                    {
                        computerField[xCoordinate, yCoordinate].ShipIsHit = true;
                        MessageBox.Show("Du hast getroffen");
                    }
                    else if (computerField[xCoordinate, yCoordinate].FieldIsHit == true)
                    {
                        computerField[xCoordinate, yCoordinate].FieldIsHit = true;
                        //MessageBox.Show("Schiff verfehlt");
                    }
                }
            }
            else
            {
                MessageBox.Show("Spiel gewonnen");
            }
        }
        public void SpielerAngreifen()
        {
            if (!(AllPlayerShipsSunk()))
            {
                int xCoordinate, yCoordinate;
                xCoordinate = random.Next(0, 6);
                yCoordinate = random.Next(0, 6);
                if (xCoordinate >= 0 && xCoordinate < 6 && yCoordinate >= 0 && yCoordinate < 6)
                {
                    if (playerField[xCoordinate, yCoordinate].ShipIsSet == true)
                    {
                        playerField[xCoordinate, yCoordinate].ShipIsHit = true;
                        MessageBox.Show("Du wurdest getroffen");
                    }
                    else
                    {
                        playerField[xCoordinate, yCoordinate].FieldIsHit = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Spiel verloren");
            }
        }


        public void SetComputerShip()
        {
            
            int shipSizeRandom = random.Next(3, 4);
            var shipDirectionRandom = random.Next(0, 3);
            int rowRandom = random.Next(0, 6);
            int columnRandom = random.Next(0, 6);
            var shipDirection = ShipDirection.Oben;
            if (shipDirectionRandom == 0)
            {
                shipDirection = ShipDirection.Links;
            }
            else if (shipDirectionRandom == 1)
            {
                shipDirection = ShipDirection.Rechts;
            }
            else if (shipDirectionRandom == 2)
            {
                shipDirection = ShipDirection.Unten;
            }
            bool player = false;
            SetShip(computerField, shipSizeRandom, shipDirection, rowRandom, columnRandom, player);                                   
        }

        public void SetShip(FieldData[,] fieldArray, int shipSize, ShipDirection direction, int row, int column, bool player)
        {
            var coordinate = ConvertShipToCoords(shipSize, direction, row, column);


            if (KoordinateAusserhalb(fieldArray, shipSize, direction, row, column))
            {
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (KoordinateBesetzt(fieldArray, shipSize, direction, row, column))
            {
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                
                if (player && Ship.GetShipsPlayer().Count <= 3)
                {
                    Ship.SetShip(new ShipData(shipSize, column, row, direction), player);
                    PlaceShipsInGrid(fieldArray, shipSize, direction, row, column);
                }
                
                if (!player && Ship.GetShipsComputer().Count <= 3)
                {
                    Ship.SetShip(new ShipData(shipSize, column, row, direction), player);
                    PlaceShipsInGrid(fieldArray, shipSize, direction, row, column);
                }
            }
        }

        public ShipData ConvertShipToCoords(int shipSize, ShipDirection direction, int startrow, int startcolumn)
        {
            ShipData ship = new ShipData(shipSize, startrow, startcolumn, direction);
            return ship;
        }

        private bool KoordinateAusserhalb(FieldData[,] fieldArray, int shipSize, ShipDirection direction, int row, int column)
        {
            var coordinate = ConvertShipToCoords(shipSize, direction, row, column);
            bool outerBound = false;
            try
            {
                for (int iX = 0; iX < fieldArray.GetLength(0); iX++)
                {
                    for (int iY = 0; iY < fieldArray.GetLength(1); iY++)
                    {
                        if (row > 6 || row < 0 || column > 6 || column < 0 || shipSize > fieldArray.GetLength(0) || shipSize > fieldArray.GetLength(1))
                        {
                            outerBound = true;
                        }
                        else if (direction == ShipDirection.Links && shipSize > column + 1)
                        {
                            outerBound = true;
                        }
                        else if (direction == ShipDirection.Rechts && shipSize + column >= 7)
                        {
                            outerBound = true;
                        }
                        else if (direction == ShipDirection.Oben && shipSize > row + 1)
                        {
                            outerBound = true;
                        }
                        else if (direction == ShipDirection.Unten && shipSize + row >= 7)
                        {
                            outerBound = true;
                        }
                    }
                }

            }
            catch (ArgumentException)
            {

                MessageBox.Show("Koordinate liegt außerhalb des Feldes");
            }
            
            return outerBound;
        }
        private bool KoordinateBesetzt(FieldData[,] fieldArray, int shipSize, ShipDirection direction, int row, int column)
        {
            bool contains = false;
            var coordinate = ConvertShipToCoords(shipSize, direction, row, column);
            try
            {
                for (int iX = 0; iX < fieldArray.GetLength(0); iX++)
                {
                    for (int iY = 0; iY < fieldArray.GetLength(1); iY++)
                    {
                        while (shipSize > 0)
                        {
                            shipSize--;
                            if (direction == ShipDirection.Links)
                            {
                                if (fieldArray[row, column - 1].ShipIsSet == true)
                                {
                                    contains = true;
                                }
                            }
                            else if (direction == ShipDirection.Rechts)
                            {
                                if (fieldArray[row, column + 1].ShipIsSet)
                                {
                                    contains = true;
                                }
                            }
                            else if (direction == ShipDirection.Oben)
                            {
                                if (fieldArray[row - 1, column].ShipIsSet)
                                {
                                    contains = true;
                                }
                            }
                            else if (direction == ShipDirection.Unten)
                            {
                                if (fieldArray[row + 1, column].ShipIsSet)
                                {
                                    contains = true;
                                }
                            }
                        }
                    }
                }

            }
            catch (ArgumentException)
            {
                MessageBox.Show("Feld kann nur einmal besetzt werden");                
            }
            
            return contains;

        }


        // feld mit schiff besetzen wenn koordinaten innerhalb des feldes, dann prüfen ob nach links oder rechts demenentsprechend spalte--, solagen bis shipsize größer 0
        private void PlaceShipsInGrid(FieldData[,] fieldArray, int shipSize, ShipDirection direction, int startrow, int startcolumn)
        {
            for (int iX = 0; iX < fieldArray.GetLength(0); iX++)
            {
                for (int iY = 0; iY < fieldArray.GetLength(1); iY++)
                {
                    fieldArray[startrow, startcolumn].ShipIsSet = true;

                    if (shipSize <= 1)
                    {
                        return;
                    }
                    while (shipSize > 1)
                    {
                        shipSize--;
                        if (direction == ShipDirection.Links ||
                            direction == ShipDirection.Rechts &&
                            !(KoordinateAusserhalb(fieldArray, shipSize, direction, startrow, startcolumn - 1)))
                        {
                            if (direction == ShipDirection.Links)
                            {
                                fieldArray[startrow, startcolumn - 1].ShipIsSet = true;
                                startcolumn--;
                            }
                            else if (direction == ShipDirection.Rechts)
                            {
                                fieldArray[startrow, startcolumn + 1].ShipIsSet = true;
                                startcolumn++;
                            }
                        }
                        else if (direction == ShipDirection.Oben ||
                            direction == ShipDirection.Unten &&
                            !(KoordinateAusserhalb(fieldArray, shipSize, direction, startrow - 1, startcolumn)))
                        {
                            if (direction == ShipDirection.Oben)
                            {
                                if (startrow - 1 == 0)
                                {
                                    fieldArray[startrow - 1, startcolumn].ShipIsSet = true;
                                }
                                else
                                {
                                    fieldArray[startrow - 1, startcolumn].ShipIsSet = true;
                                    startrow--;
                                }

                            }
                            else
                            {
                                fieldArray[startrow + 1, startcolumn].ShipIsSet = true;
                                startrow++;
                            }
                        }
                    }                    
                }
            }
        }
        public void Reset()
        {

        }
    }
}
