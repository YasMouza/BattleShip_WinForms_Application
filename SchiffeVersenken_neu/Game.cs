using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchiffeVersenken_neu.Properties;
using SchiffeVersenken_neu;
using System.Diagnostics;
using SchiffeVersenken_neu.Logik;
using SchiffeVersenken_neu.Datenklasse;

namespace SchiffeVersenken_neu
{
    public partial class Game : Form
    {
        public Button[,] BoardPlayerButton;
        public Button[,] BoardComputerButton;
        Button currentButton;
        Button[,] completeField;
        Button attackiertesFeld;
        Button currentButtonPlayer;
        Button currentButtonComputer;
        public Field playerField;
        public Field computerField;
        Field felder;
        int reihenAnzahl;
        int spaltenAnzahl;
        int yCoordinate;
        int xCoordinate;
        //Coordinates coordinate;
        int shipSize;
        private bool saved = false;

        public Game()
        {
            InitializeComponent();
            GeneriereSpielfeld();
            playerField = new Field(6, 6);
            computerField = new Field(6, 6);
            felder = new Field(6, 6);
        }

        //private void Game_Load(object sender, EventArgs e)
        //{

        //}

        private void MarkiereFieldPlayer()  //Farbänderung der Felder des Spielers bei Attacke des Computers
        {

            for (int xCoordinate = 0; xCoordinate < playerField.GetFieldArray().GetLength(0); xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < playerField.GetFieldArray().GetLength(1); yCoordinate++)
                {

                    if (playerField.GetFieldArray()[xCoordinate, yCoordinate].ShipIsSet)
                    {
                        currentButtonPlayer.BackColor = Color.Orange;
                        BoardPlayerButton[xCoordinate, yCoordinate].BackColor = Color.Orange;
                    }
                    if (playerField.GetFieldArray()[xCoordinate, yCoordinate].ShipIsHit)
                    {
                        BoardPlayerButton[xCoordinate, yCoordinate].BackColor = Color.Red;
                    }
                    if (playerField.GetFieldArray()[xCoordinate, yCoordinate].FieldIsHit)
                    {
                        BoardPlayerButton[xCoordinate, yCoordinate].BackColor = Color.LightBlue;
                    }
                }
            }
        }

