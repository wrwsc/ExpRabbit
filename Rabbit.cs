using System;
using System.Drawing;
using System.Windows.Forms;
using ExpRabbit.Properties;

namespace ExpRabbit
{
    public class Rabbit
    {
        private PictureBox BoxRabbit;
        private Timer MoveRight;
        private Timer MoveLeft;

        // Переменные для управления движением и прыжком
        private bool Right = false;
        private bool Left = false;
        private bool Jumping = false;
        private bool IsJumping = false;
        private int originalTop = 690; // Исходное положение зайца до прыжка

        // Параметры движения и прыжка
        private const int RabbitSpeed = 10;
        private int JumpSpeed = 40;
        private int Gravity = 20;
        private int JumpHeight = 200;
        private int JumpHorizontalSpeed = 20;

        // Массивы изображений для анимации
        private Image[] idleFrames;
        private Image[] rightFrames;
        private Image[] leftFrames;
        private Image[] jumpImage;

        // Текущий индекс кадра анимации
        private int currentFrameIndex = 0;

        public Rabbit()
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

            // Инициализация таймеров для движения
            MoveRight = new Timer();
            MoveRight.Interval = 10;
            MoveRight.Tick += MoveRight_Tick;

            MoveLeft = new Timer();
            MoveLeft.Interval = 10;
            MoveLeft.Tick += MoveLeft_Tick;
        }

        // Таймер для движения вправо
        private void MoveRight_Tick(object sender, EventArgs e)
        {
            if (!Jumping)
            {
                currentFrameIndex = (currentFrameIndex + 1) % rightFrames.Length;
                BoxRabbit.Image = rightFrames[currentFrameIndex];

                if (BoxRabbit.Right + RabbitSpeed < BoxRabbit.Parent.ClientSize.Width)
                    BoxRabbit.Left += RabbitSpeed;
            }
        }

        // Таймер для движения влево
        private void MoveLeft_Tick(object sender, EventArgs e)
        {
            if (!Jumping)
            {
                currentFrameIndex = (currentFrameIndex + 1) % leftFrames.Length;
                BoxRabbit.Image = leftFrames[currentFrameIndex];

                if (BoxRabbit.Left - RabbitSpeed >= 0)
                    BoxRabbit.Left -= RabbitSpeed;
            }
        }

        // Таймер для прыжка
        private void JumpTimer_Tick(object sender, EventArgs e)
        {
            if (Jumping)
            {
                if (BoxRabbit.Top > originalTop - JumpHeight)
                {
                    BoxRabbit.Top -= JumpSpeed;
                    BoxRabbit.Left += (Right ? JumpHorizontalSpeed : (Left ? -JumpHorizontalSpeed : 0));
                    currentFrameIndex = (currentFrameIndex + 1) % jumpImage.Length;
                    BoxRabbit.Image = jumpImage[currentFrameIndex];
                }
                else
                {
                    Jumping = false;
                }

                // Проверка на границы экрана по горизонтали
                if (BoxRabbit.Left < 0)
                {
                    BoxRabbit.Left = 0;
                }
                else if (BoxRabbit.Right > BoxRabbit.Parent.ClientSize.Width)
                {
                    BoxRabbit.Left = BoxRabbit.Parent.ClientSize.Width - BoxRabbit.Width;
                }
            }
            else
            {
                if (BoxRabbit.Top < originalTop)
                {
                    BoxRabbit.Top += Gravity;
                    BoxRabbit.Image = jumpImage[0];
                }
                else
                {
                    IsJumping = false;
                    ((Timer)sender).Stop();
                    BoxRabbit.Image = Resources.idleFrames;

                    // Проверка на границы экрана по горизонтали при остановке прыжка
                    if (BoxRabbit.Left < 0)
                    {
                        BoxRabbit.Left = 0;
                    }
                    else if (BoxRabbit.Right > BoxRabbit.Parent.ClientSize.Width)
                    {
                        BoxRabbit.Left = BoxRabbit.Parent.ClientSize.Width - BoxRabbit.Width;
                    }
                }
            }
        }

        // заяц
        public PictureBox GetPictureBox()
        {
            return BoxRabbit;
        }

        // Движения вправо
        public void StartMoveRight()
        {
            if (!Right)
            {
                Right = true;
                BoxRabbit.Image = Resources.RightRun;
                MoveRight.Start();
            }
        }

        // Движения влево
        public void StartMoveLeft()
        {
            if (!Left)
            {
                Left = true;
                BoxRabbit.Image = Resources.LeftRun;
                MoveLeft.Start();
            }
        }

        // Остановка движения вправо
        public void StopMoveRight()
        {
            BoxRabbit.Image = Resources.idleFrames;
            MoveRight.Stop();
            Right = false;
        }

        // Остановка движения влево
        public void StopMoveLeft()
        {
            BoxRabbit.Image = Resources.idleFrames;
            MoveLeft.Stop();
            Left = false;
        }

        // Запуск прыжка
        public void Jump()
        {
            if (!Jumping && !IsJumping)
            {
                IsJumping = true;
                Jumping = true;
                originalTop = BoxRabbit.Top;
                Timer jumpTimer = new Timer();
                jumpTimer.Interval = 10;
                jumpTimer.Tick += JumpTimer_Tick;
                jumpTimer.Start();
            }
        }
    }
}
