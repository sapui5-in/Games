using Ludo.UI.Enum;
using System.Windows.Forms;

namespace Ludo.UI.Class.Controls
{
    public class Ghor
    {
        public GhorRenderer UIControl = new GhorRenderer();

        public int Position;
        public GhorType GhorType;

        public Ghor(int position)
        {
            Position = position;
        }

        public Ghor(int position, GhorType ghorType)
        {
            Position = position;
            GhorType = ghorType;
        }
    }
}