        public void GeneriereSpielfeld()
        {
            BoardPlayerButton = new Button[6, 6] { { A1, B1, C1, D1, E1, F1 },
                { A2, B2, C2, D2, E2, F2 },
                { A3, B3, C3, D3, E3, F3 },
                { A4, B4, C4, D4, E4, F4 },
                { A5, B5, C5, D5, E5, F5 },
                { A6, B6, C6, D6, E6, F6 } };

            BoardComputerButton = new Button[6, 6] { { U1, U2, U3, U4, U5, U6 },
                        { V1, V2, V3, V4, V5, V6 },
                        { W1, W2, W3, W4, W5, W6 },
                        { X1, X2, X3, X4, X5, X6 },
                        { Y1, Y2, Y3, Y4, Y5, Y6 },
                        { Z1, Z2, Z3, Z4, Z5, Z6 } };

            for (int xCoordinate = 0; xCoordinate < BoardComputerButton.GetLength(0); xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < BoardComputerButton.GetLength(1); yCoordinate++)
                {
                    BoardComputerButton[xCoordinate, yCoordinate].Click += new EventHandler(SelectShipFire);
                }
            }
            for (int xCoordinate = 0; xCoordinate < BoardPlayerButton.GetLength(0); xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < BoardPlayerButton.GetLength(1); yCoordinate++)
                {
                    BoardPlayerButton[xCoordinate, yCoordinate].Click += new EventHandler(BestimmeSchiffe);
                }
            }
        }

        //private bool currentButtonPlayerExists(Button currentButtonPlayer, Button[,] BoardPlayerButton)
        //{
        //    foreach (var button in BoardPlayerButton)
        //    {
        //        if (button == currentButtonPlayer)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}


        //private Coordinates CoordinateOfPlayerButton(Button currentButtonPlayer, Button[,] BoardPlayerButton)
        //{
        //    var coordinates = new Coordinates()
        //    {
        //        XCoordinate = xCoordinate,
        //        YCoordinate = yCoordinate,
        //    };

        //    for (int x = 0; x < BoardPlayerButton.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < BoardPlayerButton.GetLength(1); y++)
        //        {
        //            if (currentButtonPlayer == BoardPlayerButton[x, y])
        //            {
        //                coordinates.XCoordinate = x;
        //                coordinates.YCoordinate = y;
        //            }
        //        }
        //    }
        //    return coordinates;
        //}

        //private Coordinates ConvertButtonstoCoordinates(Button currentButtonComputer, Button[,] BoardPlayerButton)
        //{
        //    if (currentButtonPlayerExists(currentButtonComputer, BoardPlayerButton))
        //    {
        //        return CoordinateOfPlayerButton(currentButtonComputer, BoardPlayerButton);
        //    }
        //}

        public void BestimmeSchiffe(object sender, EventArgs e)
        {
            currentButtonPlayer = sender as Button;
            GewaehlterButton.Text = currentButtonPlayer.Text;

            
            //for (int iX = 0; iX < playerField.GetFieldArray().GetLength(0); iX++)
            //{
            //    for (int iY = 0; iY < playerField.GetFieldArray().GetLength(1); iY++)
            //    {
            //        //int row = 0, column = 0;
            //        //Coordinates coords = new Coordinates()
            //        //{
            //        //    XCoordinate = row,
            //        //    YCoordinate = column
            //        //};
                    
            //        //textBox1.Text = row.ToString();
            //        //textBox2.Text = column.ToString();
            //        ////textBox1.Text = BoardPlayerButton[iX, iY].ToString();
            //        ////textBox1.Text = playerField.GetFieldArray()[iX, iY].ToString();
            //    }
            //}           

        }

        public void SetzeSpielerSchiffe()
        {
            
            if (sizeBox.Text != null && directionBox.Text != null && GewaehlterButton.Text != null)
            {
                var direction = (ShipDirection)Enum.Parse(typeof(ShipDirection), directionBox.Text); 
                if (TryFindeCoordinatesOfButton(direction, out Coordinates coordinates))
                {
                    if (playerField.TrySetShip(Int16.Parse(sizeBox.Text), direction, coordinates.XCoordinate, coordinates.YCoordinate))
                    {
                        MarkiereFieldPlayer();
                        SetShipComputer();
                    }
                    else
                    {
                        MessageBox.Show("Schiff darf hier nicht gesetzt werden");
                    }
                }
                //else
                //{
                //    MessageBox.Show("Die Koordinaten konnten nicht gefunden werden");
                //}
            }
            else
            {
                MessageBox.Show("Bitte gebe alle notwendigen Informationen ein, um dein Schiff zu platzieren");

            }

        }

        private bool TryFindeCoordinatesOfButton(ShipDirection direction, out Coordinates coordinates)
        {
            bool IsFound = false;
            coordinates = new Coordinates();

            for (int row = 0; row < BoardPlayerButton.GetLength(0); row++)
            {
                for (int column = 0; column < BoardPlayerButton.GetLength(1); column++)
                {
                    if (BoardPlayerButton[row, column] == currentButtonPlayer)
                    {
                        coordinates = new Coordinates()
                        {
                            XCoordinate = row,
                            YCoordinate = column
                        };
                        IsFound = true;
                        textBox1.Text = row.ToString();
                        textBox2.Text = column.ToString();
                    }
                }
            }

            return IsFound;
        }

        private void SetzeSchiffeButton_Click(object sender, EventArgs e)
        {
            SetzeSpielerSchiffe();
            //SchiffsAuswahl();
            try
            {
                _ = GewaehlterButton.Text == null;
            }
            catch (Exception)
            {

                MessageBox.Show("Bitte wähle einen Startpunkt, um Schiff zu setzen");
            }
            if (sizeBox.SelectedIndex == 0 || sizeBox.SelectedIndex == 1 || sizeBox.SelectedIndex == 2)
            {
                sizeBox.Items.Remove(sizeBox.SelectedItem);
            }
        }

        public void SetShipComputer()
        {
            computerField.SetComputerShip();
        }
        private void LosGehtsButton_Click(object sender, EventArgs e)
        {
            //VerfuegbareSchiffe();
            try
            {
                if (sizeBox.Items.Count < 1)
                {
                    for (int iX = 0; iX < 6; iX++)
                    {
                        for (int iY = 0; iY < 6; iY++)
                        {
                            BoardComputerButton[iY, iX].Enabled = true;
                            BoardPlayerButton[iX, iY].Enabled = false;
                            if (computerField.GetFieldArray()[iX, iY].ShipIsSet)
                            {
                                BoardComputerButton[iX, iY].BackColor = Color.DarkRed;
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Bitte setze drei Schiffe, um das Spiel zu starten");

            }


        }
        private void Attack_Click(object sender, EventArgs e)
        {
            AttackiereComputer();
            Coordinates coordinate = new Coordinates()
            {
                XCoordinate = xCoordinate,
                YCoordinate = yCoordinate,
            };


            AttackierePlayer(xCoordinate, yCoordinate, coordinate, shipSize);



        }
        private Coordinates CoordinateOfComputerAttack(Button AttackiertesFeld, Button[,] BoardPlayerButton)
        {
            var coordinates = new Coordinates()
            {
                XCoordinate = xCoordinate,
                YCoordinate = yCoordinate,
            };

            for (int x = 0; x < BoardPlayerButton.GetLength(0); x++)
            {
                for (int y = 0; y < BoardPlayerButton.GetLength(1); y++)
                {
                    if (AttackiertesFeld == BoardPlayerButton[x, y])
                    {
                        coordinates.XCoordinate = x;
                        coordinates.YCoordinate = y;

                    }
                }
            }
            return coordinates;
        }
        public void AttackierePlayer(int xCoordinate, int yCoordinate, Coordinates coordinate, int shipSize)
        {
            playerField.SpielerAngreifen(xCoordinate, yCoordinate, shipSize, coordinate);
            MarkiereFieldPlayer();
        }

        public void AttackiereComputer()
        {
            for (int iY = 0; iY < BoardComputerButton.GetLength(0); iY++)
            {
                for (int iX = 0; iX < BoardComputerButton.GetLength(1); iX++)
                {
                    if (BoardComputerButton[iX, iY] == currentButtonComputer)
                    {
                        Coordinates coordinate = new Coordinates()
                        {
                            XCoordinate = iX,
                            YCoordinate = iY,
                        };
                        computerField.ComputerAngreifen(coordinate);
                        if (computerField.GetFieldArray()[iX, iY].ShipIsHit)
                        {
                            currentButtonComputer.BackColor = Color.Red;
                        }
                        else
                        {
                            currentButtonComputer.BackColor = Color.Orange;
                        }
                    }
                }
            }
        }

        private void SelectShipFire(object sender, EventArgs e)
        {
            currentButtonComputer = sender as Button;
            AngriffsKoordinate.Text = currentButtonComputer.Text;
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            for (int iY = 0; iY < computerField.GetFieldArray().GetLength(0); iY++)
            {
                for (int iX = 0; iX < computerField.GetFieldArray().GetLength(1); iX++)
                {
                    BoardComputerButton[iY, iX].Enabled = false;
                }
            }
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void ResetGameButton_Click(object sender, EventArgs e)
        {
             
            if (MessageBox.Show("Do you want to reset the game?", "Reset Game",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DialogResult = DialogResult.Abort;
                saved = true;
                BoardComputerButton[xCoordinate, yCoordinate].BackColor = Color.Transparent;
                BoardPlayerButton[xCoordinate, yCoordinate].BackColor = Color.Transparent;
                ResetGame();
                Application.Restart(); 
            }
        }

        private void ResetGame()
        {
            var coordinates = new Coordinates()
            {
                XCoordinate = xCoordinate,
                YCoordinate = yCoordinate,
            };
            felder.BeendeSpiel(coordinates);
            saved = true;
            DialogResult = DialogResult.Abort;
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
