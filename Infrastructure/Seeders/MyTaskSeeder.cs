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
                Name = "Помилка"
            };
            var state2 = new MyTaskState()
            {
                Id = TaskStates.Wait,
                Name = "Очікування"
            };
            var state3 = new MyTaskState()
            {
                Id = TaskStates.Process,
                Name = "Виконується"
            };
            var state4 = new MyTaskState()
            {
                Id = TaskStates.Completed,
                Name = "Виконалася"
            };
            var state5 = new MyTaskState()
            {
                Id = TaskStates.Stopped,
                Name = "Призупинено"
            };


            modelBuilder.HasData(state1,
                                 state2,
                                 state3,
                                 state4,
                                 state5);
        }
    }
}
