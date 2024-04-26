using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpRabbit
{
    public class Map
    {
        public PictureBox PictureBackground { get; private set; }

        public Map()
        {
            PictureBackground = new PictureBox();
            PictureBackground.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void SetBackground(string Background)
        {
            Image backgr = Image.FromFile(Background);
            PictureBackground.Image = backgr;
        }

        public void UpdateBackgroundSize(Size newSize)
        {
            PictureBackground.Size = newSize;
        }
    }
}

