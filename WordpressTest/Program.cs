using System;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace WordPressTest
{
    class Program
    {
        static void Main(string[] args)
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
                        Content = new Content("Content for new post.")
                    };
                    await client.Posts.Create(post);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }

        private static async Task<WordPressClient> GetClient()
        {
            // JWT authentication
            var client = new WordPressClient("http://wordpress-domain.com/wp-json/");
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken("user", "password");
            return client;
        }
    }
}