using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Shared.Abstract;
using ToDo.Models;

namespace ToDo.Business.Abstract
{
    public interface ITodoService:IService<Todo>
    {
        IQueryable<Todo> GetAll(int userId);
        bool RemoveTag(int todoId, int tagId);

        Todo Add(Todo todo, int[] tagsIds, int userId);
    }
}
