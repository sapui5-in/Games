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
        private System.Drawing.Color Color;

        public TableLayoutPanel MainTableLayout;
        public Panel QuadrantHome;
        public Panel Container;

        public QuadrantRenderer(Quadrant quadrant)
        {
            Quadrant = quadrant;
            QuadrantHomeRenderer = new QuadrantHomeRenderer(Quadrant.QuadrantHome);
            this.SetInitialParameters(quadrant.BoardSize, Quadrant.QuadrantPosition);
            this.Renderer(Quadrant.QuadrantPosition);
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

                Color = System.Drawing.Color.Red;
            }
            else if (quadrantPos == 1)
            {
                XPosition = 0;
                YPosition = 0;

                ContainerXPosition = 0;
                ContainerYPosition = 0;

                ColumnCount = 7;
                RowCount = 8;

                Color = System.Drawing.Color.Green;
            }
            else if (quadrantPos == 2)
            {
                XPosition = 1;
                YPosition = 0;

                ContainerXPosition = 7 * this.CellSize;
                ContainerYPosition = 0;

                ColumnCount = 8;
                RowCount = 7;

                Color = System.Drawing.Color.Blue;
            }
            else
            {
                XPosition = 1;
                YPosition = 1;

                ContainerXPosition = 8 * this.CellSize;
                ContainerYPosition = 7 * this.CellSize;

                ColumnCount = 7;
                RowCount = 8;

                Color = System.Drawing.Color.Yellow;
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
            this.Container.Name = "Container";
            this.Container.Margin = new Padding(0);

            this.Container.Location = new System.Drawing.Point(9 + ContainerXPosition, 9 + ContainerYPosition);
            //this.Container.SuspendLayout();
        }

        private void CreateMainTableLayout()
        {
            this.MainTableLayout = new TableLayoutPanel();
            //this.MainTableLayout.SuspendLayout();

            this.MainTableLayout.Anchor = ((AnchorStyles)(((AnchorStyles.Top
                | AnchorStyles.Bottom) | (AnchorStyles.Left | AnchorStyles.Right))));
            this.MainTableLayout.AutoSize = true;
            this.MainTableLayout.BackColor = System.Drawing.Color.White;
            this.MainTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayout.Margin = new Padding(0);
            this.MainTableLayout.Name = "MainTableLayout";

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
            this.QuadrantHome.BackColor = this.Color;

            int xPos = XPosition * (this.ColumnCount - 6) * this.CellSize;
            int yPos = YPosition * (this.RowCount - 6) * this.CellSize;

            this.QuadrantHome.Location = new System.Drawing.Point(xPos + 1, yPos + 1);
            this.QuadrantHome.Margin = new Padding(0);
            this.QuadrantHome.Name = "QuadrantHome";
            this.QuadrantHome.Size = new System.Drawing.Size(6 * this.CellSize - 1, 6 * this.CellSize - 1);
        }

        private void GhorRenderer(int quadrantPos)
        {
            if (quadrantPos == 0)
            {
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(0), 6, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(1), 6, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(2), 6, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(3), 6, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(4), 6, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(5), 6, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(6), 5, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(7), 4, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(8), 3, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(9), 2, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(10), 1, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(11), 0, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(12), 7, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(13), 7, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(14), 7, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(15), 7, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(16), 7, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(17), 7, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(18), 7, 0);
            }
            if (quadrantPos == 1)
            {
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(0), 0, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(1), 1, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(2), 2, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(3), 3, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(4), 4, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(5), 5, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(6), 6, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(7), 6, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(8), 6, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(9), 6, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(10), 6, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(11), 6, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(12), 0, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(13), 1, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(14), 2, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(15), 3, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(16), 4, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(17), 5, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(18), 6, 7);
            }
            if (quadrantPos == 2)
            {
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(0), 1, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(1), 1, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(2), 1, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(3), 1, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(4), 1, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(5), 1, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(6), 2, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(7), 3, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(8), 3, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(9), 4, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(10), 5, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(11), 6, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(12), 0, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(13), 0, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(14), 0, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(15), 0, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(16), 0, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(17), 0, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(18), 0, 6);
            }
            if (quadrantPos == 3)
            {
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(0), 6, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(1), 5, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(2), 4, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(3), 3, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(4), 2, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(5), 1, 1);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(6), 0, 2);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(7), 0, 3);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(8), 0, 4);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(9), 0, 5);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(10), 0, 6);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(11), 0, 7);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(12), 6, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(13), 5, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(14), 4, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(15), 3, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(16), 2, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(17), 1, 0);
                this.MainTableLayout.Controls.Add(this.GetQuadrantGhor(18), 0, 0);
            }
        }

        private Ghor GetQuadrantGhor(int position)
        {
            Ghor ghor = this.Quadrant.GetGhorByPosition(position);
            ghor.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left) | AnchorStyles.Right)));
            if (position == 1 || position >= 13)
            {
                ghor.BackColor = Color;
                if (ghor.Position == 18)
                {
                    ghor.Position = -1;
                }
            }
            else
            {
                ghor.BackColor = System.Drawing.Color.White;
            }
            ghor.Margin = new System.Windows.Forms.Padding(0);
            ghor.Size = new System.Drawing.Size(49, 49);

            Label label = new System.Windows.Forms.Label();
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(18, 18);
            label.Text = ghor.Position.ToString();

            ghor.Controls.Add(label);

            return ghor;
        }
    }
}