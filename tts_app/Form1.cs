using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace tts_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> sonListe = new List<string>();

        private void button1_Click_1(object sender, EventArgs e)
        {
            kelimeAyir();
            label1.Text = string.Join(" ", sonListe);

            foreach (string item in sonListe)
            {
                    SoundPlayer player = new SoundPlayer();
                    string sesDosyasiYolu = $@"C:\Users\emree\source\repos\tts_app\tts_app\Resources\{item}.wav";
                    player.SoundLocation = sesDosyasiYolu;

                 if (soundExist($"{item}.wav")){
                    player.PlaySync();
                    } 
            }
        }

        public static bool soundExist(string dosyaAdi)
        {
            string sesDosyalariKlasoru = @"C:\Users\emree\source\repos\tts_app\tts_app\Resources";
            string sesDosyasiYolu = Path.Combine(sesDosyalariKlasoru, dosyaAdi);
             
            return File.Exists(sesDosyasiYolu);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox objTextBox = (TextBox)sender;
            text = objTextBox.Text;
        }

        string text = "";
        private void kelimeAyir()
        {
            sonListe.Clear(); 
            string kelime = "";
            string harfler = "abcçdefgğhıijklmnoöprsştuüvyzqwx";
            for (int n = 0; n < text.Length; n++)
            {
                char h = text[n];
                if (harfler.Contains(char.ToLower(h).ToString()))
                {
                    kelime += h;
                }
                else if (!string.IsNullOrEmpty(kelime))
                {
                    sonListe.AddRange(Hecele(kelime).Split('-'));
                    kelime = "";
                }
            }
            if (!string.IsNullOrEmpty(kelime))
            {
                sonListe.AddRange(Hecele(kelime).Split('-'));
            }
        }

        private string Hecele(string kel)
        {
            string sesliler = "aeıioöuü";
            int boyut = kel.Length;
            string bitli = "-";
            for (int n = 0; n < boyut; n++)
            {
                char h = kel[n];
                if (sesliler.Contains(char.ToLower(h).ToString()))
                {
                    bitli += "1";
                }
                else
                {
                    bitli += "0";
                }
            }
            bitli += "-";

            bitli = bitli
                .Replace("10001", "10-001")
                .Replace("10101", "101-01")
                .Replace("1001", "10-01")
                .Replace("0101", "01-01")
                .Replace("-101", "-1-01")
                .Replace("0110", "01-10");

            int p = 1;
            string heceler = "";
            for (int n = 0; n < bitli.Length; n++)
            {
                if (bitli[n] == '-')
                {
                    heceler += "-";
                }
                else
                {
                    heceler += kel[p - 1];
                    p++;
                }
            }
            return heceler.Substring(1, heceler.Length - 1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
