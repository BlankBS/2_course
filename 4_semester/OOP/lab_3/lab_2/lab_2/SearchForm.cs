using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_2
{
    public partial class SearchForm : Form
    {
        public string CityPattern => txtSearchCity.Text;
        public int MinArea => (int)numMinArea.Value;

        public SearchForm()
        {
            InitializeComponent();

            btnDoSearch.Text = "Найти";
            btnDoSearch.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.OK; 
                this.Close();
            };
        }
    }
}
