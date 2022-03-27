using Exchange.Server.SQLDataBase;
using Exchange.System.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Server.Models
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
        /// <returns>Letter[i > 0] - если есть письма; Letter[0] - если в БД нет писем; Null - возникла ошибка в БД</returns>
        public Letter[] GetAllOrDefault()
        {
            try
            {
                using (LettersDbContext db = new LettersDbContext())
                {
                    var fromDbLetters = db.Letters.Where(letter => letter.Id >= 0).ToList();
                    if (fromDbLetters == null || fromDbLetters.Count == 0)
                        return new Letter[0];
                    var letters = new List<Letter>();
                    foreach (var letter in fromDbLetters)
                    {
                        using (SourcesDbContext sources = new SourcesDbContext())
                        {
                            var findSource = sources.Sources.Where(src => src.Letter.Id == letter.Id).ToArray();
                            letter.Sources = findSource;
                            letters.Add(letter);
                        }
                    }
                    return letters.ToArray();
                }
            }
            catch { return null; }
        }
    }
}
