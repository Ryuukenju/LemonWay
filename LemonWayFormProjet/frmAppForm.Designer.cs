namespace LemonWayFormProjet
{
    partial class frmAppForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.but_AsynCompute_Fibonancci = new System.Windows.Forms.Button();
            this.but_SynCompute_Fibonancci = new System.Windows.Forms.Button();
            this.textCompute_Fibonancci = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // but_AsynCompute_Fibonancci
            // 
            this.but_AsynCompute_Fibonancci.Location = new System.Drawing.Point(359, 89);
            this.but_AsynCompute_Fibonancci.Name = "but_AsynCompute_Fibonancci";
            this.but_AsynCompute_Fibonancci.Size = new System.Drawing.Size(163, 23);
            this.but_AsynCompute_Fibonancci.TabIndex = 0;
            this.but_AsynCompute_Fibonancci.Text = "Compute Fibonancci Async";
            this.but_AsynCompute_Fibonancci.UseVisualStyleBackColor = true;
            this.but_AsynCompute_Fibonancci.Click += new System.EventHandler(this.but_AsynCompute_Fibonancci_Click);
            // 
            // but_SynCompute_Fibonancci
            // 
            this.but_SynCompute_Fibonancci.Location = new System.Drawing.Point(24, 89);
            this.but_SynCompute_Fibonancci.Name = "but_SynCompute_Fibonancci";
            this.but_SynCompute_Fibonancci.Size = new System.Drawing.Size(157, 23);
            this.but_SynCompute_Fibonancci.TabIndex = 1;
            this.but_SynCompute_Fibonancci.Text = "Compute Fibonancci Sync";
            this.but_SynCompute_Fibonancci.UseVisualStyleBackColor = true;
            this.but_SynCompute_Fibonancci.Click += new System.EventHandler(this.but_SynCompute_Fibonancci_Click);
            // 
            // textCompute_Fibonancci
            // 
            this.textCompute_Fibonancci.Location = new System.Drawing.Point(24, 137);
            this.textCompute_Fibonancci.Multiline = true;
            this.textCompute_Fibonancci.Name = "textCompute_Fibonancci";
            this.textCompute_Fibonancci.Size = new System.Drawing.Size(498, 200);
            this.textCompute_Fibonancci.TabIndex = 2;
            // 
            // frmAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 400);
            this.Controls.Add(this.textCompute_Fibonancci);
            this.Controls.Add(this.but_SynCompute_Fibonancci);
            this.Controls.Add(this.but_AsynCompute_Fibonancci);
            this.Name = "frmAppForm";
            this.Text = "LemonWay";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button but_AsynCompute_Fibonancci;
        private System.Windows.Forms.Button but_SynCompute_Fibonancci;
        private System.Windows.Forms.TextBox textCompute_Fibonancci;
    }
}

