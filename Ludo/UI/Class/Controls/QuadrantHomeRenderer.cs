using Ludo.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ludo.UI.Class.Controls
{
    public class QuadrantHomeRenderer : Renderer
    {
        public Panel Container;
        public QuadrantHome QuadrantHome;

        public QuadrantHomeRenderer(QuadrantHome quadrantHome)
        {
            QuadrantHome = quadrantHome;
            this.Renderer();
        }

        private void Renderer ()
        {
            Container = this.RenderOuterContainer();
            Container.Controls.Add(this.RenderInnerContainer());
        }

        private Panel RenderOuterContainer()
        {
            Panel panel = new Panel();
            panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            panel.BackColor = System.Drawing.Color.White;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel.Location = new System.Drawing.Point(50, 50);
            panel.Size = new System.Drawing.Size(200, 200);

            return panel;
        }

        private Panel RenderInnerContainer()
        {
            Panel panel = new Panel();
            panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            panel.Location = new System.Drawing.Point(25, 25);
            panel.Size = new System.Drawing.Size(150, 150);
            panel.Controls.Add(this.RenderInnerGrid());

            return panel;
        }

        private TableLayoutPanel RenderInnerGrid()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();

            tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            tableLayoutPanel.BackColor = System.Drawing.Color.White;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel.Size = new System.Drawing.Size(149, 149);

            tableLayoutPanel.Controls.Add(this.GetQuadrantGhor(1), 0, 0);
            tableLayoutPanel.Controls.Add(this.GetQuadrantGhor(2), 2, 0);
            tableLayoutPanel.Controls.Add(this.GetQuadrantGhor(0), 0, 2);
            tableLayoutPanel.Controls.Add(this.GetQuadrantGhor(3), 2, 2);

            return tableLayoutPanel;
        }

        private Ghor GetQuadrantGhor(int position)
        {
            Ghor ghor = this.QuadrantHome.GetGhorByPosition(position);
            ghor.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left) | AnchorStyles.Right)));

            System.Drawing.Color color;

            color = Util.GetDrawingColor(QuadrantHome.Color);

            ghor.BackColor = color;
            ghor.Margin = new System.Windows.Forms.Padding(0);
            ghor.Size = new System.Drawing.Size(49, 49);
            ghor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            Label label = new System.Windows.Forms.Label();
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(18, 18);
            label.Text = ghor.Position.ToString();

            ghor.Controls.Add(label);

            return ghor;
        }
    }
}
