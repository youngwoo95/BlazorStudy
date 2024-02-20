using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace YLManager.UI
{
    public class ColorConverter
    {
        /// <summary>
        /// RGB 값을 Color 객체로 변환
        /// </summary>
        /// <param name="red">int 타입 RED 숫자</param>
        /// <param name="green">int 타입 GREEN 숫자</param>
        /// <param name="blue">int 타입 BLUE 숫자</param>
        /// <returns>COLOR 객체 반환</returns>
        public static Color RgbToColor(int red, int green, int blue)
        {
            return Color.FromArgb((byte)red, (byte)green, (byte)blue);
        }

        /// <summary>
        /// Color 객체를 RGB 값으로 변환
        /// </summary>
        /// <param name="color">변환시킬 COLOR 객체</param>
        /// <returns>RED/GREEN/BLUE 의 int값</returns>
        public (int red, int green, int blue) ColorToRgb(Color color)
        {
            return (color.R, color.G, color.B);
        }

    }
}
