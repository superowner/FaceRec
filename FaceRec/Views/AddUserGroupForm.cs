using FaceRec.Core;
using FaceRec.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRec.Views
{
    public partial class AddUserGroupForm : Form
    {
        public AddUserGroupForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = this.txtName.Text.Trim();
            if (name.Length > 0)
            {
                try
                {
                    using (var store = new Store())
                    {
                        var group = store.UserGroups.FirstOrDefault(ug => ug.Name == name);
                        if (group == null)
                        {
                            group = new UserGroup();
                            group.Id = Guid.NewGuid().ToString();
                            group.Name = name;

                            store.UserGroups.Add(group);


                            store.SaveChanges();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("分组已存在，请核对", "提示");
                        }
                    }

                }
                catch (Exception error)
                {
                    MessageBox.Show("添加分组出错\r\n" + error.Message, "错误");
                }
            }
        }
    }
}
