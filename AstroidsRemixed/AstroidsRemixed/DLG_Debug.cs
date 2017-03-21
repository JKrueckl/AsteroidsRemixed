using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AstroidsRemixed.GameModels;

namespace AstroidsRemixed
{
    public partial class DLG_Debug : Form
    {
        public string currentSelection { get; private set; }

        List<object> itemsInGame = new List<object>();
        int oldCount = 0;

        public DLG_Debug()
        {
            InitializeComponent();
        }

        private void Debug_CallItems_Tick(object sender, EventArgs e)
        {           
            if(ActiveForm is DLG_Main)
                itemsInGame = ((DLG_Main)ActiveForm).SendGameObjects();

            if (itemsInGame.Count != oldCount)
            {
                Debug_SelectBox.Items.Clear();

                foreach (var gameObject in itemsInGame)
                {
                    if (gameObject is PlayerInfo)
                    {
                        Debug_SelectBox.Items.Add("Player");
                    }
                    else if (gameObject is EnemyOne)
                    {
                        Debug_SelectBox.Items.Add("EnemyOne");
                    }
                    else if (gameObject is EnemyTwo)
                    {
                        Debug_SelectBox.Items.Add("EnemyTwo");
                    }
                }
                oldCount = itemsInGame.Count;
            }
        }

        private void Debug_SelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSelection = Debug_SelectBox.Items[Debug_SelectBox.SelectedIndex].ToString();      
        }

        public void TranslateInformation(object gameObject)
        {
            Debug_ObjectInfo.Items.Clear();

            if (gameObject is PlayerInfo)
            {
                Debug_ItemDisplayedLabel.Text = "Player Info";
                Debug_ObjectInfo.Items.Add(string.Format("Pos (x, y): {0:F2}, {1:F2}", ((PlayerInfo)gameObject)._pos.X, ((PlayerInfo)gameObject)._pos.Y));
                Debug_ObjectInfo.Items.Add(string.Format("Angle: {0}", ((PlayerInfo)gameObject)._currentRot));
                Debug_ObjectInfo.Items.Add(string.Format("Shooting Mode: {0}", ((PlayerInfo)gameObject)._currentFireMode));
            }
            else if (gameObject is EnemyOne)
            {
                Debug_ItemDisplayedLabel.Text = "EnemyOne Info";
                Debug_ObjectInfo.Items.Add(string.Format("Pos (x, y): {0:F2}, {1:F2}", ((EnemyOne)gameObject)._pos.X, ((EnemyOne)gameObject)._pos.Y));
                Debug_ObjectInfo.Items.Add(string.Format("Angle: {0}", ((EnemyOne)gameObject)._currentRot));
                Debug_ObjectInfo.Items.Add(string.Format("Shooting Mode: {0}", ((EnemyOne)gameObject)._currentFireMode));
            }
            else if (gameObject is EnemyTwo)
            {
                Debug_ItemDisplayedLabel.Text = "EnemyTwo Info";
                Debug_ObjectInfo.Items.Add(string.Format("Pos (x, y): {0:F2}, {1:F2}", ((EnemyTwo)gameObject)._pos.X, ((EnemyTwo)gameObject)._pos.Y));
                Debug_ObjectInfo.Items.Add(string.Format("Angle: {0}", ((EnemyTwo)gameObject)._currentRot));
                Debug_ObjectInfo.Items.Add(string.Format("Shooting Mode: {0}", ((EnemyTwo)gameObject)._currentFireMode));
            }
        }
    }
}
