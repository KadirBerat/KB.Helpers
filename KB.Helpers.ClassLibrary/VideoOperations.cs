using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KB.Helpers.ClassLibrary
{
    class VideoOperations
    {
        public string ChangeIframeContent(string url)
        {
            string res = "";

            //var url = "<iframe width='1280' height='720' src='https://www.youtube.com/embed/QuV9iPaZTBU' frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' allowfullscreen ></iframe>";

            string[] splited = url.Split(' ');
            for (int i = 0; i < splited.Length; i++)
            {
                if (splited[i].Contains("<iframe"))
                    splited[i] = splited[i];
                else if (splited[i].Contains("width"))
                    splited[i] = "width='100%'";
                else if (splited[i].Contains("height"))
                    splited[i] = "height='100%'";
                else if (splited[i].Contains("src"))
                {
                    var getIdText = splited[i];
                    var id = getIdText.Replace("src='https://www.youtube.com/embed/", "").Replace("'", "");
                    splited[i] = "";
                    splited[i] = "src='" + "https://www.youtube.com/embed/" + $"{id}?&autoplay=1&enablejsapi=1&loop=1&rel=0&showinfo=0&color=white&iv_load_policy=3&playlist={id}&version=3&controls=0&mute=1'";
                }
                else if (splited[i].Contains("></iframe>"))
                    splited[i] = splited[i];
                else if (splited[i].Contains("frameborder"))
                {
                    splited[i] = splited[i];
                }
                else if (splited[i].Contains("allowfullscreen"))
                {
                    splited[i] = splited[i];
                }
                else
                {
                    splited[i] = "";
                }

                splited[i] = splited[i] + " ";
            }
            foreach (var item in splited)
            {
                res += item;
            }

            return res;
        }
    }
}
