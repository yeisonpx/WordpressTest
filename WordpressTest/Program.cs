using System;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace WordpressTest
{
    class Program
    {
        static  void Main(string[] args)
        {
            CreatePost().Wait();
        }

        private static async Task CreatePost()
        {
            try
            {
                WordPressClient client = await GetClient();
                if (await client.IsValidJWToken())
                {
                    var post = new Post
                    {
                        Title = new Title("New post test"),
                        Content = new Content("<h2>Test de contenido titulo h2</h2>")

                    };

                    await client.Posts.Create(post);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);

            }
            Console.ReadKey();
        }

        private static async Task<WordPressClient> GetClient()
        {
            // JWT authentication
            var client = new WordPressClient("http://yeisonpx-001-site1.itempurl.com/wp-json/");
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken("sonuzer-admin", "EfUCVcfY*cTdFqh!$eq0tlUm");
            return client;
        }
    }
}
