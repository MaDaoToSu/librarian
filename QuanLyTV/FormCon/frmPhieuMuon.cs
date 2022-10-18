﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTV.FormCon
{
    public partial class frmPhieuMuon : Form
    {

        QuanLyThuVienEntities QLTV = new QuanLyThuVienEntities();


        public frmPhieuMuon()
        {
            InitializeComponent();
        }



        string tenDN;
        public frmPhieuMuon(string tenDN)
        {
            InitializeComponent();
            this.tenDN = tenDN;
            txtMaPhieu.ReadOnly = true;
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void loadAdmin()
        {
            var ListPhieuMuon = from pt in QLTV.PhieuMuons
                                select new
                                {
                                    MaPhieuMuon = pt.MaPhieuMuon,
                                    MaDG = pt.MaDG,
                                    MaNV = pt.MaNV,
                                };
            dgv.DataSource = ListPhieuMuon.ToList();
        }

        private void loadUser()
        {
            var ListPhieuTra = from pt in QLTV.PhieuMuons
                               from acc in QLTV.Accounts
                               where acc.TenTK == tenDN && pt.MaDG == acc.MaDG
                               select new
                               {
                                   MaPhieuMuon = pt.MaPhieuMuon,
                                   MaDG = pt.MaDG,
                                   MaNV = pt.MaNV,
                               };
            dgv.DataSource = ListPhieuTra.ToList();
            grbChucNang.Visible = false;
        }


        private void AddBinding()
        {
            txtMaPhieu.DataBindings.Add("Text", dgv.DataSource, "MaPhieuMuon");
            txtMaDocGia.DataBindings.Add("Text", dgv.DataSource, "MaDG");
            txtMaNhanVien.DataBindings.Add("Text", dgv.DataSource, "MaNV");
        }

        private void ClearBinhding()
        {
            txtMaPhieu.DataBindings.Clear();
            txtMaDocGia.DataBindings.Clear();
            txtMaNhanVien.DataBindings.Clear();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AddBinding();
                ClearBinhding();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmPhieuMuon_Load(object sender, EventArgs e)
        {
            try
            {
                if (tenDN != "admin")
                {
                    loadUser();
                }
                else
                {
                    loadAdmin();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {

                var parseMaDG = long.Parse(txtMaDocGia.Text);
                var parseMaNV = long.Parse(txtMaNhanVien.Text);
                var CheckMaDG = QLTV.DocGias.Where(p => p.MaDG == parseMaDG);
                var checkMaNV = QLTV.NhanViens.Where(p => p.MaNV == parseMaNV);
                if (CheckMaDG == null)
                {
                    throw new Exception("Không có mã độc giả này!!!");
                }
                else if (checkMaNV == null)
                {
                    throw new Exception("Không có mã nhân viên này!!!");
                }
                else
                {
                    var Phieu = new PhieuMuon()
                    {
                        MaDG = parseMaDG,
                        MaNV = parseMaNV,
                    };
                    QLTV.PhieuMuons.Add(Phieu);
                    QLTV.SaveChanges();
                    MessageBox.Show("Thêm phiếu mượn thành công!!!");
                    loadAdmin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                if (tenDN != "admin")
                {
                    txtMaPhieu.Text = "";
                    txtMaDocGia.Text = "";
                    txtMaNhanVien.Text = "";
                    txtTimKiem.Text = "";
                    rdbMaDocGia.Checked = false;
                    rdbMaNhanVien.Checked = false;
                    rdbMaPhieu.Checked = false;
                    loadUser();
                }
                else
                {
                    txtMaPhieu.Text = "";
                    txtMaDocGia.Text = "";
                    txtMaNhanVien.Text = "";
                    txtTimKiem.Text = "";
                    rdbMaDocGia.Checked = false;
                    rdbMaNhanVien.Checked = false;
                    rdbMaPhieu.Checked = false;
                    loadAdmin();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaPhieu.Text == "")
                {
                    throw new Exception("Vui lòng Click vào phiếu mượn muốn sửa");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn có chắc muốn sửa ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {
                        var parseMaPhieu = long.Parse(txtMaPhieu.Text);
                        var parseMaDG = long.Parse(txtMaDocGia.Text);
                        var parseMaNV = long.Parse(txtMaNhanVien.Text);
                        var checkMaPhieu = QLTV.PhieuMuons.SingleOrDefault(p => p.MaPhieuMuon == parseMaPhieu);
                        if (checkMaPhieu != null)
                        {
                            checkMaPhieu.MaDG = parseMaDG;
                            checkMaPhieu.MaNV = parseMaNV;
                            QLTV.SaveChanges();
                            MessageBox.Show("Cập nhật phiếu mượn thành công!!!");
                            loadAdmin();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật phiếu mượn thất bại!!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaPhieu.Text == "")
                {
                    throw new Exception("Vui lòng Click vào phiếu mượn muốn xóa");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn có chắc muốn Xóa ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {
                        var parseMaPhieu = long.Parse(txtMaPhieu.Text);
                        var checkMaPhieu = QLTV.PhieuMuons.SingleOrDefault(p => p.MaPhieuMuon == parseMaPhieu);
                        if (checkMaPhieu != null)
                        {
                            QLTV.PhieuMuons.Remove(checkMaPhieu);
                            QLTV.SaveChanges();
                            MessageBox.Show("Xóa thành công!!!");
                            loadAdmin();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text == "")
                {
                    throw new Exception("Vui lòng nhập phiếu muốn tìm kiếm!!!");
                }
                else if (rdbMaDocGia.Checked == false && rdbMaNhanVien.Checked == false && rdbMaPhieu.Checked == false)
                {
                    throw new Exception("Vui lòng chọn trường muốn tìm kiếm!!!");
                }
                else
                {
                    //tim Ma phieu
                    if (rdbMaPhieu.Checked == true)
                    {
                        var parseMaPhieu = long.Parse(txtTimKiem.Text);
                        var findMaPhieuAdmin = from MaPhieu in QLTV.PhieuMuons
                                               where MaPhieu.MaPhieuMuon == parseMaPhieu
                                               select new
                                               {
                                                   MaPhieuMuon = MaPhieu.MaPhieuMuon,
                                                   MaDG = MaPhieu.MaDG,
                                                   MaNV = MaPhieu.MaNV,
                                               };
                        var findMaPhieuUser = from pt in QLTV.PhieuMuons
                                              from acc in QLTV.Accounts
                                              where acc.TenTK == tenDN && pt.MaDG == acc.MaDG && pt.MaPhieuMuon == parseMaPhieu
                                              select new
                                              {
                                                  MaPhieuMuon = pt.MaPhieuMuon,
                                                  MaDG = pt.MaDG,
                                                  MaNV = pt.MaNV,
                                              };
                        if (tenDN != "admin")
                        {
                            dgv.DataSource = findMaPhieuUser.ToList();
                        }
                        else
                        {
                            dgv.DataSource = findMaPhieuAdmin.ToList();

                        }

                    }
                    //Tim Ma Doc Gia
                    else if (rdbMaDocGia.Checked == true)
                    {
                        var parseMaDocGia = long.Parse(txtTimKiem.Text);
                        var findMaMaDocGia = from MaDocGia in QLTV.PhieuMuons
                                             where MaDocGia.MaDG == parseMaDocGia
                                             select new
                                             {
                                                 MaPhieuMuon = MaDocGia.MaPhieuMuon,
                                                 MaDG = MaDocGia.MaDG,
                                                 MaNV = MaDocGia.MaNV,
                                             };
                        var findMaUser = from dg in QLTV.PhieuMuons
                                         from acc in QLTV.Accounts
                                         where acc.TenTK == tenDN && dg.MaDG == acc.MaDG && dg.MaDG == parseMaDocGia
                                         select new
                                         {
                                             MaPhieuMuon = dg.MaPhieuMuon,
                                             MaDG = dg.MaDG,
                                             MaNV = dg.MaNV,

                                         };
                        if (tenDN != "admin")
                        {
                            dgv.DataSource = findMaUser.ToList();
                        }
                        else
                        {
                            dgv.DataSource = findMaMaDocGia.ToList();
                        }
                    }
                    //Tim Ma Nhan Vien
                    else if (rdbMaNhanVien.Checked == true)
                    {
                        var parseMaNhanVien = long.Parse(txtTimKiem.Text);
                        var findNhanVien = from MaNhanVien in QLTV.PhieuMuons
                                           where MaNhanVien.MaNV == parseMaNhanVien
                                           select new
                                           {
                                               MaPhieuMuon = MaNhanVien.MaPhieuMuon,
                                               MaDG = MaNhanVien.MaDG,
                                               MaNV = MaNhanVien.MaNV,
                                           };
                        var findMaNVUser = from nv in QLTV.PhieuMuons
                                           from acc in QLTV.Accounts
                                           where acc.TenTK == tenDN && nv.MaDG == acc.MaDG && nv.MaNV == parseMaNhanVien
                                           select new
                                           {
                                               MaPhieuMuon = nv.MaPhieuMuon,
                                               MaDG = nv.MaDG,
                                               MaNV = nv.MaNV,
                                           };
                        if (tenDN != "admin")
                        {
                            dgv.DataSource = findMaNVUser.ToList();
                        }
                        else
                        {
                            dgv.DataSource = findNhanVien.ToList();
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChiTietPM_Click(object sender, EventArgs e)
        {
            if (txtMaPhieu.Text == "")
            {
                MessageBox.Show("Vui lòng chọn phiếu mượn");
            }
            else
            {
                var maphieumuon = long.Parse(txtMaPhieu.Text);
                new frmChiTietPhieuMuon(tenDN, maphieumuon).ShowDialog();

            }

        }
    }
}
