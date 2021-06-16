using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeTelegramBot.Domain
{
    public abstract class Entity<TId>
    {
        public virtual TId id { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(TId id)
        {
            this.id = id;
        }

/*        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TId> other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (ValueObject.GetUnproxiedType(this) != ValueObject.GetUnproxiedType(other))
                return false;

            if (Id.Equals(default(TId)) || other.Id.Equals(default(TId)))
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (ValueObject.GetUnproxiedType(this).ToString() + Id).GetHashCode();
        }*/
    }

    public abstract class Entity : Entity<int>
    {
        protected Entity()
        {
        }

        protected Entity(int id)
            : base(id)
        {
        }
    }
}
