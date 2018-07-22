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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();

            this.dgvUsers.AutoGenerateColumns = false;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (var store = new Store())
                {
                    var groupsCount = store.UserGroups.Count();
                    this.btnAddUser.Enabled = (groupsCount > 0);

                    this.BindUserDataSource(store);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("加载数据失败\r\n"+error.Message, "错误");
            }
        }


        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            var addUserGroupForm = new AddUserGroupForm();
            var result = addUserGroupForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.btnAddUser.Enabled = true;
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var addUserForm = new AddUserForm();
            var result = addUserForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    using (var store = new Store())
                    {
                        this.BindUserDataSource(store);

                        ProgramContext.InitializeKnowUsers(store);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("加载用户数据失败\r\n" + error.Message, "错误");
                }
            }
        }

        private void BindUserDataSource(Store store)
        {
            var userViews = store.UserViews.ToArray();
            this.dgvUsers.DataSource = userViews;
        }


        private void dgvUsers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

        }
    }
}
