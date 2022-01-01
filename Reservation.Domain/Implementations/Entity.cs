using System;
using System.ComponentModel.DataAnnotations;
using Reservation.Domain.Interfaces;

namespace Reservation.Domain.Implementations
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = Comb.Create();
        }

        [Key] public Guid Id { get; set; }
    }
}