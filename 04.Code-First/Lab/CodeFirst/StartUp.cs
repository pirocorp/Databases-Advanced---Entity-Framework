namespace CodeFirst
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            var context = new CodeFirstDbContext();

            ResetDatabase(context);

            //Joins(context);
            var posts = context.Posts
                .Select(p => new
                {
                    p.Category,
                    p.Title,
                    p.Content,
                    p.Author,
                    Tags = p.PostTags
                        .Where(pt => pt.PostId == p.Id)
                        .Select(pt => pt.Tag.Name)
                        .ToArray()
                })
                .ToList();

            foreach (var post in posts)
            {
                Console.WriteLine($"{post.Title} in {post.Category.Name} - {post.Author.Username}");
                Console.WriteLine(post.Content);
                var tagsString = string.Join(", ", post.Tags);
                if(!string.IsNullOrWhiteSpace(tagsString)) Console.WriteLine(tagsString);
                Console.WriteLine();
            }
        }

        private static void Joins(CodeFirstDbContext context)
        {
            var categories = context.Categories
                .Include(c => c.Posts)
                .ThenInclude(p => p.Author)
                .Include(c => c.Posts)
                .ThenInclude(p => p.Replies)
                .ThenInclude(r => r.Author)
                .ToList();

            //var categories = context.Categories
            //    .Select(c => new
            //    {
            //        c.Name,
            //        Posts = c.Posts
            //            .Select(p => new
            //            {
            //                p.Title,
            //                p.Content,
            //                p.Author.Username,
            //                Replies = p.Replies
            //                    .Select(r => new
            //                    {
            //                        r.Content,
            //                        r.Author.Username
            //                    })
            //                    .ToList()
            //            })
            //            .ToList()
            //    })
            //    .ToList();

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Name} - {category.Posts.Count}");

                foreach (var post in category.Posts)
                {
                    Console.WriteLine($"--{post.Title} - {post.Content} - {post.Author.Username}");

                    foreach (var reply in post.Replies)
                    {
                        Console.WriteLine($"----{reply.Content} - {reply.Author.Username}");
                    }
                }
            }
        }

        private static void ResetDatabase(CodeFirstDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            Seed(context);
        }

        private static void Seed(CodeFirstDbContext context)
        {
            var users = new[]
            {
                new User("Gosho", "123"),
                new User("Pesho", "456"),
                new User("Eftim", "123"),
                new User("Julan", "123"),
                new User("Valio", "123"),
                new User("Georgi", "123"),
            };

            //Cascade insert will insert only first 4 users
            //because last two users are not mentioned in any entity that is 
            //inserted that is why i add them explicitly
            context.Users.AddRange(users);

            var categories = new[]
            {
                new Category("C#"),
                new Category("Support"),
                new Category("Python"),
                new Category("Java"),
            };

            var posts = new[]
            {
                new Post("C# Rulz", "Verno", categories[0], users[0]),
                new Post("Python Rulz", "Verno", categories[2], users[1]),
                new Post("No support", "Fuckers", categories[1], users[2]),
                new Post("Java Sucks", "Javarast", categories[3], users[3])
            };

            context.Posts.AddRange(posts);

            var replies = new[]
            {
                new Reply("Yes we know", posts[2], users[0]), 
                new Reply("Yep", posts[0], users[0]),
            };

            context.Replies.AddRange(replies);

            var tags = new[]
            {
                new Tag("C#"),
                new Tag("Java"),
                new Tag("Python"), 
                new Tag("Other"), 
            };

            var postTags = new[]
            {
                new PostTag() {PostId = 1, Tag = tags[0]}, 
                new PostTag() {PostId = 4, Tag = tags[1]}, 
                new PostTag() {PostId = 2, Tag = tags[2]}, 
                new PostTag() {PostId = 3, Tag = tags[3]},
            };

            context.Tags.AddRange(tags);
            context.PostsTags.AddRange(postTags);
            context.SaveChanges();
        }
    }
}