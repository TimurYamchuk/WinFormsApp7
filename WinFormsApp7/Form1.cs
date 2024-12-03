// Form1.cs
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public partial class Form1 : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private bool isPlayerXTurn;
        private RadioButton easyModeButton;
        private RadioButton hardModeButton;
        private CheckBox firstMoveCheckbox;
        private Button newGameButton;

        public Form1()
        {
            InitializeComponent(); // Вызов метода InitializeComponent
        }

        private void InitializeGame()
        {
            isPlayerXTurn = firstMoveCheckbox.Checked; // Определяем, кто ходит первым

            foreach (var button in buttons)
            {
                button.Text = "";
                button.Enabled = true;
                button.BackColor = SystemColors.Control;
            }

            if (!isPlayerXTurn)
            {
                MakeAIMove();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null && string.IsNullOrEmpty(clickedButton.Text))
            {
                clickedButton.Text = isPlayerXTurn ? "X" : "O";
                clickedButton.Enabled = false;

                if (CheckWinCondition())
                {
                    MessageBox.Show($"{(isPlayerXTurn ? "X" : "O")} wins!", "Game Over");
                    DisableAllButtons();
                    return;
                }

                if (CheckDrawCondition())
                {
                    MessageBox.Show("Draw!", "Game Over");
                    return;
                }

                isPlayerXTurn = !isPlayerXTurn;

                if (!isPlayerXTurn)
                {
                    MakeAIMove();
                }
            }
        }

        private void MakeAIMove()
        {
            if (easyModeButton.Checked)
            {
                MakeRandomMove();
            }
            else if (hardModeButton.Checked)
            {
                MakeOptimalMove();
            }

            if (CheckWinCondition())
            {
                MessageBox.Show("O wins!", "Game Over");
                DisableAllButtons();
                return;
            }

            if (CheckDrawCondition())
            {
                MessageBox.Show("Draw!", "Game Over");
                return;
            }

            isPlayerXTurn = true; // После хода AI передаем очередь игроку
        }

        private void MakeRandomMove()
        {
            var emptyButtons = buttons.Cast<Button>().Where(b => string.IsNullOrEmpty(b.Text)).ToList();
            if (emptyButtons.Any())
            {
                var random = new Random();
                var button = emptyButtons[random.Next(emptyButtons.Count)];
                button.Text = "O";
                button.Enabled = false;
            }
        }

        private void MakeOptimalMove()
        {
            for (int i = 0; i < 3; i++)
            {
                if (TryCompleteLine(buttons[i, 0], buttons[i, 1], buttons[i, 2])) return;
                if (TryCompleteLine(buttons[0, i], buttons[1, i], buttons[2, i])) return;
            }

            if (TryCompleteLine(buttons[0, 0], buttons[1, 1], buttons[2, 2])) return;
            if (TryCompleteLine(buttons[0, 2], buttons[1, 1], buttons[2, 0])) return;

            MakeRandomMove();
        }

        private bool TryCompleteLine(Button b1, Button b2, Button b3)
        {
            if (b1.Text == "O" && b2.Text == "O" && string.IsNullOrEmpty(b3.Text))
            {
                b3.Text = "O";
                b3.Enabled = false;
                return true;
            }

            if (b1.Text == "O" && b3.Text == "O" && string.IsNullOrEmpty(b2.Text))
            {
                b2.Text = "O";
                b2.Enabled = false;
                return true;
            }

            if (b2.Text == "O" && b3.Text == "O" && string.IsNullOrEmpty(b1.Text))
            {
                b1.Text = "O";
                b1.Enabled = false;
                return true;
            }

            if (b1.Text == "X" && b2.Text == "X" && string.IsNullOrEmpty(b3.Text))
            {
                b3.Text = "O";
                b3.Enabled = false;
                return true;
            }

            if (b1.Text == "X" && b3.Text == "X" && string.IsNullOrEmpty(b2.Text))
            {
                b2.Text = "O";
                b2.Enabled = false;
                return true;
            }

            if (b2.Text == "X" && b3.Text == "X" && string.IsNullOrEmpty(b1.Text))
            {
                b1.Text = "O";
                b1.Enabled = false;
                return true;
            }

            return false;
        }

        private bool CheckWinCondition()
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text == buttons[i, 1].Text && buttons[i, 1].Text == buttons[i, 2].Text && !string.IsNullOrEmpty(buttons[i, 0].Text))
                    return true;
                if (buttons[0, i].Text == buttons[1, i].Text && buttons[1, i].Text == buttons[2, i].Text && !string.IsNullOrEmpty(buttons[0, i].Text))
                    return true;
            }

            if (buttons[0, 0].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 2].Text && !string.IsNullOrEmpty(buttons[0, 0].Text))
                return true;

            if (buttons[0, 2].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 0].Text && !string.IsNullOrEmpty(buttons[0, 2].Text))
                return true;

            return false;
        }

        private bool CheckDrawCondition()
        {
            return buttons.Cast<Button>().All(b => !string.IsNullOrEmpty(b.Text)) && !CheckWinCondition();
        }

        private void DisableAllButtons()
        {
            foreach (var button in buttons)
            {
                button.Enabled = false;
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }
    }
}
