﻿namespace etmDiaryApi.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User>? Users { get; set; }
    }
}