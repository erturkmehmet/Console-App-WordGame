using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
/*
 * 
 * */

namespace KelimeOyunu
{
    class Program
    {

        static void Main(string[] args)
        {
            //ADO.NET MİMARİSİ
            SqlConnection Conn = new SqlConnection(@"Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            //Databasede verilerimizin boşluktan sonraki kısmı almıyoruz yani birinci kelimeleri alıyoruz
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select Distinct case when CHARINDEX(' ',ProductName)>0 Then LEFT(ProductName, CHARINDEX(' ', ProductName) - 1) else ProductName END FROM PRODUCTS", Conn);

            DataTable dt = new DataTable();

            //Veritabanımızdaki verileri DataAdaptöre dolduruyoruz.
            dataAdapter.Fill(dt);
            while (true)
            {
                //İlk while programın sonsuz döngüye girerek sürekli devam etmesini sağlıyor
                Console.Write("Harf giriniz: ");
                string valueReceived = Console.ReadLine().ToUpper();

                bool condition = true;
                //sayacı arttırarak dataTable 'daki row 'ları gezebiliyoruz.
                int counter = 0;
                //Seçilen harften kaç tane veri(product)miz olduğunu tuttuğumuz değişken  
                int number = 0;
                string value;

                while (condition)
                {
                    //Sırayla o rowdaki değeri value 'a atıyoruz.
                    value = dt.Rows[counter][0].ToString().ToUpper(); ;

                    //Kullanıcıdan alınan harfin dataTable 'daki verimizin ilk harfiyle aynı olup olmadığını kontrol ediyoruz.
                    if (value.Substring(0, 1) == valueReceived)
                    {
                        Console.WriteLine(value);
                        //Aynı kelimeyi tekrar görmek istemediğimiz için kullandığımız değere boş karakter atıyoruz.
                        dt.Rows[counter][0] = " ";
                        //Keliemizin son harfi yeni kelimemizin ilk harfi olacak.
                        valueReceived = value[value.Length - 1].ToString();
                        //Counter 'ı if 'den sonra bir artıracağımız için '-1' atadık. 
                        counter = -1;
                        number++;
                    }
                    //Sayac son row 'a geldi mi? 
                    if (counter == dt.Rows.Count - 1)
                    {
                        //yeni veri kalmadığı için döngüden çıkıyoruz.
                        condition = false;
                        Console.WriteLine(number + " tane product bulundu");
                    }

                    counter++;
                };
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}