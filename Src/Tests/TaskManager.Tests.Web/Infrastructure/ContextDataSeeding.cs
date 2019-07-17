namespace TaskManager.Tests.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using TaskManager.Domain.Entity;
    using TaskManager.Domain.Enum;
    using TaskManager.Persistence;

    public class ContextDataSeeding
    {
        public static void Run(
            TaskManagerDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            context.Users.AddRange(AddUsersToDatabase());
            AddRolesToUsers(context, roleManager, userManager);

            context.Projects.AddRange(AddProjectsToDatabase());
            context.Tasks.AddRange(AddTasksToDatabase());

            context.SaveChanges();
        }

        public static void AddRolesToUsers(
            TaskManagerDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            var users = context.Users;

            int i = 0;
            foreach (var user in users)
            {
                string roleName = string.Empty;
                IdentityRole role;

                if (i % 3 == 0)
                {
                    roleName = "Viewer";
                    role = new IdentityRole(roleName);
                    roleManager.CreateAsync(role);
                }
                else if (i % 3 == 1)
                {
                    roleName = "Developer";
                    role = new IdentityRole(roleName);
                    roleManager.CreateAsync(role);
                }
                else if (i % 3 == 2)
                {
                    roleName = "Manager";
                    role = new IdentityRole(roleName);
                    roleManager.CreateAsync(role);
                }

                userManager.UpdateSecurityStampAsync(user);
                userManager.AddToRoleAsync(user, roleName);

                i++;
            }
        }

        private static ApplicationUser[] AddUsersToDatabase()
        {
            int usersCount = 3;
            var users = new ApplicationUser[usersCount];

            for (int i = 0; i < usersCount; i++)
            {
                users[i] = new ApplicationUser
                {
                    FirstName = $"First{i}",
                    LastName = $"Name{i}"
                };
            }

            return users;
        }


        private static Project[] AddProjectsToDatabase()
        {
            int projectsCount = 6;
            var projects = new Project[projectsCount];

            for (int i = 0; i < projectsCount; i++)
            {
                projects[i] = new Project
                {
                    Name = $"Project{i}",
                    Description = $"Description{i}"
                };
            }

            return projects;
        }

        private static Task[] AddTasksToDatabase()
        {
            int tasksCount = 5;
            var tasks = new Task[tasksCount];

            for (int i = 0; i < tasksCount; i++)
            {
                if (i % 2 == 0)
                {
                    tasks[i] = new Task
                    {
                        Name = $"Task{i}",
                        Description = $"Desc{i}",
                        Priority = Priority.Low,
                        ProjectId = 6,
                        StoryPoints = 20
                    };
                }
                if (i == 2 || i == 4)
                {
                    tasks[i] = new Task
                    {
                        Name = $"Task{i}",
                        Description = $"Desc{i}",
                        Priority = Priority.Low,
                        ProjectId = 6,
                        StoryPoints = 20,
                        ApplicationUserId = "NotEmpty"
                    };
                }
                else
                {
                    tasks[i] = new Task
                    {
                        Name = $"Task{i}",
                        Description = $"Desc{i}",
                        Priority = Priority.High,
                        ProjectId = 6
                    };
                }
            }

            return tasks;
        }
    }
}
