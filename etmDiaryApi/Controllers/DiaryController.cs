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
        [HttpGet("ListThemes")]
        public async Task<IEnumerable> GetListThemes()
        {
            return await db.Themes.Select(j => new
            {
                id = j.Id,
                Name = j.Name
            }).ToListAsync();
        }

        //Запрос на Получение состояний задач
        [HttpGet("ListConditions")]
        public async Task<IEnumerable> GetListConditions()
        {
            return await db.Conditions.Select(j => new
            {
                id = j.Id,
                Name = j.Name
            }).ToListAsync();
        }

        //запрос на получение истории
        [HttpGet("HistoryTask")]
        public async Task<IEnumerable> GetHistoryTask(int idTask)
        {
            var task = await db.Tasks.FindAsync(idTask);
            if(task.History!=null)
                return task.History.Split("|").ToList();
            return null;
        }

        //Получение задачи. Сохранение ее в бд
        //Проблеммы с разными форматами даты. Отправляем в одном формате, принимаем в другом
        //Возможно лучьше поменять дазделение чисел в дате на точки
        [HttpPost("SaveTask")]
        public async Task<IActionResult> PostSaveTask(TaskTransfer request)
        {
            if(request.Id == null)
            {
                Models.Task newTask = new Models.Task
                {
                    Start = request.Start,
                    End = request.End,
                    Priority = request.Priority,
                    Description = request.Description,
                    ThemeId = request.ThemeId,
                    PartnerId = request.PartnerId,
                    UserId = request.UserId,
                    ConditionId = request.ConditionId
                };
                db.Tasks.Add(newTask);
                int result = await db.SaveChangesAsync();
                return Ok(result);
            }
            else
            {
                var task = db.Tasks.Find(request.Id);
                task.End = request.End;
                task.Priority = request.Priority;
                task.Description = request.Description;
                task.ThemeId = request.ThemeId;
                task.PartnerId = request.PartnerId;
                task.UserId = request.UserId;
                task.ConditionId = request.ConditionId;
                task.Result = request.Result;
                int result = await db.SaveChangesAsync();
                return Ok(result);
            }

            return NotFound();
        }

        //Добавление в историю
        [HttpGet("AddHistory")]
        public async Task<IActionResult> GetAddHistory(int idTask, string line)
        {
            var task = await db.Tasks.FindAsync(idTask);
            var date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            if (task.History != null)
            {
                string text = task.History + "|"+ date + " - " + line;
                task.History = text;
            }
            else
            {
                task.History = date + " - " + line;
            }
            int result = await db.SaveChangesAsync();
            return Ok(result);

        }

        //Запрос на получение канбан доски с задачами
        [HttpGet("CanbanTask")]
        public async Task<IEnumerable> GetCanbanSample(int idTask)
        {
            var canban = await db.Tasks
                .Join(db.Boards, p => p.BoardId, c => c.Id, (p, c) => new
                {
                    TaskId = p.Id,
                    Sample = c.Sample
                }).Where(u => u.TaskId == idTask)
                .FirstOrDefaultAsync();
            var canbanSample = canban.Sample.Split('|');//нужно распарсить

            return canbanSample.ToList();
        }

        //Отправка доску задач на Express
        [HttpGet("SticksOnBoardTask")]
        public async Task<IEnumerable> GetSticksOnBoardTask(int idTask)
        {
            var sticks = await db.Sticks
                .Where(u => u.TaskId == idTask)
                .Join(db.Tasks, p => p.TaskId, c => c.Id, (p, c) => new
                {
                    Id = p.Id,
                    Description = p.Description,
                    NumStatus = p.NumStatus
                }).ToArrayAsync();
            return sticks;
        }

        //Запрос на получение достки с экспресс-задачами
        [HttpGet("CanbanExpress")]
        public async Task<IEnumerable> GetCanbanRxpress()
        {
            var canban = await db.Boards
                .FindAsync(1);
            var canbanSample = canban.Sample.Split('|');//нужно распарсить

            return canbanSample.ToList();
        }
        //Отправка стиков на Express ---------- добавить дату можно
        [HttpGet("SticksOnExpress")]
        public async Task<IEnumerable> GetSticksOnExpress(int userCode)
        {
            var sticks = await db.Users
                .Where(u => u.Code == userCode)
                .Join(db.Sticks, p => p.Id, c => c.UserId, (p, c) => new
                {
                    Id = c.Id,
                    Description = c.Description,
                    NumStatus = c.NumStatus,
                    TaskId = c.TaskId
                })
                .Where(u => u.TaskId == null)
                .ToArrayAsync();
            return sticks;
        }

        //Получение шаблона канбан доску и сохранение в бд с добавлением пользователей
        [HttpPost("AddSampleAndSticks")]
        public async Task<IActionResult> PostAddSampleAndSticks(PostTaskWithBoard post)
        {
            Board board = new Board { Sample = post.Sample };
            db.Boards.Add(board);
            var listUsers = await db.Users.Where(u => post.usersCode.Contains(u.Code)).ToListAsync();
            List<Models.Task> listTasks = new List<Models.Task>();
            foreach(var user in listUsers)
            {
                Models.Task newTask = new Models.Task
                {
                    Start = post.Start,
                    End = post.End,
                    Priority = 1,
                    Description = post.Descreption,
                    User = user,
                    ConditionId = 1,
                    ThemeId = 3
                };
                listTasks.Add(newTask);
                db.Tasks.Add(newTask);
            }
            var staus = post.Sample.Split('|')[0];

            foreach(var task in listTasks)
            {
                var sticksForUser = post.descriptions.Select(u => new Stick
                {
                    Description = u,
                    Date = DateTime.Now,
                    NameStatus = staus,
                    NumStatus = 0,
                    Task = task,
                    User = task.User,
                    Board = board
                }).ToList();

                db.Sticks.AddRange(sticksForUser);
            }

            var result = db.SaveChanges();
            return Ok(result);
        }

        //Создание стика 

        //Изменение стика

        //Запрос для получения отчета

        //Получение избранных задач

        //Добавление избранную задачу таск

        //Добавление избранную задачу стик

        //получение и сохранение комментариев???
    }
}
