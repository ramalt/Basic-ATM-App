using System;
using System.Collections.Generic;
using System.Linq;


namespace RamBankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.Clear();
            dbContext db = new dbContext();
            Repository rp = new Repository();
            Section sec = new Section();


            rp.LoadingAnimation(10);
            rp.giris();

        }
        public class Hesap
        {
            public int hesapId { get; set; }
            public int hesapNo { get; set; }
            public int parola { get; set; }
            public int ibanNo { get; set; }
            public decimal bakiye { get; set; }
        }
        public class dbContext
        {
            public List<Hesap> hesaps = new List<Hesap>()
        {
            new Hesap(){hesapId=1,hesapNo=1111,parola=0123,ibanNo=1111,bakiye=21.00M},
            new Hesap(){hesapId=2,hesapNo=1112,parola=0124,ibanNo=2111,bakiye=143.023M},
            new Hesap(){hesapId=3,hesapNo=1113,parola=0131,ibanNo=3111,bakiye=456.67M},
            new Hesap(){hesapId=4,hesapNo=1114,parola=0132,ibanNo=4111,bakiye=234.56M},
            new Hesap(){hesapId=5,hesapNo=1121,parola=0133,ibanNo=1211,bakiye=678.23M},
            new Hesap(){hesapId=6,hesapNo=1122,parola=0134,ibanNo=2211,bakiye=250.25M},
            new Hesap(){hesapId=7,hesapNo=1123,parola=0141,ibanNo=3211,bakiye=2345.45M},
            new Hesap(){hesapId=8,hesapNo=1124,parola=0142,ibanNo=4211,bakiye=456.23M},
            new Hesap(){hesapId=9,hesapNo=1131,parola=0143,ibanNo=1311,bakiye=34.009M},

        };
        }
        public class Section
        {
            public int hesapNumarasi { get; set; }
        }
        public class Repository
        {
            dbContext db = new dbContext();
            Section sec = new Section();
            /////////////////////////////////////ANİMASYON
            public void LoadingAnimation(int beklemeSüresi = 5, string yuklemeMesajı = "")
            {
                Console.Write(yuklemeMesajı);
                int bekle = 0;
                for (int i = 0; i <= 3; i++)
                {
                    Console.Write("* ");
                    System.Threading.Thread.Sleep(200);
                    if (i == 3)
                    {
                        Console.Write(" \n");
                        i = 0;
                        Console.Clear();

                    }
                    bekle++;
                    if (bekle == beklemeSüresi)
                    {
                        break;
                    }
                }
            }
            /////////////////////////////////////GİRİŞ
            public void giris()
            {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("\t\t\tGiriş ekranına hoşgeldiniz...");
                Console.WriteLine("------------------------------------------------------------------------\n");
                Console.WriteLine("hesap no:");
                int hspNo = Convert.ToInt32(Console.ReadLine());

                foreach (var item in db.hesaps)
                {
                    
                    if (item.hesapNo == hspNo)
                    {
                        Console.WriteLine("parola:");
                        int pwd = Convert.ToInt32(Console.ReadLine());
                        if (item.parola == pwd)
                        {
                            Console.WriteLine("giriş başarılı.");
                            sec.hesapNumarasi = hspNo;
                            LoadingAnimation(10, "meenüye yönlendiriyosunuz.");
                            menuGoster();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("şifreniz yanlış.");
                        }
                    }
                    if (item.hesapNo != hspNo)
                    {
                        Console.WriteLine("kullanıcı yanlış");
                    }
                }

            }
            ///////////////////////////////////// MENÜ
            public void menuGoster()
            {
                Console.Clear();
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine(" Lütfen aşağıdaki menüden yapmak istediğiniz işlemin numarasını giriniz.");
                Console.WriteLine("------------------------------------------------------------------------\n");
                Console.WriteLine("1- PARA ÇEKME\t\t\t\t2- PARA YATIRMA\n3- PARA GONDERME\t\t\t4- HESAP BİLGİSİ\n");
                Console.Write("seçiminiz: ");
                int secimNo = Convert.ToInt32(Console.ReadLine());
                sayfaYonlendir(secimNo);
            }

            public void sayfaYonlendir(int secimNo)
            {

                switch (secimNo)
                {
                    case 1:
                        ParaCek();
                        break;
                    case 2:
                        ParaYatir();
                        break;
                    case 3:
                        ParaGonder();
                        break;
                    case 4:
                        HesapBilgisiGoster(sec.hesapNumarasi);
                        break;

                    default:
                        break;
                }
            }

            public void HesapBilgisiGoster(int hesapNo)
            {
                Console.Clear();
                LoadingAnimation(10, "Hesap bilgileri ekranına yönlendiriliyorsunuz");
                foreach (var item in db.hesaps)
                {
                    if (item.hesapNo == hesapNo)
                    {
                        Console.WriteLine("Hesap Numaranız");
                        Console.WriteLine(item.hesapNo);
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("Hesap Parolanız");
                        Console.WriteLine(item.parola.ToString());
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("IBAN Numaranız");
                        Console.WriteLine(item.ibanNo);
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("Güncel Bakiyeniz");
                        Console.WriteLine(item.bakiye.ToString() + " TL");
                        Console.WriteLine("--------------------------------------\n");
                        Console.WriteLine("\t\t\t çıkmak için herhangi bir tuşa basın.");
                        Console.ReadKey();
                        LoadingAnimation(15, "menüye dönüyorsunuz.");
                        menuGoster();

                    }
                }
            }
            public void ParaCek()
            {
                //var hsp = db.hesaps.Where(hesap=>hesap.hesapNo == sec.hesapNumarasi).ToList();

                foreach (var item in db.hesaps)
                {
                    if (item.hesapNo == sec.hesapNumarasi)
                    {
                        Console.WriteLine("\t\t\tPARA ÇEKME");
                        Console.WriteLine("------------------------------------------------------------------------\n");
                        Console.WriteLine("mevcut bakiyeniz :" + item.bakiye);
                        Console.WriteLine("------------------------------------------------------------------------");
                        Console.WriteLine("çekmek istediğiniz miktarı giriniz:");
                        decimal cekilecekMiktar = Convert.ToDecimal(Console.ReadLine());
                        if (item.bakiye > cekilecekMiktar)
                        {
                            LoadingAnimation(10, "İşlem sürüyor");
                            item.bakiye = item.bakiye - cekilecekMiktar;
                            Console.WriteLine("işlem başarılı.");
                            Console.WriteLine("güncel bakiyeniz: " + item.bakiye.ToString());
                            Console.WriteLine("çıkış yapmak için herhangi bir tuşa basın.");
                            Console.ReadKey();
                            menuGoster();
                            break;
                        }

                    }
                }
            }
            public void ParaYatir()
            {
                foreach (var item in db.hesaps)
                {
                    if (item.hesapNo == sec.hesapNumarasi)
                    {
                        Console.WriteLine("\t\t\tPARA ÇEKME");
                        Console.WriteLine("------------------------------------------------------------------------\n");
                        Console.WriteLine("mevcut bakiyeniz :" + item.bakiye);
                        Console.WriteLine("------------------------------------------------------------------------");
                        Console.WriteLine("çekmek istediğiniz miktarı giriniz:");
                        decimal yatırılacakMiktar = Convert.ToDecimal(Console.ReadLine());
                        LoadingAnimation(10, "İşlem sürüyor");
                        item.bakiye = item.bakiye + yatırılacakMiktar;
                        Console.WriteLine("işlem başarılı.");
                        Console.WriteLine("güncel bakiyeniz: " + item.bakiye.ToString());
                        Console.WriteLine("çıkış yapmak için herhangi bir tuşa basın.");
                        Console.ReadKey();
                        menuGoster();
                        break;
                    }
                }
            }
            public void ParaGonder()
            {
                foreach (var item in db.hesaps)
                {
                    if (item.hesapNo == sec.hesapNumarasi)
                    {
                        Console.WriteLine("\t\t\tPARA GONDERİMİ");
                        Console.WriteLine("------------------------------------------------------------------------\n");
                        Console.WriteLine("gönderilecek hesabın IBAN numarasını giriniz:");
                        int aliciIban = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("mevcut bakiyeniz :" + item.bakiye);
                        Console.WriteLine("------------------------------------------------------------------------");
                        Console.WriteLine(aliciIban + " hesabına gondermek istediğiniz miktarı giriniz:");
                        decimal gonderilenMiktar = Convert.ToDecimal(Console.ReadLine());
                        if (item.bakiye > gonderilenMiktar)
                        {
                            LoadingAnimation(10, "İşlem sürüyor");
                            foreach (var alici in db.hesaps)
                            {
                                if (alici.ibanNo == aliciIban)
                                {
                                    alici.bakiye = alici.bakiye + gonderilenMiktar;
                                }
                            }
                            item.bakiye = item.bakiye - gonderilenMiktar;
                            LoadingAnimation(10, "gönderiliyor.");
                            Console.WriteLine("işlem başarılı.");
                            Console.WriteLine("güncel bakiyeniz: " + item.bakiye.ToString());
                            Console.WriteLine("çıkış yapmak için herhangi bir tuşa basın.");
                            Console.ReadKey();
                            menuGoster();
                            break;
                        }

                    }
                }
            }
        }
    }
}


