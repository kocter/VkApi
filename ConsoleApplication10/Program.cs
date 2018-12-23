using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace ConsoleApplication10
{
    class Program
    {
        static void Main(string[] args)
        {

            //Данные авторизации
            FileStream fin;
            string login,password;
            fin = new FileStream("D:/Authorize/login.txt", FileMode.Open);
            StreamReader fstr_in = new StreamReader(fin);
            login = fstr_in.ReadLine();
            fstr_in.Close();

            fin = new FileStream("D:/Authorize/password.txt", FileMode.Open);
            fstr_in = new StreamReader(fin);
            password = fstr_in.ReadLine();

            //


            //Авторизация
            var api = new VkApi();


            Console.WriteLine("Пожалуйста подождите, идёт авторизация...");
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 3503091,
                Login = login,
                Password = password,
                Settings = Settings.Wall
            });
            Console.WriteLine(api.Token);  

            Console.WriteLine("Авторизация прошла успешно!");


// подключаемся с помощью апи вк к группе и получаем посты
            var news = api.Wall.Get(new VkNet.Model.RequestParams.WallGetParams
            {

               
                Domain = "science_technology",
                Count = 30,
                Fields = ProfileFields.All
            });


            

            //обявляем переменные для перебора потсов и статей
            int i = 0, j=0;         

            List<string> Link = new List<string>();   // листинг с ссылками на статьи
            List<string> TEXT = new List<string>();   // листинг с текстом  Статей

            foreach (var newwwww in news.WallPosts)   // вычисляем среди всех постов статьи и добавляем их в листинг
            {

             if(news.WallPosts[i].Attachment.Type.ToString() == "VkNet.Model.Attachments.Link")
                {
                    Link.Add(news.WallPosts[i].Attachment.Instance.ToString());
                }



                i++;
              
            }
            // news.WallPosts[i].Attachment.Type   дает определить тип статья это link
            // news.WallPosts[i].Attachment.Instance   с помощю этого можно получить ссылку на статью



            // выводим все сслыки на статьи 
            foreach (var link in Link)
            {


                Console.WriteLine(Link[j]);
                j++;
            }


            j = 0;


            string text;    

            foreach (var link in Link)
            {


                text = null;

                //Парсер для вывода текста


                string main_url;
                HtmlWeb webDoc;
                HtmlAgilityPack.HtmlDocument doc;


                main_url = Link[j];
                webDoc = new HtmlWeb();
                webDoc.OverrideEncoding = Encoding.Default;
                doc = webDoc.Load(main_url);

                HtmlNode Title = doc.DocumentNode.SelectSingleNode("//h1"); // Заголовок
           

                HtmlNodeCollection par = doc.DocumentNode.SelectNodes("//p"); // Остальной текст


                text = Title.InnerText + "\n";




                foreach (var tag in par)
                {

                    string a;
                    a = tag.InnerText;



                    text = text + a +"\n";
                }
               
                TEXT.Add(text);


                j++;
            }





            Console.WriteLine(TEXT[2]);




            Console.ReadKey();

       










        }











    }
}
    
