﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuizDBEntities : DbContext
    {
        public QuizDBEntities()
            : base("name=QuizDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tblCorrectOption> tblCorrectOptions { get; set; }
        public DbSet<tblQuestionOption> tblQuestionOptions { get; set; }
        public DbSet<tblQuizQuestion> tblQuizQuestions { get; set; }
        public DbSet<tblQuiz> tblQuizs { get; set; }
        public DbSet<tblQuizCategory> tblQuizCategories { get; set; }
        public DbSet<tblUser> tblUsers { get; set; }
    }
}
