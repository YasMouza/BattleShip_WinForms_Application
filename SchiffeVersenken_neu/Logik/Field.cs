using System;
using System.Collections.Generic;
using System.Drawing;
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
        public FieldData[,] felder;

        int Row;
        int Column;
        Coordinates coordinate;
        private bool ShipIsSet;
        int shipSize;
        private int shipIndex;
        public string testErgebnis = string.Empty;

        public Field(int reihenAnzahl, int spaltenAnzahl)
        {
            //coordinate.xCoordinate = 0;
            //coordinate.yCoordinate = 0;
            
            felder = new FieldData[reihenAnzahl, spaltenAnzahl];
            for (int xCoordinate = 0; xCoordinate < 6; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < 6; yCoordinate++)
                {
                    felder[xCoordinate, yCoordinate] = new FieldData();                    
                }
            }
        }

        public static int AddVal(int a, int b)
        {
            return a + b;
        }

        public FieldData[,] GetFieldArray()
        {
            return felder;
        }

        public void ComputerAngreifen(Coordinates coordinate) 
        {
            //Messagebox disablen für unit Tests 
            if (GetFieldArray()[coordinate.XCoordinate, coordinate.YCoordinate].ShipIsSet)
            {
                felder[coordinate.XCoordinate, coordinate.YCoordinate].ShipIsHit = true;
                testErgebnis = "Du hast getroffen";
                MessageBox.Show("Du hast getroffen");
            }
            else if (!GetFieldArray()[coordinate.XCoordinate, coordinate.YCoordinate].ShipIsSet)
            {
                testErgebnis = "Schiff verfehlt";
                MessageBox.Show("Schiff verfehlt");
            }
            if (SpielGewonnen())
            {
                testErgebnis = "Du hast gewonnen";
                MessageBox.Show("Du hast gewonnen", "Gewonnen");
            }
        }

        public bool SpielGewonnen()
        {
            bool allShipsSunk = true;                

            if (Ship.GetShips().Count <= 3)
            {
                for (int iX = 0; iX < felder.GetLength(0); iX++)
                {
                    for (int iY = 0; iY < felder.GetLength(1); iY++)
                    {
                        if (felder[iX, iY].ShipIsSet && !felder[iX, iY].ShipIsHit)
                        {
                            allShipsSunk = false;
                        }                        
                    }
                }               
                
            }
            return allShipsSunk;
        }

        public bool SpielVerloren()
        {
            bool allShipsSunk = true;
            if (Ship.GetShips().Count <= 3)
            {
                for (int iX = 0; iX < felder.GetLength(0); iX++)
                {
                    for (int iY = 0; iY < felder.GetLength(1); iY++)
                    {
                        if (felder[iX, iY].ShipIsSet && !felder[iX, iY].ShipIsHit)
                        {
                            allShipsSunk = false;
                        }
                    }
                }
            }
            return allShipsSunk;
        }

        public bool BeendeSpiel(Coordinates coordinates)
        {
            bool spielBeendet = false;
            if (spielBeendet = true)
            {
                felder[coordinates.XCoordinate, coordinates.YCoordinate].FieldIsHit = true;
                felder[coordinates.XCoordinate, coordinates.YCoordinate].ShipIsHit = true;
                felder[coordinates.XCoordinate, coordinates.YCoordinate].ShipIsSet = true;

            }return spielBeendet;
        }

        public void SpielerAngreifen(int xCoordinate, int yCoordinate, int shipSize, Coordinates coordinate)
        {
            xCoordinate = random.Next(0, 6);
            yCoordinate = random.Next(0, 6);
            if (xCoordinate >= 0 && xCoordinate < 6 && yCoordinate >= 0 && yCoordinate < 6) //ohne .FieldIsHit möglich?
            {
                if (felder[xCoordinate, yCoordinate].ShipIsSet)
                {
                    felder[xCoordinate, yCoordinate].ShipIsHit = true;
                    MessageBox.Show("Du wurdest getroffen");
                }
                else
                {
                    felder[xCoordinate, yCoordinate].FieldIsHit = true;
                }
            }
            if (SpielVerloren())
            {
                MessageBox.Show("Alle deine Schiffe wurden versenkt", "verloren");

            }
        }

        public void SetComputerShip()
        {
            
            int shipSizeRandom = random.Next(2, 4);
            var shipDirectionRandom = random.Next(0, 3);
            int rowRandom = random.Next(0, 5);
            int columnRandom = random.Next(0, 5);
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
            
            TrySetShip(shipSizeRandom, shipDirection, rowRandom, columnRandom);
            bool ShipIsSet = false;
            while (ShipIsSet == false)
            {                

                try
                {
                    TrySetShip(shipSizeRandom, shipDirection, rowRandom, columnRandom);
                    ShipIsSet = true;
                }
                catch (Exception)
                {

                    ShipIsSet = false;
                }
            }
        }

        public bool TrySetShip(int shipSize, ShipDirection direction, int row, int column)
        {
            var ret = false;
            var coordinate = ConvertShipToCoords(row, column);

            if (!KoordinatenAusserhalb(coordinate, shipSize, direction) && !KoordinatenBesetzt(coordinate, shipSize, direction))
            {
                if (Ship.GetShips().Count <= 3)
                {
                    PlaceShipsInGrid(shipSize, direction, row, column);
                    ret = true;
                }
            }            
            return ret;
        }

        public Coordinates ConvertShipToCoords(int XCoordinate, int YCoordinate)
        {
            Coordinates coordinates = new Coordinates()
            {
                XCoordinate = XCoordinate,
                YCoordinate = YCoordinate
            };
            return coordinates;
        }

        private bool KoordinatenAusserhalb(Coordinates coordinate, int shipSize, ShipDirection direction)
        {            
            int xStart = 0, xEnd = felder.GetLength(0), yStart = 0, yEnd = felder.GetLength(1);
            
            bool outerBound = false;
            
            if (direction == ShipDirection.Oben)
            {
                xStart = xStart + (shipSize - 1);
            }
            else if (direction == ShipDirection.Unten)
            {
                xEnd = xEnd - (shipSize - 1);
            }
            else if (direction == ShipDirection.Rechts)
            {
                yEnd = yEnd - (shipSize - 1);
            }
            else if (direction == ShipDirection.Links)
            {
                yStart = yStart + (shipSize - 1);
            }
            if (!(coordinate.XCoordinate >= xStart && coordinate.XCoordinate <= xEnd && coordinate.YCoordinate >= yStart && coordinate.YCoordinate <= yEnd))
            {
                outerBound = true;
                MessageBox.Show("Koordinate liegt außerhalb des Feldes");
            }
            return outerBound;
        }

        private bool KoordinatenBesetzt(Coordinates coordinates, int shipSize, ShipDirection direction)
        {
            int counter = 0;
            var besetzt = false;
            //return felder[coordinates.XCoordinate, coordinates.YCoordinate].ShipIsSet;
            while (!besetzt && counter < shipSize)
            {                
                if (direction == ShipDirection.Links)
                {
                    besetzt = felder[coordinates.XCoordinate, coordinates.YCoordinate - counter].ShipIsSet;
                }
                else if (direction == ShipDirection.Rechts)
                {
                    besetzt = felder[coordinates.XCoordinate, coordinates.YCoordinate + counter].ShipIsSet;
                }
                else if (direction == ShipDirection.Oben)
                {
                    besetzt = felder[coordinates.XCoordinate - counter, coordinates.YCoordinate].ShipIsSet;
                }
                else if (direction == ShipDirection.Unten)
                {
                    besetzt = felder[coordinates.XCoordinate + counter, coordinates.YCoordinate].ShipIsSet;
                }
                counter++;
            }
            return besetzt;
        }

        public void PlaceShipsInGrid(int shipSize, ShipDirection direction, int Row, int Column)
        {
            var coordinate = ConvertShipToCoords(Row, Column);
            if (shipSize <= 1 && KoordinatenAusserhalb(coordinate, shipSize, direction))
            {
                return;
            }
            while (shipSize >= 1)                
            {
                felder[Row, Column].ShipIsSet = true;
                if (direction == ShipDirection.Links)
                {                    
                    Column--;
                }
                else if (direction == ShipDirection.Rechts)
                {                    
                    Column++;
                }
                else if (direction == ShipDirection.Oben)
                {                    
                    Row--;
                }               
                else
                {                    
                    Row++;
                }
                shipSize--;
            }
        }
    }
}
