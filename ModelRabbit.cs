using System;
using System.Drawing;
using System.Windows.Forms;
using ExpRabbit.Properties;

namespace ExpRabbit
{
    public class ModelRabbit
    {
        public Timer MoveRight;
        public Timer MoveLeft;

        // Переменные для управления движением и прыжком
        public bool Right = false;
        public bool Left = false;
        public bool Jumping = false;
        public bool IsJumping = false;
        public int originalTop = 690; // Исходное положение зайца до прыжка

        // Параметры движения и прыжка
        public int RabbitSpeed = 10;
        public int JumpSpeed = 40;
        public int Gravity = 20;
        public int JumpHeight = 200;
        public int JumpHorizontalSpeed = 20;
    }
}
