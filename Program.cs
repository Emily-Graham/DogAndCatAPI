using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
/*
public class Statics
{
    public static readonly HttpClient CLIENT = new HttpClient();
}
*/
namespace DogAndCatAPI
{
    internal class Program
    {
        public enum Options : int
        {
            Cats,
            Dogs
        }

        public class ApiUtils
        {
            public static dynamic fetchData(string url)
            {
                HttpClient CLIENT = new HttpClient();
                Task<HttpResponseMessage> httpResponse = CLIENT.GetAsync(url);
                HttpResponseMessage response = httpResponse.Result;
                //Response Data
                HttpContent responseContent = response.Content;
                Task<string> responseData = responseContent.ReadAsStringAsync();
                string data = responseData.Result;

                dynamic deserializedData = JsonConvert.DeserializeObject<dynamic>(data);
                return deserializedData;
            }
        }

        public class Utils
        {
            //public static FetchDogs
            public static List<string> fetchDogs()
            {
                //empty list that will contain breeds
                List<string> dogBreeds = new List<string>();

                //call the apiUtil function
                dynamic dogData = ApiUtils.fetchData("https://dog.ceo/api/breeds/list");

                // add the breed as a string to the list of breeds
                foreach (var dogBreed in dogData.message)
                {
                    dogBreeds.Add(dogBreed.ToString());
                }

                return dogBreeds;
            }

            //public static FetchCats
            public static List<string> fetchCats()
            {
                //empty list that will contain breeds
                List<string> catBreeds = new List<string>();

                //call the apiUtil function
                dynamic catData = ApiUtils.fetchData("https://catfact.ninja/breeds");

                // add the breed as a string to the list of breeds
                foreach (var catBreed in catData.data)
                {
                    catBreeds.Add(catBreed.breed.ToString());
                }

                return catBreeds;
            } 

            public static void displayBreeds(int animal)
            {
                List<string> list = new List<string>();

                //if statement determines if call cats/dogs
                if(animal == 0)
                {
                    list = Utils.fetchCats();
                }
                else if(animal == 1)
                {
                    list = Utils.fetchDogs();
                }
               
                foreach (string breed in list)
                {
                    Console.WriteLine($"	{breed}");
                }
               
                Console.WriteLine($"{Environment.NewLine}Total Breeds: {list.Count}{Environment.NewLine}");
            }
        }

        static void Main(string[] args)
        {
            bool restart = true;

            while(restart)
            {
                Console.WriteLine("Which animal breeds would you like to see?");
                Console.WriteLine($"Enter 0 for cats.{Environment.NewLine}Enter 1 for dogs.");

                int userInput = Convert.ToInt32(Console.ReadLine()); //how to handle exceptions eg: userInput is 't'

                if (userInput == (int)Options.Cats)
                {
                    Console.WriteLine($"{Environment.NewLine}Cat Breeds: {Environment.NewLine}");
                    Utils.displayBreeds((int)Options.Cats);
                }
                else if (userInput == (int)Options.Dogs)
                {
                    Console.WriteLine($"{Environment.NewLine}Dog Breeds: {Environment.NewLine}");
                    Utils.displayBreeds((int)Options.Dogs);
                }
                else
                {
                    Console.WriteLine("You did not enter a valid option."); //how to return to start?
                }

                // Give user options to exit or call more results
                Console.WriteLine("would you like to continue? Type y to continue and hit enter. Hit enter to exit.");
                string userChoice = Console.ReadLine();
                if (userChoice == "y") { restart = true; }
                else if (userChoice != "y") { restart = false; }
            }
        }
    }
}

//Calum Questions
    //the global CLIENT variable
    //is the while loop okay? are there other methods?
    // Liitle insight about type Task
