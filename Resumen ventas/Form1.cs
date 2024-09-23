using System;
using System.Windows.Forms;

namespace VentasSemanal
{
    public partial class Form1 : Form
    {
        private TextBox[,] ventasTextBoxes = new TextBox[12, 7]; // TextBoxes para las ventas
        private Label menorVentaLabel;
        private Label mayorVentaLabel;
        private Label totalVentasLabel;
        private Label[] ventasPorDiaLabels = new Label[7]; // Para mostrar las ventas por cada día de la semana
        private Button calcularButton;

        public Form1()
        {
            InitializeComponent();
            CrearLabelsDiasSemana();  // Crear los labels para los días de la semana
            CrearLabelsMeses();       // Crear los labels para los meses
            CrearMatrizTextBoxes();   // Crear los TextBoxes para ingresar ventas
            CrearLabelsResultados();  // Crear los Labels para mostrar resultados
            CrearBotonCalcular();     // Crear el botón de calcular
        }

        // Método para crear los Labels de los días de la semana
        private void CrearLabelsDiasSemana()
        {
            string[] diasSemana = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" };
            for (int i = 0; i < diasSemana.Length; i++)
            {
                Label diaLabel = new Label();
                diaLabel.Text = diasSemana[i];
                diaLabel.Width = 48;
                diaLabel.Location = new System.Drawing.Point(10 + i * 45, 10);
                Controls.Add(diaLabel);
            }
        }

