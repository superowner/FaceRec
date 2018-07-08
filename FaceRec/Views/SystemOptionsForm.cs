using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FaceRec.Models;

namespace FaceRec.Views
{
    public partial class SystemOptionsForm : Form
    {
        public SystemOptionsForm()
        {
            InitializeComponent();

            this.sampleSizeComboBox.DataSource = SampleSize.AllSizes;
            this.sampleSizeComboBox.DisplayMember = "Name";
            this.sampleSizeComboBox.ValueMember = "Value";

            this.setDefaultValues();
        }

        private void setDefaultValues()
        {
            var config = Program.Current.Config;
            this.sampleSizeComboBox.SelectedValue = config.ImageSampleSize.Value;
            this.numericTolerance.Value = (decimal)config.Tolerance;
            this.chkEnableGPUAccelerating.Checked = config.EnableGPUAccelerating;
            this.chkEnableMovementDetecting.Checked = config.EnableMovementDetecting;
            this.chkEnableCloudDetecting.Checked = config.EnableCloudDetecting;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.setDefaultValues();
        }
    }
}
