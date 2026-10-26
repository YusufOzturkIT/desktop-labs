namespace BmiMvvm.Models;

public record BmiResult(double Bmi, string Category, string ColorHex);

public static class BmiCalculator
{
    public static BmiResult? Calculate(double weight, string weightUnit, double height, string heightUnit)
    {
        if (weight <= 0 || height <= 0)
            return null;

        // Birim Dönüşümleri
        double weightKg = weightUnit == "lb" ? weight * 0.4536 : weight;
        double heightM = heightUnit == "in" ? (height * 2.54) / 100.0 : height / 100.0;

        if (heightM <= 0) return null;

        double bmi = weightKg / (heightM * heightM);

        string category;
        string color;

        if (bmi < 18.5)
        {
            category = "Underweight";
            color = "#FFA500"; // Orange
        }
        else if (bmi < 25.0)
        {
            category = "Normal weight";
            color = "#008000"; // Green
        }
        else if (bmi < 30.0)
        {
            category = "Overweight";
            color = "#FF4500"; // OrangeRed
        }
        else
        {
            category = "Obese";
            color = "#FF0000"; // Red
        }

        return new BmiResult(bmi, category, color);
    }
}