using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FaceRec.Models;

namespace FaceRec.Views
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void setDefaultValues()
        {
            var config = ProgramContext.Current.Config;
            this.chkDrawRectangle.Checked = config.DrawRectangle;
            this.numericTolerance.Value = (decimal)config.Tolerance;
            this.chkEnableGPUAcceleration.Checked = config.EnableGPUAcceleration;
            this.chkEnableMovementDetection.Checked = config.EnableMovementDetection;
            this.chkEnableCloudDetection.Checked = config.EnableCloudDetection;
            this.chkEnableRealTimeRecoginition.Checked = config.EnableRealTimeRecoginition;
            this.numericUpSampleTimes.Value = config.UpSampleTimes;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var config = new AppConfig();
            config.DrawRectangle = this.chkDrawRectangle.Checked;
            config.Tolerance = (float)this.numericTolerance.Value;
            config.EnableGPUAcceleration = this.chkEnableGPUAcceleration.Checked;
            config.EnableMovementDetection = this.chkEnableMovementDetection.Checked;
            config.EnableCloudDetection = this.chkEnableCloudDetection.Checked;
            config.EnableRealTimeRecoginition = this.chkEnableRealTimeRecoginition.Checked;
            config.UpSampleTimes = (uint)this.numericUpSampleTimes.Value;

            try
            {
                config.Save();

                ProgramContext.Current.Config.Copy(config);
                MessageBox.Show("保存成功！", "提示");
                this.Close();
            }catch(Exception error)
            {
                throw error;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.setDefaultValues();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            this.setDefaultValues();
        }
    }
}
