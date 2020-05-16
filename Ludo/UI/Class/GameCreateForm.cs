using Ludo.API.Model;
using Ludo.Services;
using Ludo.UI.Enum;
using System;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public partial class GameCreateForm : Form
    {
        GameService GameService;
        LudoContext LudoContext = new LudoContext();

        public GameCreateForm()
        {
            InitializeComponent();
            CreateGame();
        }

        private void CreateGame()
        {
            GameService = new GameService(LudoContext);
            ServiceResponse response = GameService.Create();

            this.GameId.Text = (response.Data as Ludo.API.Model.Game).Id.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPlayer(player2.Text, Color.Green);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddPlayer(player3.Text, Color.Blue);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddPlayer(player4.Text, Color.Yellow);
        }

        private void AddPlayer(string playerId, Color color)
        {
            if (playerId != "")
            {
                GameService.AddPlayer(Convert.ToInt32(playerId), Utils.Util.GetQuadrantFromColor(color));
            }
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            //Handle Error
            ServiceResponse serviceResponse = GameService.Start();
            GameBoardForm gameBoardForm = new GameBoardForm();

            gameBoardForm.Show();

            gameBoardForm.Start((serviceResponse.Data as GameDetails).Game.Id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GameBoardForm gameBoardForm = new GameBoardForm();

            gameBoardForm.Show();

            gameBoardForm.Start(Convert.ToInt32(GameId.Text));
        }
    }
}
