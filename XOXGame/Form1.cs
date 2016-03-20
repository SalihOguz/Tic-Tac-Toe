using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOXGame
{
    public partial class Form1 : Form
    {
                                                                                          //diğer: false A planı ikinci turda ilk turdakinin komşu köşesine oynanıyor
        public List<PictureBox> resim = new List<PictureBox>();                          //bool defans o tur defans yapılıp yapılmadığını belirler, bitirdi o tur bitirici hamle oynanabilirliğini gösterir
        public bool sıra = false, kazandı=false, turkazanma=false, beraberlik=true, mod= Form2.oyunmodu, defans=false, bitirdi=false, diğer = false; //sıra: false = x, True = o | mod: false = tek kişilik, true= çift kişilik 
        public string[] kutu = new string[17]; //for'da hata olmaması için 17, sadece 9 tane kullanılıyor              
        public int tıklanan, xpuan=0, opuan=0, kat=4, el, Aadım=1, Badım=1, Cadım=1, extıklanan; //kat çapraz üçleme için, el oynanan sırayı sayar, A,B,C adım planlarda kaçıncı adımda olduğu
        public string pcPlan;
        public Random üret = new Random();

        public Form1()
        {
            InitializeComponent();

            resim.Add(pictureBox1);
            resim.Add(pictureBox2);
            resim.Add(pictureBox3);
            resim.Add(pictureBox4);
            resim.Add(pictureBox5);
            resim.Add(pictureBox6);
            resim.Add(pictureBox7);
            resim.Add(pictureBox8);
            resim.Add(pictureBox9);
        }

        void sıradegis()
        {
            if(el>=4)
            kazanma();

            el++;

            if (turkazanma == false)
            {
                sıra = !sıra;

                if (sıra == false)
                {
                    pictureBox12.ImageLocation = (Application.StartupPath + "\\resimler\\x.png");
                    if (mod == false)//tek kişilikse sıra bilgisayara geçer, değilse x oyuncusunun tıklaması beklenir
                    {
                        pckarar();
                    }
                }
                else
                {
                    pictureBox12.ImageLocation = (Application.StartupPath + "\\resimler\\o.png");
                    resimErişimi();
                }
            }
        }

        void tıklama() //hangi karede x var hangisinde o var
        { 
        if (sıra == false)
            {
                resim[tıklanan].ImageLocation = (Application.StartupPath + "\\resimler\\x.png");
                kutu[tıklanan] = "x";
            }
            else
            {
                resim[tıklanan].ImageLocation = (Application.StartupPath + "\\resimler\\o.png");
                kutu[tıklanan] = "o";
            }
        }

        void resimdegis (int a, int b, int c) //üçleme yapınca üstüne tik veya yuvarlak koyuyur
        {
            if (sıra == false)
            {
                xpuan++;
                label4.Text = xpuan.ToString();
                resim[a].ImageLocation = (Application.StartupPath + "\\resimler\\x1.png");
                resim[b].ImageLocation = (Application.StartupPath + "\\resimler\\x1.png");
                resim[c].ImageLocation = (Application.StartupPath + "\\resimler\\x1.png");
            }
            else
            {
                opuan++;
                label5.Text = opuan.ToString();
                resim[a].ImageLocation = (Application.StartupPath + "\\resimler\\o1.png");
                resim[b].ImageLocation = (Application.StartupPath + "\\resimler\\o1.png");
                resim[c].ImageLocation = (Application.StartupPath + "\\resimler\\o1.png");
            }
            oyunturbitti();
        }

        void oyunturbitti()// oyun veya tur bitti, tur için beraberlik de olabilir
        {
            for (int i = 0; i < 9; i++) //oyunu sıfırlamaya başlıyor
            {
                kutu[i] = null;
                resim[i].Enabled = false;
            }

            if (xpuan == Form2.tursayisi)//Oyunu tamamen bitiriyor
            {
                label1.Visible = true;
                button1.Visible = true;
                label1.Text = (Form2.ad1 + " Kazandı");
            }
            else if (opuan == Form2.tursayisi)
            {
                label1.Visible = true;
                button1.Visible = true;
                label1.Text = (Form2.ad2 + " Kazandı");
            }
            else //Oyun bitmediyse diğer tur düğmesi çıkıyor
            {
                el = 1;
                turkazanma = true;
                pictureBox12.ImageLocation = (Application.StartupPath + "\\resimler\\ileri.png");
                label6.Text = ("Diğer Tur İçin Tıklayın");
            }
        }

        void kazanma()
        {
            for(int i=0;i<7;i+=3) //Yatay üçlemeler
            {
                if (kutu[i] == kutu[i+1] && kutu[i+1] == kutu[i + 2] && kutu[i]!=null)
                {
                        resimdegis(i, i + 1, i + 2);
                        break;
                }
            }
            for (int i = 0; i < 3; i++) //Dikey üçlemeler
            {
                if (kutu[i] == kutu[i + 3] && kutu[i + 3] == kutu[i + 6] && kutu[i] != null)
                {
                        resimdegis(i, i + 3, i + 6);
                        break;
                }
            }
            for (int i = 0; i < 3; i+=2) //Çapraz üçlemeler
            {
                if (kutu[i] == kutu[i + kat] && kutu[i + kat] == kutu[i + (2 * kat)] && kutu[i] != null)
                {
                        resimdegis(i, i + kat, i + 2*kat);
                        break;
                }
                kat = kat / 2;
            }
            kat = 4; //kat'ı sıfırlamak

            beraberlik = true;

            for (int i = 0; i < 9; i++)
            {
                if (kutu[i] == null)
                {
                    beraberlik = false;
                    break;
                }
            }
            if (beraberlik == true)//tur berabere
            {
                oyunturbitti();
            }
        }

        #region pictureBoxClick Olayları

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tıklanan = 0;
            tıklama();
            sıradegis();
            pictureBox1.Enabled = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tıklanan = 1;
            tıklama();
            sıradegis();
            pictureBox2.Enabled = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            tıklanan = 2;
            tıklama();
            sıradegis();
            pictureBox3.Enabled = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            tıklanan = 3;
            tıklama();
            sıradegis();
            pictureBox4.Enabled = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            tıklanan = 4;
            tıklama();
            sıradegis();
            pictureBox5.Enabled = false;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            tıklanan = 5;
            tıklama();
            sıradegis();
            pictureBox6.Enabled = false;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            tıklanan = 6;
            tıklama();
            sıradegis();
            pictureBox7.Enabled = false;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            tıklanan = 7;
            tıklama();
            sıradegis();
            pictureBox8.Enabled = false;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            tıklanan = 8;
            tıklama();
            sıradegis();
            pictureBox9.Enabled = false;
        }

        #endregion pictureBoxClick Olayları

        private void pictureBox12_Click(object sender, EventArgs e)//Yeni Tur Düğmesi, Oyunu sıfırlıyor
        {
            if (turkazanma == true)
            {
                for (int i = 0; i < 9; i++)
                {
                    resim[i].ImageLocation = null;
                    resim[i].Enabled = true;
                }
                sıra = false;
                kat = 4;
                el = 1;
                Aadım = 1;
                Badım = 1;
                Cadım = 1;
                extıklanan = 10;
                tıklanan = 10;
                turkazanma = false;
                pictureBox12.ImageLocation = (Application.StartupPath + "\\resimler\\x.png");
                label6.Text = ("Sıradaki Oyuncu");
                if (mod == false)
                pckarar();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = Form2.ad1;
            label3.Text = Form2.ad2;
            el = 0;
            if(mod==false)
            pckarar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
            label1.Visible = false;
            button1.Visible = false;
            xpuan = 0;
            opuan = 0;
        }
        # region singleplayer
        void resimErişimi()//tek kisilik modda oyuncunun bilgisayarın oyununa karışmaması için
        {
            if(sıra==false)
            for (int i = 0; i < 9; i++)
            {
                resim[i].Enabled = false;
            }
            else 
            for (int i = 0; i < 9; i++)
            {
                if (kutu[i] == null)
                    resim[i].Enabled = true;
            }
        }

        void pckarar()
        {
            resimErişimi();
            defans = false;
            bitirdi = false;

            pcPlan = "a";
            //if (el == 0) //oyunun başına a,b veya c planını rastgele seçiyor
            //{
            //    int planbelirle;
            //    planbelirle = üret.Next(0, 100);
            //    if (planbelirle <= 40) //%40 ihtimalle plan a
            //        pcPlan = "a";
            //    else if (planbelirle <= 65) //%25 plan b
            //        pcPlan = "b";
            //    else                       //%35 plan c
            //        pcPlan = "c";
            //}

           tıklanan=extıklanan;
           diğer = false;

            if (el > 2)//ilk iki tur haricinde tehlike var mı diye bakıyor ve önlem alıyor
                planDefansBitiris();

            if (pcPlan == "c" && defans == false && bitirdi == false)
                planC();
            if (pcPlan == "a" && defans == false && bitirdi == false)
                planA();
            if (pcPlan == "b" && defans == false && bitirdi == false)
                planB();
            

            extıklanan = tıklanan;
            tıklama();
            resim[tıklanan].Enabled = false;
            sıradegis();
            
        }

        void planA()//Üç köşeyi ele geçirmeye dayalı bir taktik (Oynanma ihtimali %40)
        {
            int karar;

            if (Aadım == 1)//A planı için ilk elde yapılacak hamle
            {
                karar = üret.Next(0,20);
                if (karar <=5)
                    tıklanan = 0;
                else if (karar <=10)
                    tıklanan = 2;
                else if (karar <=15)
                    tıklanan = 6;
                else
                    tıklanan = 8;

                extıklanan = tıklanan;
            }
            else if (Aadım == 2)//A planı için 2. Adım
            {
                

                //önceki tur seçilen karenin çapraz karşısı boş mu diye bakıyor, eğer boşsa rastgele bir sayı tutulup 2 ihtimalden birisi oynanıyor.
                if (extıklanan == 0 && kutu[8] != "o" || extıklanan == 2 && kutu[6] != "o" || extıklanan == 8 && kutu[0] != "o" || extıklanan == 6 && kutu[2] != "o")
                {
                    karar = üret.Next(0, 100);
                    if (karar < 75)// %75 ihtimal çıkarsa öncekinin işaretlenen karenib çapraz karşısındakine oynanıyor
                    {
                        if (extıklanan == 0)
                            tıklanan = 8;
                        else if (extıklanan == 2)
                            tıklanan = 6;
                        else if (extıklanan == 8)
                            tıklanan = 0;
                        else if (extıklanan == 6)
                            tıklanan = 2;

                        diğer=true;
                    }
                }
                if (diğer == false)//Eğer önceki tur oynanan karenin çapraz karşısı boş değilse veya %25lik ihtimal çıkarsa komşu köşelerden birisine oynanıyor
                {
                    karar = üret.Next(0, 10);
                    if (extıklanan == 0 || extıklanan == 8)
                    {
                        if (karar < 5 && kutu[6] != "o")
                            tıklanan = 6;
                        else if (kutu[2] != "o")
                            tıklanan = 2;
                        else
                            tıklanan = 6;
                    }
                    if (extıklanan == 2 || extıklanan == 6)
                    {
                        if (karar < 5 && kutu[0] != "o")
                            tıklanan = 0;
                        else if (kutu[8] != "o")
                            tıklanan = 8;
                        else
                            tıklanan = 0;
                    }
                }
            }
            else if (Aadım == 3)//A Planı Üçüncü Adım
            {
                if (kutu[6] == null)
                            tıklanan = 6;
                else if (kutu[8] == null)
                            tıklanan = 8;
                else if (kutu[2] == null)
                            tıklanan = 2;
                else if (kutu[0] == null)
                            tıklanan = 0;

            }
            else if (Aadım == 4)//A Planı 4. Adım
            {
                for(int i=0;i<9;i++)
                {
                    if (kutu[i] == null)
                    {
                        tıklanan = i;
                        break;
                    }
                }

            }
            Aadım++;
        }

        void planB() //iki koşu köşeyi ve ortayı ele geçirmeye dayalı bir taktik (Oynanma ihtimali %25)
        {


            Badım++;
        }

        void planC() //Bir köşe, o köşenin komşu karesi ve ortayı ele geçirmeye dayalı taktik. (Oynanma ihtimali %35)
        {
            int karar;

            if (Cadım == 1) //C planı için ilk elde yapılacak hamle
            {
                karar = üret.Next(0, 20);
                if (karar <= 5)
                    tıklanan = 0;
                else if (karar <= 10)
                    tıklanan = 2;
                else if (karar <= 15)
                    tıklanan = 6;
                else
                    tıklanan = 8;

                extıklanan = tıklanan;
            }
            else if (Cadım == 2) //C planı 2. adım
            {
                if (kutu[4] == null)
                    tıklanan = 4;
                else
                    pcPlan = "a";
            }
            else if (Cadım == 3) //C planı 3. adım
            {
                karar = üret.Next(0, 10);
                if (karar <= 10)//tıklanan 1 veya 7 olacak
                {
                    if (extıklanan == 0 || extıklanan == 6)
                        tıklanan = extıklanan + 1;
                    else
                        tıklanan = extıklanan - 1;
                }
                else//tıklanan 3 veya 5 olacak
                {
                    if (extıklanan == 0 || extıklanan == 2)
                        tıklanan = extıklanan + 3;
                    else
                        tıklanan = extıklanan - 3;
                }
            }
            
            Cadım++;
        }

        void planDefansBitiris()//Eakibin üçleme yapma tehlikesi var mı diye bakılıyor, varsa ona göre oynanıyor 21 ihtimal gözden geçirilmeli & Bitirmek için hamle
        {
            int a=0;
            string karakter = "x";
            while (a < 2)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (kutu[i] == karakter && kutu[i + 1] == karakter)//Satırlarda tehlike var mı kontrol ediliyor
                    {
                        if ((i == 0 || i == 3 || i == 6) && kutu[i + 2] == null)
                            tıklanan = i + 2;
                        if ((i == 1 || i == 4 || i == 7) && kutu[i - 1] == null)
                            tıklanan = i - 1;
                    }
                    if (kutu[i] == karakter && kutu[i + 3] == karakter)//Sütunlarda tehlike var mı kontrol ediliyor
                    {
                        if ((i == 0 || i == 1 || i == 2) && kutu[i + 6] == null)
                            tıklanan = i + 6;
                        if ((i == 3 || i == 4 || i == 5) && kutu[i - 3] == null)
                            tıklanan = i - 3;
                    }
                    if (kutu[i] == karakter && kutu[i + 4] == karakter)//sağ yukarıdan çapraz kontrolü
                    {
                        if (i == 0 && kutu[8] == null)
                            tıklanan = 8;
                        if (i == 4 && kutu[0] == null)
                            tıklanan = 0;
                        if (i == 2 && kutu[4] == null)//sağdan çaprazda başı ve sonu dolu ortası boş ise
                            tıklanan = 4;
                    }
                    if (kutu[i] == karakter && kutu[i + 2] == karakter)
                    {
                        if (i == 2 && kutu[6] == null)//sağ üstten çapraz
                            tıklanan = 6;
                        if (i == 4 && kutu[2] == null)//sağ üstten çapraz
                            tıklanan = 2;
                        if ((i == 0 || i == 3 || i == 6) && kutu[i + 1] == null)//satırlarda iki köşe dolu ortası boş ise
                            tıklanan = i + 1;
                    }
                    if (kutu[i] == karakter && kutu[i + 6] == karakter && kutu[i + 3] == null)//sütunlarda sütunun başı ve sonu dolu ortası boş ise
                        tıklanan = i + 3;
                    if (kutu[i] == karakter && kutu[i + 8] == karakter && kutu[i + 4] == null)//soldan çaprazda başı ve sonu dolu ortası boş ise
                        tıklanan = i + 4;
                }
                if (tıklanan != extıklanan && karakter == "x")//Bu el üçleme yapılmış
                {
                    a++;
                    bitirdi = true;
                }
                else if (tıklanan != extıklanan && karakter == "o")//Bu el savunma yapılmış
                    defans = true;
                a++;
                karakter = "o";
            }
        }

#endregion singleplayer
    }
}
