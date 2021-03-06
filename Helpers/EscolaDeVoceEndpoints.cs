using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaDeVoce.Frontend.Helpers
{
    public class EscolaDeVoceEndpoints
    {
        public const string baseUrl = "http://escola-api.azurewebsites.net/api/";
        public const string tokenUrl = "http://escola-api.azurewebsites.net/token";

        // public const string baseUrl = "http://edv-api.azurewebsites.net/";
        // public const string tokenUrl = "http://edv-api.azurewebsites.net/token";

        // public const string baseUrl = "http://localhost:5000/api/";
        // public const string tokenUrl = "http://localhost:5000/token";

        public class Category{
            public const string getCategories = EscolaDeVoceEndpoints.baseUrl + "categorias";
        }

        public class Project{
            public const string getProjects = EscolaDeVoceEndpoints.baseUrl + "projects";
        }

        public class News{
            public const string getNews = EscolaDeVoceEndpoints.baseUrl + "news";
        }

        public class Courses{
            public const string getCourses = EscolaDeVoceEndpoints.baseUrl + "courses";
            public const string getdetail = EscolaDeVoceEndpoints.baseUrl + "courses/details";
        }

        public class Dictionary{
            public const string get = EscolaDeVoceEndpoints.baseUrl + "dictionary";
        }

        public class Videos{
            public const string get = EscolaDeVoceEndpoints.baseUrl + "video";
            public const string addToFavorites = EscolaDeVoceEndpoints.baseUrl + "video/addVideoToFavorites";
            public const string updateStatus = EscolaDeVoceEndpoints.baseUrl + "video/updateVideoStatus";
            public const string getByCategory = EscolaDeVoceEndpoints.baseUrl + "video?categoriesId=";
            public const string getStatus = EscolaDeVoceEndpoints.baseUrl + "video/getStatus";
        }

        public class Especialist{
            public const string getEspecialists = EscolaDeVoceEndpoints.baseUrl + "especialists";
        }

        public class EscoleTalk{
            public const string get = EscolaDeVoceEndpoints.baseUrl + "escoletalk";
            public const string comment = EscolaDeVoceEndpoints.baseUrl + "escoletalk/comment";
        }

        public class Person{
            public const string getEmbaixadoras = EscolaDeVoceEndpoints.baseUrl + "person/embaixadoras";
            public const string get = EscolaDeVoceEndpoints.baseUrl + "person";
        }

        public class Questions{
            public const string get = EscolaDeVoceEndpoints.baseUrl + "question";
            public const string getNextQuestion = EscolaDeVoceEndpoints.baseUrl + "question/nextQuestion";
            public const string notAnsweredQuestions = EscolaDeVoceEndpoints.baseUrl + "question/notAnsweredQuestions";
            
        }

        public class User{
            public const string info = EscolaDeVoceEndpoints.baseUrl + "user/info";
            public const string create = EscolaDeVoceEndpoints.baseUrl + "user";
            public const string getChatMessages = EscolaDeVoceEndpoints.baseUrl + "user";
            public const string changeImage = EscolaDeVoceEndpoints.baseUrl + "user/saveProfileImage";
            public const string changeCover = EscolaDeVoceEndpoints.baseUrl + "user/saveProfileCover";
            public const string answerQuestion = EscolaDeVoceEndpoints.baseUrl + "answers/userAnswer";
            public const string nextQuestion = EscolaDeVoceEndpoints.baseUrl + "question/nextQuestion";
            public const string startCourse = EscolaDeVoceEndpoints.baseUrl + "user/startCourse";
            
        }

        public class Message{
            public const string get = EscolaDeVoceEndpoints.baseUrl + "messages";
            public const string getusermessages = EscolaDeVoceEndpoints.baseUrl + "messages/userMessages";
        }
    }
}
