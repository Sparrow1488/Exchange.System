using Exchange.Server.SQLDataBase;
using Exchange.System.Requests.Objects.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Server.Models
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
                    List<Publication> publications = new List<Publication>();
                    var allDbPosts = db.Publications.Where(post => post.Id >= 0).ToArray();
                    foreach (var post in allDbPosts)
                    {
                        using (SourcesDbContext sources = new SourcesDbContext())
                        {
                            var findSource = sources.Sources.Where(src => src.Publication.Id == post.Id).ToArray();
                            post.Sources = findSource;
                            publications.Add(post);
                        }
                    }
                    if (publications == null)
                        return new Publication[0];
                    else
                        return publications.OrderByDescending(post => post.DateCreate).ToArray();
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
