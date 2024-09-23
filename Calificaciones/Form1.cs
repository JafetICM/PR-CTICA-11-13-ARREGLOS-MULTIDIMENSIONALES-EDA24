using System;
using System.Windows.Forms;

namespace Calificaciones
{
    public partial class Form1 : Form
    {
        private TextBox[,] calificacionesTextBoxes = new TextBox[10, 4]; // TextBoxes para las calificaciones (10 alumnos, 4 parciales)
        private Label promedioLabel;
        private Label mayorPromedioLabel;
        private Label menorPromedioLabel;
        private Label reprobadosLabel;
        private Label distribucionLabel;
        private Button calcularButton;

        public Form1()
        {
            InitializeComponent();
            CrearMatrizTextBoxes(); // Crear los TextBoxes para las calificaciones
            CrearLabelsResultados(); // Crear los Labels para mostrar los resultados
            CrearBotonCalcular(); // Crear el botón de calcular
        }

        // Método para crear los TextBoxes donde el usuario ingresará las calificaciones
        private void CrearMatrizTextBoxes()
        {
            CrearMatrizRecursiva(0, 0);
        }

        private void CrearMatrizRecursiva(int fila, int col)
        {
            if (fila == 10) return; // Condición de parada

            calificacionesTextBoxes[fila, col] = new TextBox();
            calificacionesTextBoxes[fila, col].Width = 40;
            calificacionesTextBoxes[fila, col].Location = new System.Drawing.Point(10 + col * 45, 40 + fila * 30);
            Controls.Add(calificacionesTextBoxes[fila, col]);

            // Recorrer las columnas
            if (col < 3)
                CrearMatrizRecursiva(fila, col + 1);
            else
                CrearMatrizRecursiva(fila + 1, 0); // Ir a la siguiente fila cuando termina la columna
        }

        // Crear labels para los resultados
        private void CrearLabelsResultados()
        {
            promedioLabel = new Label();
            promedioLabel.Location = new System.Drawing.Point(350, 40);
            promedioLabel.Width = 200;
            Controls.Add(promedioLabel);

            mayorPromedioLabel = new Label();
            mayorPromedioLabel.Location = new System.Drawing.Point(350, 70);
            mayorPromedioLabel.Width = 200;
            Controls.Add(mayorPromedioLabel);

            menorPromedioLabel = new Label();
            menorPromedioLabel.Location = new System.Drawing.Point(350, 100);
            menorPromedioLabel.Width = 200;
            Controls.Add(menorPromedioLabel);

            reprobadosLabel = new Label();
            reprobadosLabel.Location = new System.Drawing.Point(350, 130);
            reprobadosLabel.Width = 200;
            Controls.Add(reprobadosLabel);

            distribucionLabel = new Label();
            distribucionLabel.Location = new System.Drawing.Point(350, 160);
            distribucionLabel.Width = 200;
            distribucionLabel.Height = 100;
            Controls.Add(distribucionLabel);
        }

        // Crear el botón de calcular
        private void CrearBotonCalcular()
        {
            calcularButton = new Button();
            calcularButton.Text = "Calcular";
            calcularButton.Location = new System.Drawing.Point(350, 270);
            calcularButton.Click += new EventHandler(CalcularResultados);
            Controls.Add(calcularButton);
        }

        // Método para calcular los resultados cuando se presiona el botón
        private void CalcularResultados(object sender, EventArgs e)
        {
            double[,] calificaciones = new double[10, 4];
            ObtenerCalificacionesRecursiva(calificaciones, 0, 0); // Obtener las calificaciones ingresadas

            double[] promedios = new double[10];
            ObtenerPromedios(calificaciones, promedios, 0); // Calcular los promedios

            double mayorPromedio = ObtenerMayorPromedio(promedios, 0, promedios[0]);
            double menorPromedio = ObtenerMenorPromedio(promedios, 0, promedios[0]);
            int reprobados = ObtenerReprobados(calificaciones, 0, 0);
            int[] distribucion = new int[5];
            ObtenerDistribucionPromedios(promedios, distribucion, 0);

            promedioLabel.Text = $"Promedio de cada alumno: {string.Join(", ", promedios)}";
            mayorPromedioLabel.Text = $"Promedio más alto: {mayorPromedio}";
            menorPromedioLabel.Text = $"Promedio más bajo: {menorPromedio}";
            reprobadosLabel.Text = $"Total de reprobados: {reprobados}";
            distribucionLabel.Text = FormatearDistribucion(distribucion);
        }

        // Método recursivo para obtener las calificaciones ingresadas por el usuario
        private void ObtenerCalificacionesRecursiva(double[,] calificaciones, int fila, int col)
        {
            if (fila == 10) return;

            calificaciones[fila, col] = double.Parse(calificacionesTextBoxes[fila, col].Text);

            if (col < 3)
                ObtenerCalificacionesRecursiva(calificaciones, fila, col + 1);
            else
                ObtenerCalificacionesRecursiva(calificaciones, fila + 1, 0);
        }

        // Método recursivo para obtener los promedios de cada alumno
        private void ObtenerPromedios(double[,] calificaciones, double[] promedios, int fila)
        {
            if (fila == 10) return;

            promedios[fila] = (calificaciones[fila, 0] + calificaciones[fila, 1] + calificaciones[fila, 2] + calificaciones[fila, 3]) / 4.0;
            ObtenerPromedios(calificaciones, promedios, fila + 1);
        }

        // Método recursivo para obtener el promedio más alto
        private double ObtenerMayorPromedio(double[] promedios, int indice, double mayor)
        {
            if (indice == 10) return mayor;

            if (promedios[indice] > mayor)
                mayor = promedios[indice];

            return ObtenerMayorPromedio(promedios, indice + 1, mayor);
        }

        // Método recursivo para obtener el promedio más bajo
        private double ObtenerMenorPromedio(double[] promedios, int indice, double menor)
        {
            if (indice == 10) return menor;

            if (promedios[indice] < menor)
                menor = promedios[indice];

            return ObtenerMenorPromedio(promedios, indice + 1, menor);
        }

        // Método recursivo para contar cuántos parciales fueron reprobados (menores a 7.0)
        private int ObtenerReprobados(double[,] calificaciones, int fila, int col)
        {
            if (fila == 10) return 0;

            int cuenta = calificaciones[fila, col] < 7.0 ? 1 : 0;

            if (col < 3)
                return cuenta + ObtenerReprobados(calificaciones, fila, col + 1);
            else
                return cuenta + ObtenerReprobados(calificaciones, fila + 1, 0);
        }

        // Método recursivo para obtener la distribución de los promedios
        private void ObtenerDistribucionPromedios(double[] promedios, int[] distribucion, int indice)
        {
            if (indice == 10) return;

            if (promedios[indice] <= 5.9) distribucion[0]++;
            else if (promedios[indice] <= 6.9) distribucion[1]++;
            else if (promedios[indice] <= 7.9) distribucion[2]++;
            else if (promedios[indice] <= 8.9) distribucion[3]++;
            else distribucion[4]++;

            ObtenerDistribucionPromedios(promedios, distribucion, indice + 1);
        }

        // Método para formatear la distribución de promedios en texto
        private string FormatearDistribucion(int[] distribucion)
        {
            return $"0.0 - 5.9: {distribucion[0]} alumnos\n" +
                   $"6.0 - 6.9: {distribucion[1]} alumnos\n" +
                   $"7.0 - 7.9: {distribucion[2]} alumnos\n" +
                   $"8.0 - 8.9: {distribucion[3]} alumnos\n" +
                   $"9.0 - 10.0: {distribucion[4]} alumnos\n";
        }
    }
}
