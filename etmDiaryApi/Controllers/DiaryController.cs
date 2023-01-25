using etmDiaryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace etmDiaryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        ApplicationDbContext db;

        public DiaryController(ApplicationDbContext db)
        {
            this.db = db;
        }


        //Запрос на получение Список задач на диаграмму
        //Параметры - дата начала, дата конец(интервал отображения), подразделение, сотрудник, состояние задачи
        //Формат даты - 12/01/2023 - но не обязательно, парсит и другие
        [HttpGet("ListTasks")]
        public async Task<IEnumerable> GetLsitTasks(string date1="01/01/2023", string date2="30/01/2023", string department= "all", int userCode = 0 , string condition="all")
        {
            var d1 = DateTime.Parse(date1);
            var d2 = DateTime.Parse(date2);

            List<Models.Task> list = null;

            if (userCode != 0)
            {
                list = await db.Tasks
                .Where(p => (p.Start <= d2 && p.End >= d1) && (p.User.Code == userCode))
                .Include(u => u.User)
                .Include(u => u.Partner)
                .Include(u => u.Condition)
                .Take(20).ToListAsync();
            }
            else if (department != "all")
            {
                list = await db.Tasks
                .Where(p => (p.Start <= d2 && p.End >= d1) && (p.User.Department.Name == department))
                .Include(u => u.User)
                .Include(u => u.Partner)
                .Include(u => u.Condition)
                .Take(20).ToListAsync();
            }
            else
            {
                list = await db.Tasks
                .Where(p => p.Start <= d2 && p.End >= d1)
                .Include(u=>u.User)
                .Include(u=>u.Partner)
                .Include(u => u.Condition)
                .Take(20).ToListAsync();
            }

            if (condition != "all")
            {
                list = list.Where(p => p.Condition.Name == condition).ToList();
            }
            
            return list.Select(j => new
            {
                id = j.Id,
                dateStart = j.Start.ToString("d"),
                dateEnd = j.End.ToString("d"),
                pertner = j.Partner.Name,
                user = j.User.UserName,
                userCode = j.User.Code,
                priority = j.Priority,
                description = j.Description
            }).ToArray();
            //return x;
        }

        //Запрос на Получение задачи

        //Запрос на Получение списка пользователей

        //Запрос на Получение списка партнеров

        //Запрос на Получение списка тем задач

        //Запрос на Получение состояний задач

        //Получение задачи. Сохранение ее в бд

        //Запрос на получение канбан доски с задачами

        //Получение канбан доску и сохранение в бд

        //Запрос для получения отчета

        //Получение избранных задач

        //Добавление избранную задачу таск

        //Добавление избранную задачу стик

        //запрос на получение комментариев

        //получение и сохранение комментариев
    }
}
