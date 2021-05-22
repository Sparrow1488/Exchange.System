using ExchangeServer.SQLDataBase;
using ExchangeSystem.Requests.Objects.Entities;
using System.Linq;

namespace ExchangeServer.MVC.Models
{
    public class PublicationModel
    {
        /// <summary>
        /// Получает список всех публикаций из БД
        /// </summary>
        /// <returns>Publication[] ; Publication[0] - список публикаци пуст ; Null - ошибка обращения к БД</returns>
        public Publication[] GetAllOrDefault()
        {
            try
            {
                using (PublicationsDbContext db = new PublicationsDbContext())
                {
                    var publications = db.Publications.Where(post => post.Id > -1).ToArray();
                    if (publications == null)
                        return new Publication[0];
                    else
                        return publications;
                }
            }
            catch { return null; }
        }
        public bool Add(Publication post)
        {
            try
            {
                using (PublicationsDbContext db = new PublicationsDbContext())
                {
                    db.Publications.Add(post);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }
    }
}
