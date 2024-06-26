﻿using Domain.Entities.Tasks;
using Domain.Resources.Types;
using Domain.Resources.Types.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Seeders
{
    internal class MyTaskStateSeeder : IEntityTypeConfiguration<MyTaskState>
    {
        public void Configure(EntityTypeBuilder<MyTaskState> builder)
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


            builder.HasData(state1,
                            state2,
                            state3,
                            state4,
                            state5);
        }
    }
}
