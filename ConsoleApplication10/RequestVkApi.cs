using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace ConsoleApplication10
{
    class RequestVkApi
    {

       


        //Класс общения с VK api
        public void authorize(out VkApi api)
        {


            //Данные авторизации
            FileStream fin;
            string login, password;
            fin = new FileStream("D:/Authorize/login.txt", FileMode.Open);
            StreamReader fstr_in = new StreamReader(fin);
            login = fstr_in.ReadLine();
            fstr_in.Close();

            fin = new FileStream("D:/Authorize/password.txt", FileMode.Open);
            fstr_in = new StreamReader(fin);
            password = fstr_in.ReadLine();

            //


            //Авторизация
             api = new VkApi();

            Console.WriteLine("Пожалуйста подождите, идёт авторизация...");
            try { 
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 6805045,    // Сменные 3503091;6795487,6805045,6809748
                Login = login,
                Password = password,
                Settings = Settings.Wall
                
            });


           //     Console.WriteLine(api.Token);

                Console.WriteLine("Авторизация прошла успешно!");


            }

            catch
            {
                
                Console.WriteLine("Авторизация не удалась!");
            }



        }


        public void getWall(ref VkApi api, ref List<string> Link, ref List<string> TEXT,string  domain, ulong count)
        {


            // подключаемся с помощью апи вк к группе и получаем посты
            try { 
            var news = api.Wall.Get(new VkNet.Model.RequestParams.WallGetParams
            {


                Domain = domain,
                Count = count,
                Fields = ProfileFields.All
            });




                //обявляем переменные для перебора потсов и статей
                int i = 0;


            foreach (var newwwww in news.WallPosts)   // вычисляем среди всех постов статьи и добавляем их в листинг
            {
                try
                {
                    if (news.WallPosts[i].Attachment.Type.ToString() == "VkNet.Model.Attachments.Link")
                    {
                        Link.Add(news.WallPosts[i].Attachment.Instance.ToString());
                    }
                }
                catch { }


                i++;

            }
            // news.WallPosts[i].Attachment.Type   Содержит тип статьи
            // news.WallPosts[i].Attachment.Instance   Содержит ссылку на статью



            // выводим все сслыки на статьи 
       /*     foreach (var link in Link)
            {


                Console.WriteLine(link);
                
            }

        */   




            }


            catch { }



            }





        public void postWall(ref VkApi api, ref List<string> Link)
        {







            foreach(var link in Link) {

                var result = api.Wall.Post(new WallPostParams
                {
                    FromGroup=true,
                    OwnerId = -176088647,
                    Signed = false,
                Attachments = new List<MediaAttachment>
                {

                    

                    new StringLink()
                    {

                        Link =  link
                    }
                }
            });


            }


        }






    }






}
