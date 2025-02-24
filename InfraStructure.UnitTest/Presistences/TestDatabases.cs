using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Infraction.Presistences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InfraStructure.UnitTest.Presistences
{
    public class TestDatabases
    {
        [Fact]
        public void Constructer_CreateInMeMory_success()
        {
           

            var context =  DbConTextFctory.Create();

            Assert.True(context.Database.EnsureCreated());// có tạo được db k  , phần này có thể dùng nhiều lần 
        }
    }
}
