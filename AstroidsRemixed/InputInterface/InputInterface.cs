using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;

namespace InputInterfaceLib
{
    public class InputInterface
    {
        Thread controllerThread;

        public volatile bool Up = false;
        public volatile bool Down = false;
        public volatile bool Left = false;
        public volatile bool Right = false;
        public volatile bool Shoot = false;
        public volatile bool Pause = false;

        GamePadState PreviousState;
        bool PreviousPauseState;

        private bool MuteKeyboard = false;

        public InputInterface()
        {
            controllerThread = new Thread(PullInputThreadMethod);
            controllerThread.Start();
        }

        public void PushInputToInterface(KeyEventArgs inputKey, bool isPressed)
        {
            if (!MuteKeyboard)
            {
                if (inputKey.KeyCode == System.Windows.Forms.Keys.Up)
                    Up = isPressed;

                if (inputKey.KeyCode == System.Windows.Forms.Keys.Down)
                    Down = isPressed;

                if (inputKey.KeyCode == System.Windows.Forms.Keys.Left)
                    Left = isPressed;

                if (inputKey.KeyCode == System.Windows.Forms.Keys.Right)
                    Right = isPressed;

                if (inputKey.KeyCode == System.Windows.Forms.Keys.Space)
                    Shoot = isPressed;

                if (inputKey.KeyCode == System.Windows.Forms.Keys.P && !isPressed)
                {
                    Pause = !Pause;
                }             
            }
        }

        public void PullInputThreadMethod()
        {
            while (true)
            {
                if (GamePad.GetState(PlayerIndex.One).IsConnected)
                {
                    Left = (GamePad.GetState(PlayerIndex.One)).IsButtonDown(Buttons.LeftThumbstickLeft);
                    Right = (GamePad.GetState(PlayerIndex.One)).IsButtonDown(Buttons.LeftThumbstickRight);
                    Up = (GamePad.GetState(PlayerIndex.One)).IsButtonDown(Buttons.RightTrigger);
                    Shoot = (GamePad.GetState(PlayerIndex.One)).IsButtonDown(Buttons.A);
                    Pause = (GamePad.GetState(PlayerIndex.One)).IsButtonDown(Buttons.Start);

                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) && !PreviousState.IsButtonDown(Buttons.Start))
                        Pause = !Pause;
                }

                MuteKeyboard = (GamePad.GetState(PlayerIndex.One).IsConnected);
                Thread.Sleep(60);
            }
        }
    }
}
