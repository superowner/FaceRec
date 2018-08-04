using DlibDotNet;
using DlibDotNet.Tools;
using FaceRec.Core;
using FaceRec.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRec.Views
{
    public partial class AddUserForm : Form
    {
        private Matrix<float> faceEncoding;

        public AddUserForm()
        {
            InitializeComponent();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (var store = new Store())
                {
                    this.combGroup.DataSource = store.UserGroups.ToArray();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("加载分组数据失败\r\n" + error.Message, "错误");
            }
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            var addUserGroupForm = new AddUserGroupForm();
            var result = addUserGroupForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (var store = new Store())
                {
                    this.combGroup.DataSource = store.UserGroups.ToArray();
                }
            }
        }

        private void picFace_Click(object sender, EventArgs e)
        {
            this.openFileDialog.DefaultExt = "jpg";
            this.openFileDialog.Filter = "所有图片|*.jpg;*.jpeg;*.png;*.bmp";
            this.openFileDialog.Title = "选择图片";
            var result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var config = ProgramContext.Current.Config;
                var recognitor = ProgramContext.Current.Recognitor;
                var fileName = this.openFileDialog.FileName;
                Array2D<RgbPixel> img;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    img = Dlib.LoadImage<RgbPixel>(fileName.EnsureNonChineseCharsFileName());
                    this.Cursor = Cursors.Default;
                }
                catch (Exception error)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show($"加载图片失败。\r\n{error.Message}", " 错误");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                var rects = recognitor.FaceLocations(img, config.UpSampleTimes, config.EnableGPUAcceleration ? "cnn" : "hog");
                this.Cursor = Cursors.Default;
                if (rects.Length == 0)
                {
                    MessageBox.Show("检测人脸失败，可能图片中人脸太小", "提示");
                    return;
                }
                if (rects.Length > 1)
                {
                    MessageBox.Show("检测失败，请选择有并且只有一张人脸的照片", "提示");
                    return;
                }
                var faceEncodings = recognitor.FaceEncodings(img, rects);
                this.faceEncoding = faceEncodings[0];

                var image = Image.FromFile(this.openFileDialog.FileName);
                var rect = rects[0].ToSystemRectangle();
                var faceImage = new Bitmap(rect.Width, rect.Height);
                using (var g = Graphics.FromImage(faceImage))
                {
                    g.DrawImage(image, new System.Drawing.Rectangle(0, 0, faceImage.Width, faceImage.Height), rect, GraphicsUnit.Pixel);
                    this.picFace.Image = faceImage;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = this.txtName.Text.Trim();
            var group = (UserGroup)this.combGroup.SelectedItem;
            var image = this.picFace.Image;
            var comment = this.txtComment.Text.Trim();
            if (name.Length > 0 && group != null && image != null)
            {
                try
                {
                    using (var store = new Store())
                    {
                        var user = store.Users.FirstOrDefault(u => u.Name == name);
                        if (user == null)
                        {
                            var originEncoding = this.faceEncoding.ToBytes();
                            var inputStream = new MemoryStream(originEncoding);
                            var outputStream = new MemoryStream();
                            using (GZipStream compressionStream = new GZipStream(outputStream, CompressionMode.Compress))
                            {
                                inputStream.CopyTo(compressionStream);
                            }

                            user = new User();
                            user.Id = Guid.NewGuid().ToString();
                            user.Name = name;
                            user.GroupId = group.Id;
                            user.Face = image.ToBytes(ImageFormat.Jpeg); // less size
                            user.Encoding = outputStream.ToArray();
                            user.Comment = comment;
                            store.Users.Add(user);

                            store.SaveChanges();
                            this.faceEncoding.Dispose();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("用户已存在，请核对", "提示");
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("添加用户出错\r\n" + error.Message, "错误");
                }
            }
            else
            {
                MessageBox.Show("请完成必填内容", "提示");
            }
        }
    }
}
