using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Moq;
using System.Data.SqlClient;

namespace WebformsCms.Tests
{
    [TestClass]
    public class MenusRepositoryTests
    {
        Data.DataSession session;


        [TestInitialize]
        public void Init()
        {
        }

        [TestCleanup]
        public void Kill()
        {
            session.UnitOfWork.Rollback();
        }

        [TestMethod]
        public void TestConstructor()
        {
            session = new Data.DataSession(true);

            var uow = session.UnitOfWork;
            uow.Begin();

            var menusRepo = new Data.MenusRepository(uow);

            var menu = new Domain.Menus()
            { 
                Name = "Test"    ,
                CreatedAt = DateTime.Now,
                CreatedById = 1            
            };

            menu.Id = menusRepo.Save(menu);

            var actual = menusRepo.GetSingle(menu.Id);
           
            Assert.AreEqual(menu.Name, actual.Name);
        }

        [TestMethod]
        public void GetAllParents_NoFound()
        {
            session = new Data.DataSession(true);

            var uow = session.UnitOfWork;
            uow.Begin(IsolationLevel.ReadUncommitted);

            var menusRepo = new Data.MenusRepository(uow);

            var menu = new Domain.Menus()
            {
                Name = "Test",
                CreatedAt = DateTime.Now,
                CreatedById = 1
            };
             
            menu.Id = menusRepo.Save(menu);

            var parents = menusRepo.GetAllParentMenus();

            Assert.AreEqual(1, parents.Count);
        }
    }
}
