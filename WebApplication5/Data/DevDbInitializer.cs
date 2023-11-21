using CsvHelper;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public static class DevDbInitializer
    {
        // adds random data from stack overflow db file and ai generated csv file for emails and password
        public static void Initialize(StackOverflowCloneContext context)
        {
            if (context.Question.Any()
                && context.Answer.Any()
                && context.Profile.Any()
                && context.Tag.Any())
            {
                return;   // DB has been seeded
            }
            
            string connectionString = "server=" + Environment.MachineName + 
                "\\SQLEXPRESS01;database=StackOverflow2010;integrated Security=SSPI;";

            List<Question> questions = new List<Question>();
            List<Answer> answers = new List<Answer>();
            List<Profile> profiles = new List<Profile>();

            List<Tag> tags = new List<Tag>()
            {
                new Tag() { Title = "C#" },
                new Tag() { Title = "Python" },
                new Tag() { Title = "Java" },
                new Tag() { Title = "Javascript" },
                new Tag() { Title = "Go" },
                new Tag() { Title = "Rust" },
                new Tag() { Title = "C++" },
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP (100) FROM [StackOverflow2010].[dbo].[Users]";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();

                // add profile names and dates from stack overflow db
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Profile profile = new Profile();
                        profile.Name = reader["DisplayName"].ToString();
                        profile.DateCreated = (DateTime)reader["CreationDate"];
                        profiles.Add(profile);
                    }
                }

                // add profile emails and passwords from ai generated csv file
                using (var reader = new StreamReader("filePersons.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    List<Profile> records = csv.GetRecords<Profile>().ToList();
                    int tally = 0;

                    PasswordHasher<Profile> hasher = new PasswordHasher<Profile>();
                    foreach (var prof in profiles)
                    {
                        prof.Email = records[tally].Email;

                        string pass = records[tally].Password;
                        prof.Password = hasher.HashPassword(prof, pass);
                        tally++;
                    }
                }

                string postQuery = "SELECT TOP (80) FROM [StackOverflow2010].[dbo].[Posts]";

                string exampleContent = 
                    @"{""ops"":[{""insert"":""Here is the first paragraph.\n\nList item 1""},{""attributes"":{""list"":""bullet""},""insert"":""\n""},{""insert"":""List item 2""},{""attributes"":{""list"":""bullet""},""insert"":""\n""},{""insert"":""Here's another paragraph, and now an image:\n""},{""insert"":""\n""}]}";
                string exampleTContent = "Here is the first paragraph";

                // add questions and answers (arbitrarily) from post table in stack overflow
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int tally = 0;
                    Random rand = new Random();
                    while (reader.Read())
                    {
                        if (reader.GetInt32("PostTypeId") == 1)
                        {
                            Question question = new Question();
                            question.Title = reader["Title"].ToString();
                            question.Content = exampleContent;
                            question.TruncatedContent = exampleTContent;
                            question.Author = profiles[tally];
                            question.VoteCount = reader.GetInt32("Score");
                            question.ViewCount = reader.GetInt32("ViewCount");
                            question.DateCreated = (DateTime)reader["CreationDate"];
                            question.Tags = tags.Take(rand.Next(7)).ToList();

                            questions.Add(question);
                            tally++;
                        }
                        else if (reader.GetInt32("PostTypeId") == 2)
                        {
                            Answer answer = new Answer();
                            answer.Content = exampleContent;
                            answer.TruncatedContent = exampleTContent;
                            answer.Author = profiles[tally];
                            answer.VoteCount = reader.GetInt32("Score");
                            answer.AssociatedQuestion = questions[-1];
                            answer.DateCreated = (DateTime)reader["CreationDate"];
                            answers.Add(answer);
                        }
                    }
                }
            }

            // seed db
            context.Profile.AddRange(profiles);
            context.Tag.AddRange(tags);
            context.Question.AddRange(questions);
            context.Answer.AddRange(answers);

            context.SaveChanges();
        }
    }
}