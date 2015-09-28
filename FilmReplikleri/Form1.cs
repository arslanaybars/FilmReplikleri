using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilmReplikleri
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();

        }

        string[] oncekiReplik = new string[3];
        string[] oncekiFavReplik = new string[3];
        bool oncekiDurum = false;
        bool oncekiFavDurum = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            cReplikler r = new cReplikler();
            tbReplik.SelectedIndex = 0;

            txtReplikDoldur();
            txtFavReplikDoldur();

            FavDurumunaGoreButon();

        }

        private void FavDurumunaGoreButon()
        {
            cReplikler r = new cReplikler();
            if (tbReplik.SelectedIndex == 0)
            {
                if (!r.FavKontrol(Convert.ToInt32(txtID.Text)))
                {
                    btnFav.Text = "Favorilere Ekle";
                }
                else
                {
                    btnFav.Text = "Favorilere Çıkar";
                }
            }
            else if (tbReplik.SelectedIndex == 1)
            {

                if (!r.FavKontrol(Convert.ToInt32(txtFavID.Text)))
                {
                    btnFav.Text = "Favorilere Ekle";
                }
                else
                {
                    btnFav.Text = "Favorilere Çıkar";
                }


            }
        }

        private void Temizle()
        {
            txtID.Clear();
            txtFavID.Clear();
            txtReplik.Clear();
            txtFav.Clear();
            txtFavFavState.Clear();
            txtFavState.Clear();
        }

        private void ReplikTemizle()
        {
            txtID.Clear();
            txtFavState.Clear();
            txtReplik.Clear();
        }

        private void FavReplikTemizle()
        {
            txtFavID.Clear();
            txtFavFavState.Clear();
            txtFavState.Clear();
        }

        private void btnOyla_Click(object sender, EventArgs e)
        {
            //Go Github
            ProcessStartInfo p1 = new ProcessStartInfo("https://github.com/arslanaybars/FilmReplikleri");
            Process.Start(p1);
        }

        private void btnHakkinda_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "Bu uygulama Aybars Arslan tarafından tasarlanmıştır. \n" +
            "İstek, öneri ve görüşleriniz için arslanaybars@gmail.com adresine mailinizi atabilirsiniz.", "Hakkında", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnKullanimKlavuzu_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "1 - Replikler rasgele olarak tabviewin içerisinde gösterilmektedir.\r\n" +
            "2 - Yeni Söze geçmek için TextBoxta bulunun Repliğe tıklamanız yeterlidir.\r\n" +
            "3 - Fav isimli butona tıklayarak istediğiniz replikleri favori olarak seçebilirsiniz.\r\n" +
            "4 - Favoriler tabına geçerek daha önce favorilere aldığını replikleri görebilirsiniz.\r\n", "Kullanım Klavuzu", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnOnceki_Click(object sender, EventArgs e)
        {
            if (tbReplik.SelectedIndex == 0)
            {
                if (oncekiDurum)
                {
                    txtID.Text = oncekiReplik[0];
                    txtReplik.Text = oncekiReplik[1];
                    txtFavState.Text = oncekiReplik[2];
                }
                else
                {
                    btnFav.Enabled = false;
                    txtReplik.Text = "Önceki replik bulunamadı";

                }

            }
            else if (tbReplik.SelectedIndex == 1)
            {
                if (oncekiFavDurum)
                {
                    txtFavID.Text = oncekiFavReplik[0];
                    txtFav.Text = oncekiFavReplik[1];
                    txtFavFavState.Text = oncekiFavReplik[2];
                }
                else
                {
                    btnFav.Enabled = false;
                    txtFav.Text = "Önceki favoriye alınmış replik bulunamadı.";
                }
            }
        }

        private void txtReplik_Click(object sender, EventArgs e)
        {
            OncekiDurumReplik();
            FavDurumunaGoreButon();
            btnFav.Enabled = true;
        }

        private void OncekiDurumReplik()
        {
            oncekiDurum = true;

            oncekiReplik[0] = txtID.Text;
            oncekiReplik[1] = txtReplik.Text;
            oncekiReplik[2] = txtFavState.Text;

            txtReplikDoldur();
        }

        private void OncekiDurumReplik(bool favState)
        {
            oncekiDurum = true;

            oncekiReplik[0] = txtID.Text;
            oncekiReplik[1] = txtReplik.Text;
            oncekiReplik[2] = favState.ToString();

            txtReplikDoldur();
        }

        private void txtFav_Click(object sender, EventArgs e)
        {
            OncekiFavDurum();
            FavDurumunaGoreButon();
            btnFav.Enabled = true;
        }

        private void OncekiFavDurum()
        {
            oncekiFavDurum = true;

            oncekiFavReplik[0] = txtFavID.Text;
            oncekiFavReplik[1] = txtFav.Text;
            oncekiFavReplik[2] = txtFavFavState.Text;

            txtFavReplikDoldur();
        }

        private void OncekiFavDurum(bool favFavState)
        {
            oncekiFavDurum = true;

            oncekiFavReplik[0] = txtFavID.Text;
            oncekiFavReplik[1] = txtFav.Text;
            oncekiFavReplik[2] = favFavState.ToString();

            txtFavReplikDoldur();
        }

        private void txtReplikDoldur()
        {
            cReplikler r = new cReplikler();
            string[] replik = r.RasgeleReplikGetir();

            txtID.Text = replik[0];
            txtReplik.Text = replik[1] + "\r\n \r\n" + replik[2];
            txtFavState.Text = replik[3];
        }

        private void txtFavReplikDoldur()
        {
            cReplikler r = new cReplikler();
            string[] favReplik = r.RasgeleFavReplikGetir();

            txtFavID.Text = favReplik[0];
            txtFav.Text = favReplik[1] + "\r\n \r\n" + favReplik[2];
            txtFavFavState.Text = favReplik[3];
        }

        private void btnFav_Click(object sender, EventArgs e)
        {
            cReplikler r = new cReplikler();
            r.Id = Convert.ToInt32(txtID.Text);
            //r.Fav = !(Convert.ToBoolean(txtFavState.Text));
            if (tbReplik.SelectedIndex == 0)
            {
                if (!r.FavKontrol(Convert.ToInt32(txtID.Text)))
                {
                    if (r.FavEkle(r))
                    {
                        MessageBox.Show("Favorilere Kayıt Edildi.");
                        OncekiDurumReplik(true);
                        txtReplikDoldur();
                    }
                    else
                    {
                        MessageBox.Show("Zaten Favorilerde");
                    }

                }
                else
                {
                    if (r.FavCikar(r))
                    {
                        MessageBox.Show("Favorilerden Çıkarıldı.");
                        OncekiDurumReplik(false);
                        txtReplikDoldur();
                    }
                    else
                    {
                        MessageBox.Show("Zaten Favorilerde");
                    }
                }
            }
            else if (tbReplik.SelectedIndex == 1)
            {
                if (!r.FavKontrol(Convert.ToInt32(txtID.Text)))
                {
                    if (r.FavEkle(r))
                    {
                        MessageBox.Show("Favorilere Kayıt Edildi.");
                        OncekiFavDurum(true);
                        txtFavReplikDoldur();
                    }
                    else
                    {
                        MessageBox.Show("Zaten Favorilerde");
                    }

                }
                else
                {
                    if (r.FavCikar(r))
                    {
                        MessageBox.Show("Favorilerden Çıkarıldı.");
                        OncekiFavDurum(false);
                        txtFavReplikDoldur();
                    }
                    else
                    {
                        MessageBox.Show("Zaten Favorilerde");
                    }
                }
            }

            FavDurumunaGoreButon();
        }

        bool tabChange = false;

        private void tbReplik_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (tabChange)
            {
                tabChange = true;
                FavDurumunaGoreButon();
            }
            else
            {
                tabChange = false;
                FavDurumunaGoreButon();
            }

        }

        }
}
