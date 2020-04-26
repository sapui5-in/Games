using Ludo.UI.Class.Controls;
using Ludo.UI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo.UI.Class
{
    public class QuadrantRenderer : Renderer
    {
        private bool Flag = false;

        public Quadrant Quadrant;
        public QuadrantHomeRenderer QuadrantHomeRenderer;

        private int ColumnCount;
        private int RowCount;
        private int CellSize;
        private int XPosition;
        private int YPosition;

        private int ContainerXPosition;
        private int ContainerYPosition;
        private int BoardSize;
        private Color Color;

        private System.Windows.Forms.Timer timer1;

        public TableLayoutPanel MainTableLayout;
        public Panel QuadrantHome;
        public Panel Container;

        private bool active = true;
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                if (active)
                {
                    this.timer1.Start();
                }
                else
                {
                    this.QuadrantHome.BackColor = Utils.Util.GetDrawingColor(Color);
                    this.timer1.Stop();
                }
            }
        }

        public QuadrantRenderer(Quadrant quadrant)
        {
            Quadrant = quadrant;
            QuadrantHomeRenderer = new QuadrantHomeRenderer(Quadrant.QuadrantHome);
            this.SetInitialParameters(quadrant.BoardSize, Quadrant.QuadrantPosition);
            this.Renderer(Quadrant.QuadrantPosition);


            this.timer1 = new System.Windows.Forms.Timer();
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }

        private void SetInitialParameters(int boardSize, int quadrantPos)
        {
            this.BoardSize = boardSize;
            this.CellSize = this.BoardSize / 15;
            if (quadrantPos == 0)
            {
                XPosition = 0;
                YPosition = 1;

                ContainerXPosition = 0;
                ContainerYPosition = 8 * this.CellSize;

                ColumnCount = 8;
                RowCount = 7;

                Color = Color.Red;
            }
            else if (quadrantPos == 1)
            {
                XPosition = 0;
                YPosition = 0;

                ContainerXPosition = 0;
                ContainerYPosition = 0;

                ColumnCount = 7;
                RowCount = 8;

                Color = Color.Green;
            }
            else if (quadrantPos == 2)
            {
                XPosition = 1;
                YPosition = 0;

                ContainerXPosition = 7 * this.CellSize;
                ContainerYPosition = 0;

                ColumnCount = 8;
                RowCount = 7;

                Color = Color.Blue;
            }
            else
            {
                XPosition = 1;
                YPosition = 1;

                ContainerXPosition = 8 * this.CellSize;
                ContainerYPosition = 7 * this.CellSize;

                ColumnCount = 7;
                RowCount = 8;

                Color = Color.Yellow;
            }
        }

        private void Renderer(int quadrantPos)
        {
            this.CreateContainer();
            this.QuadrantHomeRender();
            this.CreateMainTableLayout();
            this.GhorRenderer(quadrantPos);
            this.QuadrantHome.Controls.Add(QuadrantHomeRenderer.Container);

            this.Container.Controls.Add(this.QuadrantHome);
            this.Container.Controls.Add(this.MainTableLayout);
            //this.MainTableLayout.ResumeLayout(false);
            //this.QuadrantHome.ResumeLayout(false);
        }

        private void CreateContainer()
        {
            this.Container = new Panel();
            this.Container.Anchor = ((AnchorStyles)(((AnchorStyles.Top
                | AnchorStyles.Bottom) | AnchorStyles.Left)));
            this.Container.Size = new System.Drawing.Size(this.CellSize * this.ColumnCount + 1, this.CellSize * this.RowCount + 1);
            this.Container.Margin = new Padding(0);

            this.Container.Location = new System.Drawing.Point(9 + ContainerXPosition, 9 + ContainerYPosition);
        }

        private void CreateMainTableLayout()
        {
            this.MainTableLayout = new TableLayoutPanel();

            this.MainTableLayout.Anchor = ((AnchorStyles)(((AnchorStyles.Top
                | AnchorStyles.Bottom) | (AnchorStyles.Left | AnchorStyles.Right))));
            this.MainTableLayout.AutoSize = true;
            this.MainTableLayout.BackColor = System.Drawing.Color.White;
            this.MainTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayout.Margin = new Padding(0);

            this.CreateColumns();
            this.CreateRows();

            this.MainTableLayout.Size = new System.Drawing.Size(this.CellSize * this.ColumnCount + 1, this.CellSize * this.RowCount + 1);
        }

        private void CreateColumns()
        {
            this.MainTableLayout.ColumnCount = this.ColumnCount;

            for (int i = 0; i < this.ColumnCount; i++)
            {
                this.MainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, this.CellSize));
            }
        }

        private void CreateRows()
        {
            this.MainTableLayout.RowCount = this.RowCount;

            for (int i = 0; i < this.RowCount; i++)
            {
                this.MainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, this.CellSize));
            }
        }

        private void QuadrantHomeRender()
        {
            this.QuadrantHome = new Panel();
            //this.QuadrantHome.SuspendLayout();
            this.QuadrantHome.BackColor = Utils.Util.GetDrawingColor(Color);

            int xPos = XPosition * (this.ColumnCount - 6) * this.CellSize;
            int yPos = YPosition * (this.RowCount - 6) * this.CellSize;

            this.QuadrantHome.Location = new System.Drawing.Point(xPos + 1, yPos + 1);
            this.QuadrantHome.Margin = new Padding(0);
            this.QuadrantHome.Size = new System.Drawing.Size(6 * this.CellSize - 1, 6 * this.CellSize - 1);
        }

        private void GhorRenderer(int quadrantPos)
        {
            if (quadrantPos == 0)
            {
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(0).UIControl, 6, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(1).UIControl, 6, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(2).UIControl, 6, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(3).UIControl, 6, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(4).UIControl, 6, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(5).UIControl, 6, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(6).UIControl, 5, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(7).UIControl, 4, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(8).UIControl, 3, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(9).UIControl, 2, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(10).UIControl, 1, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(11).UIControl, 0, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(12).UIControl, 7, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(13).UIControl, 7, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(14).UIControl, 7, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(15).UIControl, 7, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(16).UIControl, 7, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(17).UIControl, 7, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(18).UIControl, 7, 0);
            }
            if (quadrantPos == 1)
            {
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(0).UIControl, 0, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(1).UIControl, 1, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(2).UIControl, 2, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(3).UIControl, 3, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(4).UIControl, 4, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(5).UIControl, 5, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(6).UIControl, 6, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(7).UIControl, 6, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(8).UIControl, 6, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(9).UIControl, 6, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(10).UIControl, 6, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(11).UIControl, 6, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(12).UIControl, 0, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(13).UIControl, 1, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(14).UIControl, 2, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(15).UIControl, 3, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(16).UIControl, 4, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(17).UIControl, 5, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(18).UIControl, 6, 7);
            }
            if (quadrantPos == 2)
            {
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(0).UIControl, 1, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(1).UIControl, 1, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(2).UIControl, 1, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(3).UIControl, 1, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(4).UIControl, 1, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(5).UIControl, 1, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(6).UIControl, 2, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(7).UIControl, 3, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(8).UIControl, 3, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(9).UIControl, 4, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(10).UIControl, 5, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(11).UIControl, 6, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(12).UIControl, 0, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(13).UIControl, 0, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(14).UIControl, 0, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(15).UIControl, 0, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(16).UIControl, 0, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(17).UIControl, 0, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(18).UIControl, 0, 6);
            }
            if (quadrantPos == 3)
            {
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(0).UIControl, 6, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(1).UIControl, 5, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(2).UIControl, 4, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(3).UIControl, 3, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(4).UIControl, 2, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(5).UIControl, 1, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(6).UIControl, 0, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(7).UIControl, 0, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(8).UIControl, 0, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(9).UIControl, 0, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(10).UIControl, 0, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(11).UIControl, 0, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(12).UIControl, 6, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(13).UIControl, 5, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(14).UIControl, 4, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(15).UIControl, 3, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(16).UIControl, 2, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(17).UIControl, 1, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(18).UIControl, 0, 0);
            }
        }

        private Ghor GetQuadrantGhor(int position)
        {
            Ghor ghor = this.Quadrant.GetGhorByPosition(position);
            ghor.UIControl.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left) | AnchorStyles.Right)));
            if (position == 1 || position >= 13)
            {
                ghor.UIControl.BackColor = Utils.Util.GetDrawingColor(Color);
            }
            else
            {
                ghor.UIControl.BackColor = System.Drawing.Color.White;
            }
            ghor.UIControl.Margin = new System.Windows.Forms.Padding(0);
            ghor.UIControl.Size = new System.Drawing.Size(49, 49);

            Label label = new System.Windows.Forms.Label();
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(18, 18);
            label.Text = ghor.Position.ToString();

            ghor.UIControl.Controls.Add(label);

            return ghor;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            ToggleBlink();
        }

        private void ToggleBlink()
        {
            if (Active)
            {
                if (Flag)
                {
                    this.QuadrantHome.BackColor = Utils.Util.GetDarkDrawingColor(Color);
                }
                else
                {
                    this.QuadrantHome.BackColor = Utils.Util.GetDrawingColor(Color);
                }
                Flag = !Flag;
            }
        }
    }
}