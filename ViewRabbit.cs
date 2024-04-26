using System;
using System.Drawing;
using System.Windows.Forms;
using ExpRabbit.Properties;

namespace ExpRabbit
{
    public class ViewRabbit
    {
        public PictureBox BoxRabbit;

        // Массивы изображений для анимации
        public Image[] idleFrames;
        public Image[] rightFrames;
        public Image[] leftFrames;
        public Image[] jumpImage;

        // Текущий индекс кадра анимации
        public int currentFrameIndex = 0;

        public ViewRabbit()
        {
            // Создание PictureBox для зайца
            BoxRabbit = new PictureBox();
            BoxRabbit.Size = new Size(130, 240);
            BoxRabbit.Location = new Point(100, 690);
            BoxRabbit.Image = Resources.idleFrames;
            BoxRabbit.BackgroundImageLayout = ImageLayout.None;

            // Движение вправо
            rightFrames = new Image[]
            {
                Resources._053,
                Resources._054,
                Resources._055,
                Resources._056,
                Resources._057,
                Resources._058,
            };

            leftFrames = new Image[]
            {
                Resources._053л,
                Resources._054л,
                Resources._055л,
                Resources._056л,
                Resources._057л,
                Resources._058л,
            };

            idleFrames = new Image[]
            {
                Resources._1R,
                Resources._2R
            };

            jumpImage = new Image[]
            {
                Resources._003,
                Resources._002
            };
        }
        public PictureBox GetPictureBox()
        {
            return BoxRabbit;
        }
    }
}
