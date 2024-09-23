using System;
using System.Windows.Forms;

namespace MatrizRecursiva
{
    public partial class Form1 : Form
    {
        private TextBox[,] matrizTextBoxes = new TextBox[5, 10]; // Matriz de 5x10
        private Label[] sumaFilasLabels = new Label[5]; // Sumas A
        private Label[] promedioFilasLabels = new Label[5]; // Promedios B
        private Label[] sumaColumnasLabels = new Label[10]; // Sumas C
        private Label[] promedioColumnasLabels = new Label[10]; // Promedios D
        private Button calcularButton;

        public Form1()
        {
            InitializeComponent();
            CrearMatrizTextBoxes(); // Crear los TextBoxes para la matriz
            CrearResultadosLabels(); // Crear los Labels para sumas y promedios
            CrearBotonCalcular(); // Crear botón calcular
        }

        // Método para crear la matriz de TextBoxes
        private void CrearMatrizTextBoxes()
        {
            CrearMatrizRecursiva(0, 0); // Llamada inicial recursiva
        }

        private void CrearMatrizRecursiva(int fila, int col)
        {
            if (fila == 5) return; // Condición de parada

            matrizTextBoxes[fila, col] = new TextBox();
            matrizTextBoxes[fila, col].Width = 30;
            matrizTextBoxes[fila, col].Location = new System.Drawing.Point(10 + col * 35, 10 + fila * 35);
            Controls.Add(matrizTextBoxes[fila, col]);

            // Mover a la siguiente columna o fila
            if (col < 9)
                CrearMatrizRecursiva(fila, col + 1);
            else
                CrearMatrizRecursiva(fila + 1, 0);
        }

        // Método para crear Labels para resultados
        private void CrearResultadosLabels()
        {
            CrearLabelsRecursivo(0, 0); // Llamada inicial recursiva
        }

        private void CrearLabelsRecursivo(int fila, int col)
        {
            if (fila < 5) // Para filas
            {
                // Labels para las sumas (A) y promedios (B) de las filas
                sumaFilasLabels[fila] = new Label();
                sumaFilasLabels[fila].Width = 50;
                sumaFilasLabels[fila].Location = new System.Drawing.Point(370, 10 + fila * 35); // Ajustado al lado derecho de la matriz
                Controls.Add(sumaFilasLabels[fila]);

                promedioFilasLabels[fila] = new Label();
                promedioFilasLabels[fila].Width = 50;
                promedioFilasLabels[fila].Location = new System.Drawing.Point(430, 10 + fila * 35); // Columna B
                Controls.Add(promedioFilasLabels[fila]);

                CrearLabelsRecursivo(fila + 1, col); // Recursión para la siguiente fila
            }
            else if (col < 10) // Para columnas
            {
                // Labels para las sumas (C) y promedios (D) de las columnas
                sumaColumnasLabels[col] = new Label();
                sumaColumnasLabels[col].Width = 50;
                sumaColumnasLabels[col].Location = new System.Drawing.Point(10 + col * 35, 200); // Ajustado debajo de la matriz
                Controls.Add(sumaColumnasLabels[col]);

                promedioColumnasLabels[col] = new Label();
                promedioColumnasLabels[col].Width = 50;
                promedioColumnasLabels[col].Location = new System.Drawing.Point(10 + col * 35, 240); // Debajo de C
                Controls.Add(promedioColumnasLabels[col]);

                CrearLabelsRecursivo(fila, col + 1); // Recursión para la siguiente columna
            }
        }

        // Método para crear el botón de calcular
        private void CrearBotonCalcular()
        {
            calcularButton = new Button();
            calcularButton.Text = "Calcular";
            calcularButton.Location = new System.Drawing.Point(500, 10); // Colocamos el botón a la derecha
            calcularButton.Click += new EventHandler(CalcularResultados);
            Controls.Add(calcularButton);
        }

        // Método para calcular sumas y promedios recursivamente
        private void CalcularResultados(object sender, EventArgs e)
        {
            int[,] matriz = new int[5, 10];
            ObtenerMatrizRecursiva(matriz, 0, 0); // Obtener la matriz ingresada
            int[] sumaFilas = new int[5];
            double[] promedioFilas = new double[5]; // Cambiado a double
            int[] sumaColumnas = new int[10];
            double[] promedioColumnas = new double[10]; // Cambiado a double

            CalcularSumasFilas(matriz, sumaFilas, promedioFilas, 0, 0); // Calcular sumas y promedios de filas
            CalcularSumasColumnas(matriz, sumaColumnas, promedioColumnas, 0, 0); // Calcular sumas y promedios de columnas

            MostrarResultadosRecursivo(sumaFilas, promedioFilas, sumaColumnas, promedioColumnas, 0, 0); // Mostrar los resultados
        }

        // Método recursivo para obtener la matriz desde los TextBoxes
        private void ObtenerMatrizRecursiva(int[,] matriz, int fila, int col)
        {
            if (fila == 5) return; // Condición de parada

            matriz[fila, col] = int.Parse(matrizTextBoxes[fila, col].Text);

            if (col < 9)
                ObtenerMatrizRecursiva(matriz, fila, col + 1);
            else
                ObtenerMatrizRecursiva(matriz, fila + 1, 0);
        }

        // Método recursivo para calcular sumas y promedios de filas
        private void CalcularSumasFilas(int[,] matriz, int[] sumaFilas, double[] promedioFilas, int fila, int col)
        {
            if (fila == 5) return; // Condición de parada

            sumaFilas[fila] += matriz[fila, col];

            if (col < 9)
                CalcularSumasFilas(matriz, sumaFilas, promedioFilas, fila, col + 1);
            else
            {
                promedioFilas[fila] = (double)sumaFilas[fila] / 10.0; // Cálculo decimal
                CalcularSumasFilas(matriz, sumaFilas, promedioFilas, fila + 1, 0);
            }
        }

        // Método recursivo para calcular sumas y promedios de columnas
        private void CalcularSumasColumnas(int[,] matriz, int[] sumaColumnas, double[] promedioColumnas, int fila, int col)
        {
            if (col == 10) return; // Condición de parada

            sumaColumnas[col] += matriz[fila, col];

            if (fila < 4)
                CalcularSumasColumnas(matriz, sumaColumnas, promedioColumnas, fila + 1, col);
            else
            {
                promedioColumnas[col] = (double)sumaColumnas[col] / 5.0; // Cálculo decimal
                CalcularSumasColumnas(matriz, sumaColumnas, promedioColumnas, 0, col + 1);
            }
        }

        // Método recursivo para mostrar los resultados en los Labels
        private void MostrarResultadosRecursivo(int[] sumaFilas, double[] promedioFilas, int[] sumaColumnas, double[] promedioColumnas, int fila, int col)
        {
            if (fila < 5) // Mostrar sumas y promedios de filas
            {
                sumaFilasLabels[fila].Text = sumaFilas[fila].ToString();
                promedioFilasLabels[fila].Text = promedioFilas[fila].ToString("F1"); // Formato con un decimal
                MostrarResultadosRecursivo(sumaFilas, promedioFilas, sumaColumnas, promedioColumnas, fila + 1, col);
            }
            else if (col < 10) // Mostrar sumas y promedios de columnas
            {
                sumaColumnasLabels[col].Text = sumaColumnas[col].ToString();
                promedioColumnasLabels[col].Text = promedioColumnas[col].ToString("F1"); // Formato con un decimal
                MostrarResultadosRecursivo(sumaFilas, promedioFilas, sumaColumnas, promedioColumnas, fila, col + 1);
            }
        }
    }
}
