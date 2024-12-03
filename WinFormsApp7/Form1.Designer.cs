// Form1.Designer.cs
using System.Windows.Forms;

namespace TicTacToeGame
{
    partial class Form1
    {
        private void InitializeComponent()
        {
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Text = "Крестики-нолики";

            buttons = new Button[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Size = new System.Drawing.Size(100, 100),
                        Location = new System.Drawing.Point(100 * j, 100 * i),
                        Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold)
                    };
                    buttons[i, j].Click += Button_Click;
                    this.Controls.Add(buttons[i, j]);
                }
            }

            easyModeButton = new RadioButton
            {
                Text = "Легкий",
                Location = new System.Drawing.Point(10, 320),
                Checked = true
            };
            this.Controls.Add(easyModeButton);

            hardModeButton = new RadioButton
            {
                Text = "Сложный",
                Location = new System.Drawing.Point(10, 350)
            };
            this.Controls.Add(hardModeButton);

            firstMoveCheckbox = new CheckBox
            {
                Text = "Первый ход X",
                Location = new System.Drawing.Point(10, 380),
                Checked = true
            };
            this.Controls.Add(firstMoveCheckbox);

            newGameButton = new Button
            {
                Text = "Новая игра",
                Location = new System.Drawing.Point(150, 350),
                Size = new System.Drawing.Size(100, 50)
            };
            newGameButton.Click += NewGameButton_Click;
            this.Controls.Add(newGameButton);

            InitializeGame();
        }
    }
}