        // Método para crear los Labels de los meses
        private void CrearLabelsMeses()
        {
            string[] meses = { "E", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D" };
            for (int i = 0; i < meses.Length; i++)
            {
                Label mesLabel = new Label();
                mesLabel.Text = meses[i];
                mesLabel.Width = 20;
                mesLabel.Location = new System.Drawing.Point(0, 40 + i * 30);
                Controls.Add(mesLabel);
            }
        }

        // Método para crear los TextBoxes donde el usuario ingresará las ventas
        private void CrearMatrizTextBoxes()
        {
            CrearMatrizRecursiva(0, 0);
        }

        private void CrearMatrizRecursiva(int fila, int col)
        {
            if (fila == 12) return; // Condición de parada

            ventasTextBoxes[fila, col] = new TextBox();
            ventasTextBoxes[fila, col].Width = 40;
            ventasTextBoxes[fila, col].Location = new System.Drawing.Point(30 + col * 45, 40 + fila * 30);
            Controls.Add(ventasTextBoxes[fila, col]);

            // Recorrer las columnas
            if (col < 6)
                CrearMatrizRecursiva(fila, col + 1);
            else
                CrearMatrizRecursiva(fila + 1, 0); // Ir a la siguiente fila cuando termina la columna
        }

        // Crear labels para los resultados
        private void CrearLabelsResultados()
        {
            menorVentaLabel = new Label();
            menorVentaLabel.Location = new System.Drawing.Point(350, 40);
            menorVentaLabel.Width = 200;
            Controls.Add(menorVentaLabel);

            mayorVentaLabel = new Label();
            mayorVentaLabel.Location = new System.Drawing.Point(350, 70);
            mayorVentaLabel.Width = 200;
            Controls.Add(mayorVentaLabel);

            totalVentasLabel = new Label();
            totalVentasLabel.Location = new System.Drawing.Point(350, 100);
            totalVentasLabel.Width = 200;
            Controls.Add(totalVentasLabel);

            string[] diasSemana = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
            for (int i = 0; i < diasSemana.Length; i++)
            {
                ventasPorDiaLabels[i] = new Label();
                ventasPorDiaLabels[i].Location = new System.Drawing.Point(350, 130 + i * 30);
                ventasPorDiaLabels[i].Width = 200;
                ventasPorDiaLabels[i].Text = diasSemana[i] + ": ";
                Controls.Add(ventasPorDiaLabels[i]);
            }
        }

        // Crear el botón de calcular
        private void CrearBotonCalcular()
        {
            calcularButton = new Button();
            calcularButton.Text = "Calcular";
            calcularButton.Location = new System.Drawing.Point(350, 340);
            calcularButton.Click += new EventHandler(CalcularResultados);
            Controls.Add(calcularButton);
        }

        // Método para calcular los resultados cuando se presiona el botón
        private void CalcularResultados(object sender, EventArgs e)
        {
            int[,] ventas = new int[12, 7];
            ObtenerVentasRecursiva(ventas, 0, 0); // Obtener las ventas ingresadas

            (int menorVenta, int mesMenor, int diaMenor) = ObtenerMenorVenta(ventas, 0, 0, ventas[0, 0], 0, 0);
            menorVentaLabel.Text = $"Menor venta: {menorVenta} en mes {mesMenor + 1}, día {ObtenerDiaSemana(diaMenor)}";

            (int mayorVenta, int mesMayor, int diaMayor) = ObtenerMayorVenta(ventas, 0, 0, ventas[0, 0], 0, 0);
            mayorVentaLabel.Text = $"Mayor venta: {mayorVenta} en mes {mesMayor + 1}, día {ObtenerDiaSemana(diaMayor)}";

            int total = ObtenerVentaTotal(ventas, 0, 0);
            totalVentasLabel.Text = $"Venta total: {total}";

            int[] ventasPorDia = new int[7];
            ObtenerVentasPorDia(ventas, ventasPorDia, 0, 0);
            MostrarVentasPorDia(ventasPorDia, 0);
        }

        // Método recursivo para obtener las ventas ingresadas por el usuario
        private void ObtenerVentasRecursiva(int[,] ventas, int fila, int col)
        {
            if (fila == 12) return;

            ventas[fila, col] = int.Parse(ventasTextBoxes[fila, col].Text);

            if (col < 6)
                ObtenerVentasRecursiva(ventas, fila, col + 1);
            else
                ObtenerVentasRecursiva(ventas, fila + 1, 0);
        }

        // Recursivo para obtener la menor venta
        private (int, int, int) ObtenerMenorVenta(int[,] ventas, int fila, int col, int menorVenta, int mesMenor, int diaMenor)
        {
            if (fila == 12) return (menorVenta, mesMenor, diaMenor);

            if (ventas[fila, col] < menorVenta)
            {
                menorVenta = ventas[fila, col];
                mesMenor = fila;
                diaMenor = col;
            }

            if (col < 6)
                return ObtenerMenorVenta(ventas, fila, col + 1, menorVenta, mesMenor, diaMenor);
            else
                return ObtenerMenorVenta(ventas, fila + 1, 0, menorVenta, mesMenor, diaMenor);
        }

        // Recursivo para obtener la mayor venta
        private (int, int, int) ObtenerMayorVenta(int[,] ventas, int fila, int col, int mayorVenta, int mesMayor, int diaMayor)
        {
            if (fila == 12) return (mayorVenta, mesMayor, diaMayor);

            if (ventas[fila, col] > mayorVenta)
            {
                mayorVenta = ventas[fila, col];
                mesMayor = fila;
                diaMayor = col;
            }

            if (col < 6)
                return ObtenerMayorVenta(ventas, fila, col + 1, mayorVenta, mesMayor, diaMayor);
            else
                return ObtenerMayorVenta(ventas, fila + 1, 0, mayorVenta, mesMayor, diaMayor);
        }

        // Recursivo para obtener la venta total
        private int ObtenerVentaTotal(int[,] ventas, int fila, int col)
        {
            if (fila == 12) return 0;

            int suma = ventas[fila, col];

            if (col < 6)
                return suma + ObtenerVentaTotal(ventas, fila, col + 1);
            else
                return suma + ObtenerVentaTotal(ventas, fila + 1, 0);
        }

        // Recursivo para obtener las ventas por día de la semana
        private void ObtenerVentasPorDia(int[,] ventas, int[] ventasPorDia, int fila, int col)
        {
            if (fila == 12) return;

            ventasPorDia[col] += ventas[fila, col];

            if (col < 6)
                ObtenerVentasPorDia(ventas, ventasPorDia, fila, col + 1);
            else
                ObtenerVentasPorDia(ventas, ventasPorDia, fila + 1, 0);
        }

        // Método para mostrar las ventas por día
        private void MostrarVentasPorDia(int[] ventasPorDia, int dia)
        {
            if (dia == 7) return;

            ventasPorDiaLabels[dia].Text = $"{ventasPorDiaLabels[dia].Text} {ventasPorDia[dia]}";
            MostrarVentasPorDia(ventasPorDia, dia + 1);
        }

        // Obtener el nombre del día de la semana
        private string ObtenerDiaSemana(int dia)
        {
            string[] diasSemana = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
            return diasSemana[dia];
        }
    }
}
