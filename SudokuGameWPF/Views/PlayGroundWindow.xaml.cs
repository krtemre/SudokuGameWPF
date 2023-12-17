using SudokuGameWPF.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuGameWPF.Views
{
    public enum GridLoc
    {
        loc_0x0, loc_0x1, loc_0x2, loc_0x3, loc_0x4, loc_0x5, loc_0x6, loc_0x7, loc_0x8,
        loc_1x0, loc_1x1, loc_1x2, loc_1x3, loc_1x4, loc_1x5, loc_1x6, loc_1x7, loc_1x8,
        loc_2x0, loc_2x1, loc_2x2, loc_2x3, loc_2x4, loc_2x5, loc_2x6, loc_2x7, loc_2x8,
        loc_3x0, loc_3x1, loc_3x2, loc_3x3, loc_3x4, loc_3x5, loc_3x6, loc_3x7, loc_3x8,
        loc_4x0, loc_4x1, loc_4x2, loc_4x3, loc_4x4, loc_4x5, loc_4x6, loc_4x7, loc_4x8,
        loc_5x0, loc_5x1, loc_5x2, loc_5x3, loc_5x4, loc_5x5, loc_5x6, loc_5x7, loc_5x8,
        loc_6x0, loc_6x1, loc_6x2, loc_6x3, loc_6x4, loc_6x5, loc_6x6, loc_6x7, loc_6x8,
        loc_7x0, loc_7x1, loc_7x2, loc_7x3, loc_7x4, loc_7x5, loc_7x6, loc_7x7, loc_7x8,
        loc_8x0, loc_8x1, loc_8x2, loc_8x3, loc_8x4, loc_8x5, loc_8x6, loc_8x7, loc_8x8,
    }

    public partial class PlayGroundWindow : Window
    {
        private Button lastButton = null;
        private List<Button> buttons = null;
        private int index = -1;

        private Button lastSelected = null;

        public PlayGroundWindow()
        {
            InitializeComponent();
            InitButtons(PlayerManager.Instance.PlayerData.Game);
        }
        private void BtnPlayground_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (lastButton != null)
                {
                    lastButton.IsDefault = false;
                }

                btn.IsDefault = true;
                lastButton = btn;
            }
        }

        private void InitButtons(List<int[]> game)
        {
            #region Adding buttons
            buttons = new List<Button>();
            buttons.Add(Btn0_0);
            buttons.Add(Btn0_1);
            buttons.Add(Btn0_2);
            buttons.Add(Btn0_3);
            buttons.Add(Btn0_4);
            buttons.Add(Btn0_5);
            buttons.Add(Btn0_6);
            buttons.Add(Btn0_7);
            buttons.Add(Btn0_8);

            buttons.Add(Btn1_0);
            buttons.Add(Btn1_1);
            buttons.Add(Btn1_2);
            buttons.Add(Btn1_3);
            buttons.Add(Btn1_4);
            buttons.Add(Btn1_5);
            buttons.Add(Btn1_6);
            buttons.Add(Btn1_7);
            buttons.Add(Btn1_8);

            buttons.Add(Btn2_0);
            buttons.Add(Btn2_1);
            buttons.Add(Btn2_2);
            buttons.Add(Btn2_3);
            buttons.Add(Btn2_4);
            buttons.Add(Btn2_5);
            buttons.Add(Btn2_6);
            buttons.Add(Btn2_7);
            buttons.Add(Btn2_8);

            buttons.Add(Btn3_0);
            buttons.Add(Btn3_1);
            buttons.Add(Btn3_2);
            buttons.Add(Btn3_3);
            buttons.Add(Btn3_4);
            buttons.Add(Btn3_5);
            buttons.Add(Btn3_6);
            buttons.Add(Btn3_7);
            buttons.Add(Btn3_8);

            buttons.Add(Btn4_0);
            buttons.Add(Btn4_1);
            buttons.Add(Btn4_2);
            buttons.Add(Btn4_3);
            buttons.Add(Btn4_4);
            buttons.Add(Btn4_5);
            buttons.Add(Btn4_6);
            buttons.Add(Btn4_7);
            buttons.Add(Btn4_8);

            buttons.Add(Btn5_0);
            buttons.Add(Btn5_1);
            buttons.Add(Btn5_2);
            buttons.Add(Btn5_3);
            buttons.Add(Btn5_4);
            buttons.Add(Btn5_5);
            buttons.Add(Btn5_6);
            buttons.Add(Btn5_7);
            buttons.Add(Btn5_8);

            buttons.Add(Btn6_0);
            buttons.Add(Btn6_1);
            buttons.Add(Btn6_2);
            buttons.Add(Btn6_3);
            buttons.Add(Btn6_4);
            buttons.Add(Btn6_5);
            buttons.Add(Btn6_6);
            buttons.Add(Btn6_7);
            buttons.Add(Btn6_8);

            buttons.Add(Btn7_0);
            buttons.Add(Btn7_1);
            buttons.Add(Btn7_2);
            buttons.Add(Btn7_3);
            buttons.Add(Btn7_4);
            buttons.Add(Btn7_5);
            buttons.Add(Btn7_6);
            buttons.Add(Btn7_7);
            buttons.Add(Btn7_8);

            buttons.Add(Btn8_0);
            buttons.Add(Btn8_1);
            buttons.Add(Btn8_2);
            buttons.Add(Btn8_3);
            buttons.Add(Btn8_4);
            buttons.Add(Btn8_5);
            buttons.Add(Btn8_6);
            buttons.Add(Btn8_7);
            buttons.Add(Btn8_8);
            #endregion

            for (int i = 0; i < 9; i++)
            {
                int[] mat = game[i];

                for (int j = 0; j < 9; j++)
                {
                    var button = buttons[i * 9 + j];

                    if (button != null)
                    {
                        if (mat[j] == 0)
                        {
                            button.Content = "";
                        }
                        else
                        {
                            button.Content = mat[j];
                            button.Foreground = Brushes.Green;
                        }
                    }
                }
            }
        }

        private void BtnSelection_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (lastSelected != null)
                {
                    lastSelected.IsDefault = false;
                }

                btn.IsDefault = true;
                lastSelected = btn;
            }

            if(lastButton != null)
            {
                string tag = lastSelected.Content.ToString();

                if (!string.IsNullOrEmpty(tag))
                {
                    lastButton.Content = tag;
                }
            }
        }
    }
}
