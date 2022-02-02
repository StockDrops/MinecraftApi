using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Core.Api.Contracts.Models
{
    /// <summary>
    /// Defines a database entity.
    /// </summary>
    public interface IEntity : IEntity<long>
    {

    }
    /// <summary>
    /// 
    /// </summary>
    public interface IStringEntity : IEntity<string>
    {

    }
    /// <summary>
    /// Defines a database entity.
    /// </summary>
    public interface IEntity<T>
        
    {
        /// <summary>
        /// Id of the entity of <typeparamref name="T"/>
        /// </summary>
        public T Id { get; set; } // The id is a long so that it's almost impossible to run out of ids.
    }
}
