using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_1
{
    public interface IMaterialCalculator
    {
        double CalculateArea(double width, double length);
        double ConvertToSquareFeet(double squareMeters);
        int CalculateQuantity(double totalAream, double unitArea, double wastePercent);
    }
    public class Calculator : IMaterialCalculator
    {
        public double CalculateArea(double width, double length)
        {
            try
            {
                if(width <= 0 || length <= 0)
                {
                    throw new Exception("Длина и ширина должна быть положительным числом");
                }

                return width * length;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода: {ex.Message}", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        public double ConvertToSquareFeet(double squareMeters) => squareMeters * 10.7369;

        public int CalculateQuantity(double totalArea, double unitArea, double wastePercent)
        {
            if(unitArea <= 0 || totalArea <= 0)
            {
                return 0;
            }

            double areaWithWaste = totalArea * (1 + wastePercent / 100);
            return (int)Math.Ceiling(areaWithWaste / unitArea);
        }
    }
}
