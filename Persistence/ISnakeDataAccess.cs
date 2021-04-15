using Snake.Model;
using System;
using System.Threading.Tasks;

namespace Snake.Persistence
{
    public interface ISnakeDataAccess
    {
        Task<SnakeTable> LoadAsync(String path);

        Task SaveAsync(String path, SnakeTable table);
    }
}
