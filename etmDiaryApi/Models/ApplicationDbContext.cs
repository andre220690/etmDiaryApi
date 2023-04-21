using Microsoft.EntityFrameworkCore;

namespace etmDiaryApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Stick> Sticks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavoritSticks> FavoritSticks { get; set; }
        public DbSet<FavoritTasks> FavoritTasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
            //Здесь заполнение БД
            if (Database.EnsureCreated())
            {
                AddData();
            }
        }

        public void AddData()
        {
                        Database.EnsureDeleted();
            Database.EnsureCreated();

            Department op1 = new Department { Name = "ОП-1 Екатеринбург" };
            Department op2 = new Department { Name = "ОП-2 Нижневартовск" };

            User user1 = new User { UserName = "Попов В.В.", isSupervisor = false, Department = op1, Login="popov_vv", Code = 111111 };
            User user2 = new User { UserName = "Баранов А.А.", isSupervisor = false, Department = op1, Login = "baranov_vv", Code = 222222 };
            User user3 = new User { UserName = "Козлов Н.Н", isSupervisor = true, Department = op1, Login = "kozlov_nn", Code = 333333 };
            User user4 = new User { UserName = "Васильев К.К", isSupervisor= false, Department = op2, Login = "vasilev_kk", Code = 444444 };
            User user5 = new User { UserName = "Петров Д.Д.", isSupervisor = false, Department = op2, Login = "petrov_dd", Code = 555555 };

            Partner part1 = new Partner { Name = "ООО Восход" };
            Partner part2 = new Partner { Name = "ИП Пупкин Г.Н" };
            Partner part3 = new Partner { Name = "ООО Электромкомплект" };

            Theme theme1 = new Theme { Name = "Освоение потенциала клиента" };
            Theme theme2 = new Theme { Name = "Работа с ДЗ" };
            Theme theme3 = new Theme { Name = "Работа с Доской" };


            Condition cond1 = new Condition { Name = "Выполнено. Успех" };
            Condition cond2 = new Condition { Name = "Выполнено. Неудачно" };
            Condition cond3 = new Condition { Name = "Назначено" };
            Condition cond4 = new Condition { Name = "Просрочено" };

            Board board1 = new Board { Sample = "Сделать|В процессе|Выполнено" };
            Board board2 = new Board { Sample = "Сделать|Отправить предложение|Получена ОС|Выполнено" };

            Task task1 = new Task { Start = new DateTime(2023, 1, 1)
                , End = new DateTime(2023,1, 31)
                , Board=board2
                , Condition = cond3
                , Priority = 1
                , Theme = theme1
                , Partner = part1
                , User = user1
                , History = "1/12/2022 14:54 - Сделал что-то|21/1/2023 09:12 - Пошел на обед|22/2/2023 11:11 - Сделал дела. Сделал дела, сделал дела - сделал дела"
                , Description = "Заведение новой продукции в ассортимент клиента"};
            Task task2 = new Task { Start = new DateTime(2022, 12, 15)
                , End = new DateTime(2023, 2, 16)
                , Board=board1
                , Condition = cond3
                , Priority = 2
                , Theme = theme2
                , Partner = part2
                , User = user1
                , Description = "Отгрузить клиента"};
            Task task3 = new Task { Start = new DateTime(2023, 1, 16)
                , End = new DateTime(2023,2, 28)
                , Board=board1
                , Condition = cond3
                , Priority = 1
                , Theme = theme1
                , Partner = part3
                , User = user2
                , Description = "Получить документацию по проекту"};
            Task task4 = new Task { Start = new DateTime(2023, 1, 31)
                , End = new DateTime(2023,2, 10)
                , Board=board1
                , Condition = cond3
                , Priority = 3
                , Theme = theme2
                , Partner = part2
                , User = user4
                , Description = "Договорится об опате задолженности"};

            Stick stick1 = new Stick { User = user1
                , Board = board2
                , Date = new DateTime(2023, 1, 15)
                , Description = "Предложение по арматуре"
                , NameStatus = "Отправить предложение"
                , NumStatus = 1
                , Task = task1};
            Stick stick2 = new Stick { User = user1
                , Board = board2
                , Date = new DateTime(2023, 1, 19)
                , Description = "Предложение по кабелю"
                , NumStatus = 0
                , NameStatus = "Сделать"
                , Task = task1};
            Stick stick3 = new Stick { User = user4
                , Board = board1
                , Date = new DateTime(2023, 1, 31)
                , Description = "Передать претензию клиенту"
                , NameStatus = "Выполнено"
                , NumStatus = 2
                , Task = task4};
            Stick stick4 = new Stick { User = user1
                , Board = board1
                , Date = new DateTime(2023, 1, 15)
                , Description = "Составить план развития"
                , NameStatus = "Сделать"
                , NumStatus = 0};
            Stick stick5 = new Stick { User = user1
                , Board = board1
                , Date = new DateTime(2023, 1, 15)
                , Description = "Заполнить отчет"
                , NameStatus = "В процессе"
                , NumStatus = 1};

            FavoritSticks fs1 = new FavoritSticks { User = user1, Stick = stick1 };
            FavoritSticks fs2 = new FavoritSticks { User = user3, Stick = stick2 };
            FavoritSticks fs3 = new FavoritSticks { User = user3, Stick = stick4 };

            FavoritTasks ft1 = new FavoritTasks { User = user1, Task = task1 };
            FavoritTasks ft2 = new FavoritTasks { User = user3, Task = task3 };

            Departments.AddRange(op1, op2);
            Users.AddRange(user1, user2, user3, user4, user5);
            Partners.AddRange(part1, part2, part3);
            Themes.AddRange(theme1, theme2, theme3);
            Conditions.AddRange(cond1, cond2, cond3, cond4);
            Boards.AddRange(board1, board2);

            SaveChanges();

            Tasks.AddRange(task1, task2, task3, task4);
            Sticks.AddRange(stick1, stick2, stick3, stick4, stick5);

            SaveChanges();

            FavoritSticks.AddRange(fs1, fs2, fs3);
            FavoritTasks.AddRange(ft1, ft2);

            SaveChanges();
            Console.WriteLine("Данные внесены");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseMySql("server=194.58.109.195;port=3306;user=root;password=123456ai;database=diary;",
            //    new MySqlServerVersion(new Version(8, 0, 25)));
        }

    }
}
