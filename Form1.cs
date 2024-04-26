using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace ExpRabbit
{
    public partial class Form1 : Form
    {
        private Map background;
        //private Rabbit rabbit;
        private ModelRabbit rabbitModel;
        private ViewRabbit rabbitView;
        private ControllerRabbit rabbitController;
        public Form1()
        {
            InitializeComponent();
            InitializeRabbit();
            InitializeEventHandlers();
            //InitializeBackground();
            //SetImageToPictureBox();
            //this.SizeChanged += Form1_SizeChanged;
            //rabbit = new Rabbit();
            //// Добавляем изображение зайца на форму
            //Controls.Add(rabbit.GetPictureBox());
            //// Устанавливаем начальные параметры для зайца
            //rabbit.GetPictureBox().BringToFront(); // Перемещаем зайца на передний план
            //rabbit.GetPictureBox().Location = new Point(100, 500); // Помещаем зайца в указанную точку на форме
            //rabbit.GetPictureBox().BackColor = Color.Transparent;
            //KeyDown += Form1_KeyDown;
            //KeyUp += Form1_KeyUp;
        }

        private void InitializeRabbit()
        {
            rabbitModel = new ModelRabbit();
            rabbitView = new ViewRabbit();
            rabbitController = new ControllerRabbit(rabbitModel, rabbitView);

            // Создаем PictureBox для зайца
            PictureBox pictureBox = rabbitView.GetPictureBox();
            pictureBox.Size = new Size(130, 240);
            pictureBox.Location = new Point(100, 500);
            pictureBox.BackColor = Color.Transparent;
            Controls.Add(pictureBox);
        }

        private void InitializeEventHandlers()
        {
            // Обработчики клавиш
            KeyDown += RabbitForm_KeyDown;
            KeyUp += RabbitForm_KeyUp;
        }

        private void RabbitForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    rabbitController.StartMoveRight();
                    break;
                case Keys.A:
                    rabbitController.StartMoveLeft();
                    break;
                case Keys.Space:
                    rabbitController.Jump();
                    break;
            }
        }

        private void RabbitForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    rabbitController.StopMoveRight();
                    break;
                case Keys.A:
                    rabbitController.StopMoveLeft();
                    break;
            }
        }

        //private void Form1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.D:
        //            rabbit.StartMoveRight();
        //            break;
        //        case Keys.A:
        //            rabbit.StartMoveLeft();
        //            break;
        //    }
        //}

        //private void Form1_KeyUp(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.D:
        //            rabbit.StopMoveRight();
        //            break;
        //        case Keys.A:
        //            rabbit.StopMoveLeft();
        //            break;
        //        case Keys.Space:
        //            rabbit.Jump();
        //            break;
        //    }
        //}
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //private void InitializeBackground()
        //{
        //    background = new Map();
        //    Controls.Add(background.PictureBackground);
        //}

        //private void SetImageToPictureBox()
        //{
        //    string Background = "C:\\Users\\LiVlQ\\Downloads\\background.jpg";
        //    background.SetBackground(Background);
        //    background.UpdateBackgroundSize(this.ClientSize);
        //}

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            background.UpdateBackgroundSize(this.ClientSize);
        }
    }
}

