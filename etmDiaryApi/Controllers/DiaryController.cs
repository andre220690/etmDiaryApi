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
        private readonly ILogger _logger;

        public DiaryController(ApplicationDbContext db, ILogger<DiaryController> logger)
        {
            this.db = db;
            _logger = logger;
        }


        //Запрос на получение Список задач на диаграмму
        //Параметры - дата начала, дата конец(интервал отображения), подразделение, сотрудник, состояние задачи
        //Формат даты - 12/01/2023 - но не обязательно, парсит и другие
        [HttpGet("ListTasks")]
        public async Task<IEnumerable> GetLsitTasks(string date1, string date2, string department= "all", int userCode = 0 , string condition="all")
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
        }

        //Запрос на Получение задачи
        [HttpGet("Task")]
        public async Task<Object> GetTask(int TaskId)
        {
            var result = await db.Tasks
                .Include(u => u.Theme)
                .Include(u => u.User)
                .Include(u => u.Partner)
                .Include(u => u.Condition)
                .FirstOrDefaultAsync(i => i.Id == TaskId);

            return new
            {
                Id = result.Id,
                Start = result.Start.ToString("d"),
                End = result.End.ToString("d"),
                Priority = result.Priority,
                Theme = result.Theme.Name,
                Partner = result.Partner.Name,
                User = result.User.UserName,
                Description = result.Description,
                Condition = result.Condition.Name,
                Result = result.Result
            };
        }

        //Запрос на Получение списка пользователей
        [HttpGet("ListUsers")]
        public async Task<IEnumerable> GetListUsers(string line)
        {
            var result = await db.Users.Where(u => u.UserName.Contains(line)).ToListAsync();
            return result.Select(j => new
            {
                UserName = j.UserName,
                Code = j.Code
            });
        }

        //Запрос на Получение списка партнеров
        [HttpGet("ListParters")]
        public async Task<IEnumerable> GetListParters(string line)
        {
            var result = await db.Partners.Where(u => u.Name.Contains(line)).ToListAsync();
            return result.Select(j => new
            {
                UserName = j.Name,
                id = j.Id
            });
        }


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
