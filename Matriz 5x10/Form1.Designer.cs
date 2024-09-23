namespace MatrizRecursiva
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Tamaño de la ventana ajustada para mostrar todos los elementos
            this.ClientSize = new System.Drawing.Size(700, 350);
            this.Name = "Form1";
            this.Text = "Matriz Recursiva";

            this.ResumeLayout(false);
        }
    }
}
