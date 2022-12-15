using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _blogId;

        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _blogId = blogId;
        }

        public IUserInterfaceManager Execute()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"{blog.Title} Details: ");
            Console.WriteLine(" 1) View Blog");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) View Blog Posts");
            Console.WriteLine(" 5) View Blog Tags");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddBlog();
                    return this;
                case "3":
                    return _parentUI;
                case "4":
                    return _parentUI;
                case "5":
                    return _parentUI;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"Name: {blog.Title}");
            Console.WriteLine($"Bio: {blog.Url}");
           
            Console.WriteLine();
            
            Console.WriteLine($"{blog.Title} - {blog.Url} ");
        }

        

        private void AddBlog()
        {
           
            List<Tag> tags = _tagRepository.GetAll();
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write(">");

            int choice = int.Parse(Console.ReadLine());
            
        }
    }
}