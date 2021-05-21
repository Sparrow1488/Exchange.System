using ExchangeServer.SQLDataBase;
using ExchangeSystem.Requests.Objects.Entities;
using System.Linq;

namespace ExchangeServer.MVC.Models
{
    public class LetterModel
    {
        /// <summary>
        /// Добавляет письмо в БД
        /// </summary>
        /// <param name="letter"></param>
        /// <returns>True - успешно, False - ошибка</returns>
        public bool Add(Letter letter)
        {
            try
            {
                using (LettersDbContext db = new LettersDbContext())
                {
                    db.Letters.Add(letter);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }
        /// <summary>
        /// Получить все письма из БД. Если писем нет, то вернуть Null
        /// </summary>
        /// <returns>Letter[] or Null</returns>
        public Letter[] GetAllOrDefault()
        {
            try
            {
                using (LettersDbContext db = new LettersDbContext())
                {
                    var letters = db.Letters.Where(letter => letter.Id >= 0).ToArray();
                    return letters;
                }
            }
            catch { return null; }
        }
    }
}
