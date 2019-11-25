using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LemonWayFormProjet
{
    public partial class BusyForm : Form
    {
        public Action Worker { get; set; }

        public BusyForm(Action worker)
        {
            InitializeComponent();
            if (worker != null)
                Worker = worker;
            // Worker = worker ?? throw new ArgumentNullException(); 
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Worker != null)
                Task.Factory.StartNew(Worker).ContinueWith(t => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
