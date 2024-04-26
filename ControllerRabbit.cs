using ExpRabbit.Properties;
using System;
using System.Windows.Forms;

namespace ExpRabbit
{
    public class ControllerRabbit
    {
        private ModelRabbit Model;
        private ViewRabbit View;

        public ControllerRabbit(ModelRabbit model, ViewRabbit view)
        {
            this.Model = model;
            this.View = view;

            // Инициализация таймеров для движения
            Model.MoveRight = new Timer();
            Model.MoveRight.Interval = 10;
            Model.MoveRight.Tick += MoveRight_Tick;

            Model.MoveLeft = new Timer();
            Model.MoveLeft.Interval = 10;
            Model.MoveLeft.Tick += MoveLeft_Tick;
        }

        // Таймер для движения вправо
        private void MoveRight_Tick(object sender, EventArgs e)
        {
            if (!Model.Jumping)
            {
                View.currentFrameIndex = (View.currentFrameIndex + 1) % View.rightFrames.Length;
                View.BoxRabbit.Image = View.rightFrames[View.currentFrameIndex];

                if (View.BoxRabbit.Right + Model.RabbitSpeed < View.BoxRabbit.Parent.ClientSize.Width)
                    View.BoxRabbit.Left += Model.RabbitSpeed;
            }
        }

        // Таймер для движения влево
        private void MoveLeft_Tick(object sender, EventArgs e)
        {
            if (!Model.Jumping)
            {
                View.currentFrameIndex = (View.currentFrameIndex + 1) % View.leftFrames.Length;
                View.BoxRabbit.Image = View.leftFrames[View.currentFrameIndex];

                if (View.BoxRabbit.Left - Model.RabbitSpeed >= 0)
                    View.BoxRabbit.Left -= Model.RabbitSpeed;
            }
        }

        private void JumpTimer_Tick(object sender, EventArgs e)
        {
            if (Model.Jumping)
            {
                if (View.BoxRabbit.Top > Model.originalTop - Model.JumpHeight)
                {
                    View.BoxRabbit.Top -= Model.JumpSpeed;
                    View.BoxRabbit.Left += (Model.Right ? Model.JumpHorizontalSpeed : (Model.Left ? - Model.JumpHorizontalSpeed : 0));
                    View.currentFrameIndex = (View.currentFrameIndex + 1) % View.jumpImage.Length;
                    View.BoxRabbit.Image = View.jumpImage[View.currentFrameIndex];
                }
                else
                {
                    Model.Jumping = false;
                }

                // Проверка на границы экрана по горизонтали
                if (View.BoxRabbit.Left < 0)
                {
                    View.BoxRabbit.Left = 0;
                }
                else if (View.BoxRabbit.Right > View.BoxRabbit.Parent.ClientSize.Width)
                {
                    View.BoxRabbit.Left = View.BoxRabbit.Parent.ClientSize.Width - View.BoxRabbit.Width;
                }
            }
            else
            {
                if (View.BoxRabbit.Top < Model.originalTop)
                {
                    View.BoxRabbit.Top += Model.Gravity;
                    View.BoxRabbit.Image = View.jumpImage[0];
                }
                else
                {
                    Model.IsJumping = false;
                    ((Timer)sender).Stop();
                    View.BoxRabbit.Image = Resources.idleFrames;

                    // Проверка на границы экрана по горизонтали при остановке прыжка
                    if (View.BoxRabbit.Left < 0)
                    {
                        View.BoxRabbit.Left = 0;
                    }
                    else if (View.BoxRabbit.Right > View.BoxRabbit.Parent.ClientSize.Width)
                    {
                        View.BoxRabbit.Left = View.BoxRabbit.Parent.ClientSize.Width - View.BoxRabbit.Width;
                    }
                }
            }
        }

        public void StartMoveRight()
        {
            if (!Model.Right)
            {
                Model.Right = true;
                View.BoxRabbit.Image = Resources.RightRun;
                Model.MoveRight.Start();
            }
        }

        // Движения влево
        public void StartMoveLeft()
        {
            if (!Model.Left)
            {
                Model.Left = true;
                View.BoxRabbit.Image = Resources.LeftRun;
                Model.MoveLeft.Start();
            }
        }

        // Остановка движения вправо
        public void StopMoveRight()
        {
            View.BoxRabbit.Image = Resources.idleFrames;
            Model.MoveRight.Stop();
            Model.Right = false;
        }

        // Остановка движения влево
        public void StopMoveLeft()
        {
            View.BoxRabbit.Image = Resources.idleFrames;
            Model.MoveLeft.Stop();
            Model.Left = false;
        }

        // Запуск прыжка
        public void Jump()
        {
            if (!Model.Jumping && !Model.IsJumping)
            {
                Model.IsJumping = true;
                Model.Jumping = true;
                Model.originalTop = View.BoxRabbit.Top;
                Timer jumpTimer = new Timer();
                jumpTimer.Interval = 10;
                jumpTimer.Tick += JumpTimer_Tick;
                jumpTimer.Start();
            }
        }
    }
}
