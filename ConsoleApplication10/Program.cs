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

            string domain = "science_technology";  //tv_nauka,  science_technology
            string Mydomain = "MyTest6";
            ulong count = 50;

            var api = new VkApi();

            List<string> Link = new List<string>();   // листинг с ссылками на статьи
            List<string> TEXT = new List<string>();   // листинг с текстом  Статей
            List<string> MyLink = new List<string>();   // листинг с ссылками на мои статьи
            List<string> MyTEXT = new List<string>();   // листинг с текстом моих Статей
            try
            {
                RequestVkApi request = new RequestVkApi();
                Text text = new Text();

                request.authorize(out api);  /// Авторизация 
                request.getWall(ref api, ref Link, ref TEXT, domain, count); // Запрос постов со стены
                request.getWall(ref api, ref MyLink, ref MyTEXT, Mydomain, count); // Запрос постов со своей стены



                text.checkArticle(ref Link, ref MyLink); // проверяем, есть ли статья уже в нашей группе
                text.parsing(ref Link, ref TEXT); // Извлечение текста статьи для анализа
                text.delServicePartOfSpeech(ref TEXT); // Извлечение текста статьи для анализа


                //   request.postWall(ref api, ref Link); // Публикация ссылки на статью в группе



                // Вывод информации о работе
                if (Link.Count != 0) {

                    Console.WriteLine("Успешное завершение получения статей. Добавленое " + Link.Count + " Статей");
                }

                else
                {
                    Console.WriteLine("Новые статьи отсутствуют");
                }



                   }

                   catch { Console.WriteLine("Получение статей не удалось - ошибка"); }




                Console.ReadKey();












            }











    }
    }

    
