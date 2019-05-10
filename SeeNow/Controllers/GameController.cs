using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SeeNow.Models;

namespace SeeNow.Controllers
{
    public class GameController : Controller
    {
        private SeeNowEntities db = new SeeNowEntities();

        //HostStart get QuizzesIndex id parameter = item.group_name
        public ActionResult QuizzesIndex(string id)
        {
            var quiz = from m in db.quizzes
                       select m;
            if (!String.IsNullOrEmpty(id))
            {
                //從quizzes取出包含id的題目
                quiz = quiz.Where(s => s.quiz_group.ToString().Contains(id));
            }

            //JOIN 所選題目+4組答案
            var answer_text = (from qz in quiz
                               join qzas in db.quiz_answer on qz.quiz_guid equals qzas.quiz_guid
                               //select new Quiz4Answers { quizzes = qz, quiz_answer = qzas };
                               select new { quizzes = qz, quiz_answer = qzas }).ToList();
            //answer_text內容為title4組+ans4組
            //{"title1":"10+10","ans1":"24","title2":"100+100","ans2":"201","title3":"100+100","ans3":"203"}
            //利用Dictionary<string, object>產出(key,value)
            //strObj.Add會有重覆title key出現,產生錯誤時用catch默許錯誤,只做strObj.Add("ans"..
            //產出的List將只有留{"title":"10+10","ans1":"24","ans2":"201","ans3":"203"}
            int i = 1;
            Dictionary<string, object> strObj = new Dictionary<string, object>();
            List<Dictionary<string, object>> qzansList = new List<Dictionary<string, object>>();
            foreach (var qz in answer_text)
            {
                try
                {
                    strObj.Add("title", qz.quizzes.tittle_text);
                    strObj.Add("time", qz.quizzes.time);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

                strObj.Add("ans" + i, qz.quiz_answer.answer_text);
                i++;
                if (i > 4)
                {
                    qzansList.Add(strObj);
                    strObj = new Dictionary<string, object>();
                    i = 1;
                }
            }
            //(key,value)列表序列化Json格式{"title":"10+10","ans1":"24","ans2":"201","ans3":"203"}
            string jsonString = JsonConvert.SerializeObject(qzansList);
            ViewBag.qzans = jsonString;
            return View(qzansList);
        }

        public ActionResult PlayerStart()
        {
            return View();
        }

        // GET: quizzes
        public ActionResult Index()
        {
          return View();
        }

       
    }
}