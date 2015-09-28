using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Controls;

namespace FilmReplikleri
{
    class cReplikler
    {
        private int _id;
        private string _replik;
        private string _sahibi;
        private bool _fav;

        #region Properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Replik
        {
            get { return _replik; }
            set { _replik = value; }
        }

        public string Sahibi
        {
            get { return _sahibi; }
            set { _sahibi = value; }
        }

        public bool Fav
        {
            get { return _fav; }
            set { _fav = value; }
        }
        #endregion

        cGenel gnl = new cGenel();

        public int ToplamReplikSayisi()
        {
            int toplamReplikSayisi = 0;

            SqlConnection conn = new SqlConnection(gnl.connStr);

            //Max ID'yi alıyoruz
            SqlCommand comm = new SqlCommand("select max(ID) from FilmReplikleri", conn);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                toplamReplikSayisi = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {
                conn.Close();
            }


            return toplamReplikSayisi + 1;
        }

        //TO-DO sahibi alt satır ve sağa yatık olmalı, sql'den coklu satır gelirse aşağı satıra almalı
        public string[] RasgeleReplikGetir()
        {
            string[] replik = new string[4];

            SqlConnection conn = new SqlConnection(gnl.connStr);

            SqlCommand comm = new SqlCommand("SELECT TOP 1 ID, Replik, Sahibi, Fav FROM FilmReplikleri ORDER BY NEWID()", conn);

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SqlDataReader dr = comm.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    try
                    {
                        replik[0] = dr[0].ToString();
                        replik[1] = dr[1].ToString();
                        replik[2] = dr[2].ToString();
                        replik[3] = dr[3].ToString();
                    }
                    catch (SqlException ex)
                    {
                        string hata = ex.Message;
                    }
                }

            }
            dr.Close();
            conn.Close();
            return replik;
        }

        public string[] RasgeleFavReplikGetir()
        {
            string[] favReplik = new string[4];

            SqlConnection conn = new SqlConnection(gnl.connStr);

            SqlCommand comm = new SqlCommand("SELECT TOP 1 ID, Replik, Sahibi, Fav FROM FilmReplikleri where fav = 1 ORDER BY NEWID()", conn);

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    try
                    {
                        favReplik[0] = dr[0].ToString();
                        favReplik[1] = dr[1].ToString();
                        favReplik[2] = dr[2].ToString();
                        favReplik[3] = dr[3].ToString();
                    }
                    catch (SqlException ex)
                    {
                        string hata = ex.Message;
                    }
                }

            }
            dr.Close();
            conn.Close();
            return favReplik;
        }

        public bool FavEkle(cReplikler r)
        {
            bool sonuc = false;

            SqlConnection conn = new SqlConnection(gnl.connStr);

            SqlCommand comm = new SqlCommand("Update FilmReplikleri set Fav = @Fav where ID=@ID", conn);
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = r._id;
            comm.Parameters.Add("@Fav", SqlDbType.Bit).Value = true;

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());

            conn.Close();

            return sonuc;
        }

        public bool FavCikar(cReplikler r)
        {
            bool sonuc = false;

            SqlConnection conn = new SqlConnection(gnl.connStr);

            SqlCommand comm = new SqlCommand("Update FilmReplikleri set Fav = @Fav where ID=@ID", conn);
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = r._id;
            comm.Parameters.Add("@Fav", SqlDbType.Bit).Value = false;

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            sonuc = Convert.ToBoolean(comm.ExecuteNonQuery());

            conn.Close();

            return sonuc;
        }

        //Rewrite
        public bool FavKontrol(int id)
        {
            bool sonuc = false;

            SqlConnection conn = new SqlConnection(gnl.connStr);

            SqlCommand comm = new SqlCommand("select fav from FilmReplikleri where ID = @ID", conn);
            comm.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            if(Convert.ToBoolean(comm.ExecuteScalar()))
            {
                sonuc = true;
            }

            
            conn.Close();

            return sonuc;
        }

    }
}
