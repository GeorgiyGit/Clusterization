using Domain.Entities.Tasks;
using Domain.Resources.Types;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    internal class MyTaskSeeder
    {
        public static void StateSeeder(EntityTypeBuilder<MyTaskState> modelBuilder)
        {
            var state1 = new MyTaskState()
            {
                Id = TaskStates.Error,
                Name = "Error"//"Помилка"
            };
            var state2 = new MyTaskState()
            {
                Id = TaskStates.Wait,
                Name = "Wait"//"Очікування"
            };
            var state3 = new MyTaskState()
            {
                Id = TaskStates.Process,
                Name = "Process"//"Виконується"
            };
            var state4 = new MyTaskState()
            {
                Id = TaskStates.Completed,
                Name = "Completed"//"Виконалася"
            };
            var state5 = new MyTaskState()
            {
                Id = TaskStates.Stopped,
                Name = "Stopped"//"Призупинено"
            };


            modelBuilder.HasData(state1,
                                 state2,
                                 state3,
                                 state4,
                                 state5);
        }
    }
}
