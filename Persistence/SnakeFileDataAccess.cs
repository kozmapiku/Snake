using Snake.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Snake.Persistence
{
    class SnakeFileDataAccess : ISnakeDataAccess
    {
        public async Task<SnakeTable> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    String line = await reader.ReadLineAsync();
                    String[] numbers = line.Split(' '); // beolvasunk egy sort, és a szóköz mentén széttöredezzük
                    Int32 tableSize = Int32.Parse(numbers[0]); // beolvassuk a tábla méretét
                    SnakeTable table = new SnakeTable(tableSize); // létrehozzuk a táblát

                    for (Int32 i = 0; i < tableSize; i++)
                    {
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');

                        for (Int32 j = 0; j < tableSize; j++)
                        {
                            table.SetValue(i, j, (TableObject)Int32.Parse(numbers[j]));
                        }
                    }

                    return table;
                }
            }
            catch
            {
                throw new SnakeDataException();
            }
        }
        public async Task SaveAsync(String path, SnakeTable table)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
                {
                    await writer.WriteLineAsync(""+table.Size); // kiírjuk az adatokat
                    for (Int32 i = 0; i < table.Size; i++)
                    {
                        for (Int32 j = 0; j < table.Size; j++)
                        {
                            await writer.WriteAsync(table[i, j] + " "); // kiírjuk az értékeket
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch
            {
                throw new SnakeDataException();
            }
        }
    }
}

