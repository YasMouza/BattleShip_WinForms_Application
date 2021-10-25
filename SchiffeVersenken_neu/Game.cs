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
        Button currentButtonPlayer;
        Button currentButtonComputer;
        Field playerField;
        Field computerField;        
        bool player = true;


        public Game()
        {
            InitializeComponent();            
            GeneriereSpielfeld();
            playerField = new Field(BoardPlayerButton, player);
            player = false;
            computerField = new Field(BoardComputerButton, player);
        }

        private void Game_Load(object sender, EventArgs e)
        {             
            

        }

        private void MarkiereFieldPlayer()  //Farbänderung der Felder des Spielers bei Attacke des Computers
        {
            for (int iX = 0; iX < playerField.FieldArray().GetLength(0); iX++)
            {
                for (int iY = 0; iY < playerField.FieldArray().GetLength(1); iY++)
                {
                    if (playerField.FieldArray()[iX, iY].ShipIsSet)
                    {
                        currentButtonPlayer.BackColor = Color.Orange;
                        BoardPlayerButton[iX, iY].BackColor = Color.Orange;
                    }
                    if (playerField.FieldArray()[iX, iY].ShipIsHit)
                    {
                        BoardPlayerButton[iX, iY].BackColor = Color.Red;
                    }
                    else if (playerField.FieldArray()[iX, iY].FieldIsHit)
                    {
                        BoardPlayerButton[iX, iY].BackColor = Color.LightBlue;
                    }
                }
            }
        }

        public void ColorComputerField()
        {
            
            currentButtonComputer.BackColor = Color.Orange;
            
            for (int iX = 0; iX < computerField.FieldArray().GetLength(0); iX++)
            {
                for (int iY = 0; iY < computerField.FieldArray().GetLength(1); iY++)
                {
                    //bool ShipIsHit = computerField.FieldArray()[iX, iY].ShipIsHit == false;
                    //if (BoardComputerButton[iX, iY].BackColorChanged = computerField.FieldArray()[iX, iY].ShipIsHit == true)
                    //{
                    //    BoardComputerButton[iX,iY].BackColor = Color.Red;
                    //}
                    //if (computerField.FieldArray()[iX,iY].ShipIsHit)
                    //{
                    //    currentButtonComputer.BackColor = Color.Orange;
                    //    BoardComputerButton[iX, iY].BackColor = Color.Orange;
                    //}

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

            for (int iX = 0; iX < BoardComputerButton.GetLength(0); iX++)
            {
                for (int iY = 0; iY < BoardComputerButton.GetLength(1); iY++)
                {
                    BoardComputerButton[iX, iY].Click += new EventHandler(SelectShipFire);
                }
            }
            for (int iX = 0; iX < BoardPlayerButton.GetLength(0); iX++)
            {
                for (int iY = 0; iY < BoardPlayerButton.GetLength(1); iY++)
                {
                    BoardPlayerButton[iX, iY].Click += new EventHandler(BestimmeSchiffe);
                }
            }
        }


        public void BestimmeSchiffe(object sender, EventArgs e)
        {
            currentButtonPlayer = sender as Button;
            GewaehlterButton.Text = currentButtonPlayer.Text; 
        }

        public void SetzeSpielerSchiffe()
        {

            var direction = (ShipDirection)Enum.Parse(typeof(ShipDirection), directionBox.Text);
            bool player = true;
            for (int iY = 0; iY < BoardPlayerButton.GetLength(0); iY++)
            {
                for (int iX = 0; iX < BoardPlayerButton.GetLength(1); iX++)
                {
                    if (BoardPlayerButton[iX, iY] == currentButtonPlayer)
                    {                        
                        playerField.SetShip(playerField.FieldArray(),
                            Int16.Parse(sizeBox.Text),
                            direction, iX, iY, player = true);
                        MarkiereFieldPlayer();
                        SetShipComputer();
                                         

                    }
                }
            }
        }
        private void SetzeSchiffeButton_Click(object sender, EventArgs e)
        {
            SetzeSpielerSchiffe();
        }


        public void SetShipComputer()
        {
            computerField.SetComputerShip();         
            
        }

        private void Attack_Click(object sender, EventArgs e)
        {
            AttackiereComputer();
            AttackierePlayer();            
            ColorComputerField();
            //currentButtonComputer.BackColor = Color.Orange;
            
        }



        public void AttackierePlayer()
        {
            playerField.SpielerAngreifen();
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
                        computerField.FireComputersShip(iX, iY);
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
        }

        private void ResetGameButton_Click(object sender, EventArgs e)
        {
            
            directionBox.Controls.Clear();
            sizeBox.Refresh();
            AngriffsKoordinate.Controls.Clear();
            AngriffsKoordinate.Refresh();
            GewaehlterButton.Controls.Clear();
            
            for (int iX = 0; iX < BoardComputerButton.GetLength(0); iX++)
            {
                for (int iY = 0; iY < BoardComputerButton.GetLength(1); iY++)
                {
                   BoardComputerButton[iX, iY].BackColor = Color.Transparent; 
                }
            }
            for (int iX = 0; iX < BoardPlayerButton.GetLength(0); iX++)
            {
                for (int iY = 0; iY < BoardPlayerButton.GetLength(1); iY++)
                {
                    BoardPlayerButton[iX, iY].BackColor = Color.Transparent;
                }
            }
            this.Refresh();       

        }


    }
}
